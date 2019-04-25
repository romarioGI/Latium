using System;
using System.Collections.Generic;
using PropositionLibrary;

namespace SequentСalculusLibrary
{
    public class ConjunctionIntroduction : SequentInferenceRule
    {
        private static readonly Formula ConjunctionTerm = new Formula("(A" + Conjunction.Symbol + "B)");
        private static readonly PropositionalVariable A = (PropositionalVariable) ConjunctionTerm.GetOperand(0);
        private static readonly PropositionalVariable B = (PropositionalVariable) ConjunctionTerm.GetOperand(1);

        private readonly SequentFormula _first;
        private readonly SequentFormula _second;
        private readonly int _firstNumber;
        private readonly int _secondNumber;    

        public ConjunctionIntroduction(SequentFormula first, int firstNumber, SequentFormula second, int secondNumber)
        {
            _first = first;
            _second = second;
            _firstNumber = firstNumber;
            _secondNumber = secondNumber;
            IsValid = _first.Consequent != null && _second.Consequent != null;
        }

        protected override SequentFormula Apply()
        {
            if(!IsValid)
                throw new ArgumentException();

            var newAntecedents = new HashSet<Formula>();
            foreach (var antecedent in _first)
                newAntecedents.Add(antecedent);
            foreach (var antecedent in _second)
                newAntecedents.Add(antecedent);

            var subs = new Dictionary<PropositionalVariable, Formula> {{A, _first.Consequent}, {B, _second.Consequent}};
            var newConsequent = ConjunctionTerm.SubstituteInFormula(subs);

            return new SequentFormula(newAntecedents, newConsequent);
        }

        public override string ToString()
        {
            return StringConstants.ConjunctionIntroduction + " " + _firstNumber + ", " + _secondNumber;
        }
    }
}
