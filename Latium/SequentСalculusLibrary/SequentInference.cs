using System;
using System.Collections.Generic;

namespace SequentСalculusLibrary
{
    public class SequentInference
    {
        private Dictionary<SequentFormula, int> _inferenceSet;
        private SequentFormula _lastFormula;
        private readonly Dictionary<SequentFormula, List<SequentFormula>> _previous;

        public SequentInference()
        {
            _inferenceSet = new Dictionary<SequentFormula, int>();
            _lastFormula = null;
            _previous = new Dictionary<SequentFormula, List<SequentFormula>>();
            Length = 0;
        }

        public int Length { get; private set; }

        public SequentFormula LastFormula
        {
            get
            {
                if (Length == 0)
                    throw new IndexOutOfRangeException();
                return _lastFormula;
            }
        }

        private void Add(SequentFormula formula)
        {
            _inferenceSet.Add(formula, Length);
            _lastFormula = formula;
            Length++;
        }

        public bool Contains(SequentFormula formula)
        {
            return _inferenceSet.ContainsKey(formula);
        }

        public bool Push(SequentFormula formula)
        {
            if (Contains(formula))
                return false;

            if (SequentAxiom.IsAxiom(formula))
            {
                Add(formula);
                return true;
            }

            foreach (var f1 in _inferenceSet)
            {
                if (formula.Equals(ConjunctionRemovingLeft.Apply(f1.Key)))
                {
                    _previous[formula] = new List<SequentFormula>{f1.Key};
                    return true;
                }
                if (formula.Equals(ConjunctionRemovingRight.Apply(f1.Key)))
                {
                    _previous[formula] = new List<SequentFormula> { f1.Key };
                    return true;
                }
                throw new NotImplementedException();
            }

            return false;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public void Minimize()
        {
            var newInference = new SequentInference();
            newInference.DfsPush(LastFormula);
            _inferenceSet = newInference._inferenceSet;
        }

        private void DfsPush(SequentFormula formula)
        {
            if (Contains(formula))
                return;

            if (SequentAxiom.IsAxiom(formula))
            {
                Add(formula);
                return;
            }

            foreach (var f in _previous[formula])
                DfsPush(f);
            Add(formula);
        }

    }
}
