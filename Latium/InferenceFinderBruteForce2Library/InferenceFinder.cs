using System.Collections.Generic;
using System.Threading.Tasks;
using PropositionalCalculusLibrary;
using PropositionLibrary;


//проблема в том, что формулы на втором раунде не умещаются в память
//решение: по мимо генерирования формул также запаралелить подстановку их в вывод

namespace InferenceFinderBruteForce2Library
{
    public static class InferenceFinder
    {
        public static Inference FindInference(Formula target, Hypotheses hypotheses)
        {
            var inference = new Inference(hypotheses);

            if (inference.Push(target))
            {
                inference.Minimize();
                return inference;
            }
            var formulas = new HashSet<Formula>();

            if (hypotheses is null == false)
                foreach (var h in hypotheses)
                {
                    inference.Push(h);
                    formulas.Add(h);
                }

            ParseFormula(target,formulas);

            while (true)
            {
                var newF = new HashSet<Formula>();

                //foreach (var a in Axioms.GetAxioms())
                //{
                //    GenAxioms(target, formulas, a, inference, newF);
                //}

                Parallel.ForEach(Axioms.GetAxioms(),a => GenAxioms(target, formulas, a, inference, newF));

                foreach (var f in newF)
                {
                    formulas.Add(f);
                }
                newF.Clear();

                var ok = true;
                while (ok)
                {
                    ok = false;
                    foreach (var f in formulas)
                    {
                        if (inference.Push(f))
                        {
                            ok = true;
                            if (target.Equals(f))
                            {
                                ok = false;
                                break;
                            }
                        }
                    }
                }

                if(inference.Contains(target))
                    break;
            }

            inference.Minimize();
            return inference;
        }

        private static void GenAxioms(Formula target, HashSet<Formula> formulas, Axiom a, Inference inference, HashSet<Formula> newF)
        {
            foreach (var f1 in formulas)
            {
                foreach (var f2 in formulas)
                {
                    foreach (var f3 in formulas)
                    {
                        var f = SubstituteInFormula(a, f1, f2, f3);
                        if (f.Equals(target) == false)
                        {
                            var ok = true;
                            lock (inference)
                            {
                                ok = inference.Push(f);
                            }
                            if (ok)
                                ParseFormula(f, newF);
                        }
                    }
                }
            }
        }

        private static void ParseFormula(Formula formula, HashSet<Formula> formulasSet)
        {
            if (formulasSet.Contains(formula))
                return;
            formulasSet.Add(formula);

            var connective = formula.TopConnective;
            if (connective is null)
                return;

            for (var i = 0; i < connective.OperandsCount; i++)
            {
                var nxtFormula = formula.GetOperand(i);
                ParseFormula(nxtFormula, formulasSet);
            }
        }

        private static Formula SubstituteInFormula(Formula formula, Formula a, Formula b, Formula c)
        {
            var subs = new Dictionary<PropositionalVariable, Formula>();

            if (a != null)
                subs.Add(new PropositionalVariable("A"), a);

            if (b != null)
                subs.Add(new PropositionalVariable("B"), b);

            if (c != null)
                subs.Add(new PropositionalVariable("C"), c);

            return formula.SubstituteInFormula(subs);
        }
    }
}
