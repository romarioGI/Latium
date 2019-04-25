using System.Collections.Generic;
using PropositionLibrary;

namespace SequentСalculusLibrary
{
    public static class NegationRemoving
    {
        public static SequentFormula Apply(Formula additionalFormula, SequentFormula first)
        {
            if (first.Consequent != null)
                return null;
            if (additionalFormula == null)
                return null;
            if (Negation.GetInstance().Equals(additionalFormula.TopConnective) == false)
                return null;
            if (first.ContainsAntecedent(additionalFormula) == false)
                return null;

            var newAntecedents = new HashSet<Formula>();
            foreach (var antecedent in first)
                newAntecedents.Add(antecedent);

            var newConsequent = additionalFormula.GetOperand(0);

            return new SequentFormula(newAntecedents, newConsequent);
        }
    }
}
