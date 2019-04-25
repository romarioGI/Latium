using System.Collections.Generic;
using PropositionLibrary;

namespace SequentСalculusLibrary
{
    public static class NegationIntroduction
    {
        private static readonly Formula NegationTerm = new Formula("(" + Negation.Symbol + "A)");
        private static readonly PropositionalVariable A = (PropositionalVariable) NegationTerm.GetOperand(0);

        public static SequentFormula Apply(Formula additionalFormula, SequentFormula first)
        {
            if (first.Consequent != null)
                return null;
            if (additionalFormula == null)
                return null;
            if (first.ContainsAntecedent(additionalFormula) == false)
                return null;

            var newAntecedents = new HashSet<Formula>();
            foreach (var antecedent in first)
                newAntecedents.Add(antecedent);

            var subs = new Dictionary<PropositionalVariable, Formula> {{A, additionalFormula}};
            var newConsequent = NegationTerm.SubstituteInFormula(subs);

            return new SequentFormula(newAntecedents, newConsequent);
        }
    }
}
