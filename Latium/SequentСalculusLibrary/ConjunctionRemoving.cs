using System;
using System.Collections.Generic;
using PropositionLibrary;

namespace SequentСalculusLibrary
{
    public class ConjunctionRemovingLeft:SequentInferenceRule
    {
        private readonly SequentFormula _first;
        private readonly int _firstNumber;

        public ConjunctionRemovingLeft(SequentFormula first, int firstNumber)
        {
            _first = first;
            _firstNumber = firstNumber;
            IsValid = _first.Consequent != null && _first.Consequent.TopConnective.Equals(Conjunction.GetInstance());
        }

        protected override SequentFormula Apply()
        {
            if (!IsValid)
                throw new ArgumentException();

            var newAntecedents = new HashSet<Formula>();
            foreach (var antecedent in _first)
                newAntecedents.Add(antecedent);
            var newConsequent = _first.Consequent.GetOperand(0);

            return new SequentFormula(newAntecedents, newConsequent);
        }

        public override string ToString()
        {
            return StringConstants.ConjunctionRemoving + " " + _firstNumber;
        }
    }

    public class ConjunctionRemovingRight : SequentInferenceRule
    {
        private readonly SequentFormula _first;
        private readonly int _firstNumber;

        public ConjunctionRemovingRight(SequentFormula first, int firstNumber)
        {
            _first = first;
            _firstNumber = firstNumber;
            IsValid = _first.Consequent != null && _first.Consequent.TopConnective.Equals(Conjunction.GetInstance());
        }

        protected override SequentFormula Apply()
        {
            if (!IsValid)
                throw new ArgumentException();

            var newAntecedents = new HashSet<Formula>();
            foreach (var antecedent in _first)
                newAntecedents.Add(antecedent);
            var newConsequent = _first.Consequent.GetOperand(1);

            return new SequentFormula(newAntecedents, newConsequent);
        }

        public override string ToString()
        {
            return StringConstants.ConjunctionRemoving + " " + _firstNumber;
        }
    }
}
