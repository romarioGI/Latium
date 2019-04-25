using System.Collections.Generic;
using PropositionLibrary;
using PropositionalCalculusLibrary;

namespace InferenceFinderBruteForceLibrary
{
    public class InferenceFinderBruteForce
    {
        public readonly Hypotheses Hypotheses;
        public readonly Formula Target;
        private Inference _inference;

        public InferenceFinderBruteForce(Formula target, Hypotheses hypotheses = null)
        {
            Target = target;
            Hypotheses = hypotheses;
            _inference = null;
        }

        private static readonly string Impl = "(A" + Implication.Symbol + "B)";

        public Inference BuildInference()
        {
            if (_inference != null)
                return _inference;
            var inference = new Inference(Hypotheses);

            if (inference.Push(Target))
                return _inference = inference;

            var addedFormulas = new HashSet<Formula>();
            var newAddedFormulas = new HashSet<Formula>();
            var queue = new HashSet<Formula>();
            var newQueue = new HashSet<Formula>();
            ParseFormula(Target, queue);

            if (Hypotheses != null)
            {
                foreach (var h in Hypotheses)
                {
                    if (inference.Push(h))
                        addedFormulas.Add(h);
                }
            }

            foreach (var q in queue)
            {
                addedFormulas.Add(q);
            }

            while (true)
            {
                foreach (var aF in addedFormulas)
                {
                    foreach (var q in queue)
                    {
                        var f = SubstituteInFormula(new Formula(Impl), aF, q, null);
                        if (inference.Push(f))
                            newAddedFormulas.Add(f);
                        else
                            newQueue.Add(f);
                    }
                }

                foreach (var q in newQueue)
                    queue.Add(q);
                newQueue.Clear();

                foreach (var naF in newAddedFormulas)
                    addedFormulas.Add(naF);
                newAddedFormulas.Clear();

                var ok = true;
                while (ok)
                {
                    ok = false;
                    foreach (var f in queue)
                    {
                        if (inference.Push(f))
                        {
                            if (f.Equals(Target))
                            {
                                ok = false;
                                break;
                            }

                            ok = true;
                            newAddedFormulas.Add(f);
                        }
                    }
                }

                foreach (var naF in newAddedFormulas)
                {
                    addedFormulas.Add(naF);
                    queue.Remove(naF);
                }

                newAddedFormulas.Clear();

                if (inference.Length != 0 && inference.LastFormula.Equals(Target))
                    break;

                if (inference.Push(Target))
                    break;
            }

            inference.Minimize();
            return _inference = inference;
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
            var subs = new Dictionary<PropositionalVariable, Formula>
            {
                {new PropositionalVariable("A"), a},
                {new PropositionalVariable("B"), b},

            };

            if (c != null)
                subs.Add(new PropositionalVariable("C"), c);

            return formula.SubstituteInFormula(subs);
        }
    }
}
