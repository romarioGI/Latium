using System.Collections.Generic;
using PropositionLibrary;

namespace SequentСalculusLibrary
{
    public static class Expansion
    {
        public static SequentFormula Apply(Formula additionalFormula, SequentFormula first)
        {
            if (additionalFormula == null)
                return null;

            var newAntecedents = new HashSet<Formula>();
            foreach (var antecedent in first)
                newAntecedents.Add(antecedent);
            newAntecedents.Add(additionalFormula);

            return new SequentFormula(newAntecedents, first.Consequent);
        }
    }
}
