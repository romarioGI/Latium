using System.Collections.Generic;
using PropositionLibrary;

namespace SequentСalculusLibrary
{
    public static class ImplicationIntroduction
    {
        private static readonly Formula ImplicationTerm = new Formula("(A" + Implication.Symbol + "B)");
        private static readonly PropositionalVariable A = (PropositionalVariable)ImplicationTerm.GetOperand(0);
        private static readonly PropositionalVariable B = (PropositionalVariable)ImplicationTerm.GetOperand(1);

        public static SequentFormula Apply(Formula additionalFormula, SequentFormula first)
        {
            if(first.ContainsAntecedent(additionalFormula) == false)
                return null;

            var newAntecedents = new HashSet<Formula>();
            foreach (var antecedent in first)
                newAntecedents.Add(antecedent);

            var subs = new Dictionary<PropositionalVariable, Formula> { { B, first.Consequent }, { A, additionalFormula } };
            var newConsequent = ImplicationTerm.SubstituteInFormula(subs);

            return new SequentFormula(newAntecedents, newConsequent);
        }
    }
}
