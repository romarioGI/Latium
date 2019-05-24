using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PropositionLibrary;
using PropositionalCalculusLibrary;

namespace PropositionalCalculusInferenceFinder
{
    public static class InferenceFinder
    {
        private static readonly Random Rd = new Random(DateTime.Now.Millisecond);

        public static Inference FindInference(Hypotheses hypotheses, Formula target)
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

            ParseFormula(target, formulas);

            var addingFormulas = new Thread(() => AddingFormulas(inference, formulas, target));
            var genFormulas = new Thread(() => GenFormulas(inference, formulas, target));

            addingFormulas.Start();
            genFormulas.Start();

            while (true)
            {
                lock (inference)
                {
                    if (inference.Push(target) || inference.Contains(target))
                    {
                        break;
                    }
                }

                Thread.Sleep(1000);
            }

            addingFormulas.Abort();
            genFormulas.Abort();

            inference.Minimize();
            return inference;
        }

        private static void AddingFormulas(Inference inference, HashSet<Formula> formulas, Formula target)
        {
            var form = new HashSet<Formula>();

            while (true)
            {
                foreach (var f in form)
                    if (f.Equals(target) == false)
                        lock (inference)
                        {
                            if (inference.Contains(target))
                                return;
                            inference.Push(f);
                        }

                lock (formulas)
                {
                    form.UnionWith(formulas);
                }
            }
        }

        private static void GenFormulas(Inference inference, HashSet<Formula> formulas, Formula target)
        {
            while (true)
            {
                var form = formulas.OrderBy(x => Rd.Next()).ToList();

                Parallel.ForEach(Axioms.GetAxioms(), a => GenAxiom(inference, form, formulas, target, a));
            }
        }

        private static void GenAxiom(Inference inference, List<Formula> formulas, HashSet<Formula> allFormulas,
            Formula target, Axiom a)
        {
            foreach (var f1 in formulas)
            {
                foreach (var f2 in formulas)
                {
                    foreach (var f3 in formulas)
                    {
                        var f = SubstituteInFormula(a, f1, f2, f3);
                        if (f.Equals(target))
                            continue;
                        bool ok;
                        lock (inference)
                        {
                            if (inference.Contains(target))
                                return;
                            ok = inference.Push(f);
                        }

                        lock (allFormulas)
                        {
                            if (ok)
                            {
                                ParseFormula(f, allFormulas);
                            }
                        }
                    }
                }
            }
        }

        private static void ParseFormula(Formula formula, HashSet<Formula> newFormulas)
        {
            if (newFormulas.Contains(formula))
                return;
            newFormulas.Add(formula);

            var connective = formula.TopConnective;
            if (connective is null)
                return;

            for (var i = 0; i < connective.OperandsCount; i++)
            {
                var nxtFormula = formula.GetOperand(i);
                ParseFormula(nxtFormula, newFormulas);
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
