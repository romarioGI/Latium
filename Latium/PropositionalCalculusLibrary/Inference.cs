using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PropositionLibrary;

namespace PropositionalCalculusLibrary
{
    public class Inference
    {
        private Dictionary<Formula, int> _inferenceSet;
        private Formula _lastFormula;

        public readonly Hypotheses Hypotheses;

        public Inference(Hypotheses hypotheses = null)
        {
            Hypotheses = hypotheses;
            _inferenceSet = new Dictionary<Formula, int>();
            _lastFormula = null;
            Length = 0;
        }

        public int Length { get; private set; }

        public Formula LastFormula
        {
            get
            {
                if (Length == 0)
                    throw new IndexOutOfRangeException();
                return _lastFormula;
            }
        }
        
        private void Add(Formula formula)
        {
            _inferenceSet.Add(formula, Length);
            _lastFormula = formula;
            Length++;
        }

        public bool Contains(Formula formula)
        {
            return _inferenceSet.ContainsKey(formula);
        }

        public bool Push(Formula formula)
        {
            if (Contains(formula))
                return false;

            if (Hypotheses != null && Hypotheses.Contains(formula))
            {
                Add(formula);
                return true;
            }

            if (Axioms.Contains(formula))
            {
                Add(formula);
                return true;
            }

            var mpFormula = FindMpPair(formula);

            if (mpFormula != null)
            {
                Add(mpFormula);
                return true;
            }

            return false;
        }

        private MpFormula FindMpPair(Formula formula)
        {
            /*
             * так как в алгоритме добавляется много аксиом, то вывод растет быстро, а значит стоит всё таки оптимизирвать поиск МПпары
             * либо нужно убедиться, что HashSet.First() работает быстро
             */
            foreach (var fB in _inferenceSet.Keys)
            {
                if (fB.TopConnective is Implication == false)
                    continue;

                if (fB.GetOperand(1).Equals(formula) == false)
                    continue;

                var fA = fB.GetOperand(0);
                if (_inferenceSet.ContainsKey(fA) == false)
                    continue;

                fA = _inferenceSet.Keys.First(x => x.Equals(fA));

                return MpFormula.GetMpFormula(fA, fB);
            }

            return null;
        }

        public override string ToString()
        {
            var ret = new string[Length];

            foreach (var f in _inferenceSet.Keys)
            {
                var line = new StringBuilder();
                line.Append($"{_inferenceSet[f]}. ");
                line.Append(f);

                if (Hypotheses != null && Hypotheses.Contains(f))
                {
                    line.Append(StringConstants.IsHypothesis);
                }
                else if (Axioms.Contains(f))
                {
                    line.Append("; ");
                    line.Append(Axioms.FindAppropriateAxiom(f).Name);
                }
                else if (f is MpFormula mpF)
                {
                    line.Append(StringConstants.IsMpFormula);
                    var indA = _inferenceSet[mpF.A];
                    var indB = _inferenceSet[mpF.B];
                    line.Append(indA + ", " + indB);
                }
                else
                {
                    throw new Exception();
                }

                ret[_inferenceSet[f]] = line.ToString();
            }

            return string.Join(StringConstants.LineEnd, ret);
        }

        public void Minimize()
        {
            var newInference = new Inference(Hypotheses);
            newInference.DfsPush(LastFormula);
            _inferenceSet = newInference._inferenceSet;
        }

        private void DfsPush(Formula formula)
        {
            if (Contains(formula))
                return;

            if (Hypotheses != null && Hypotheses.Contains(formula))
            {
                Add(formula);
                return;
            }

            if (Axioms.Contains(formula))
            {
                Add(formula);
                return;
            }

            if (formula is MpFormula mpFormula)
            {
                DfsPush(mpFormula.A);
                DfsPush(mpFormula.B);
                Add(formula);
                return;
            }

            throw new Exception();
        }
    }
}