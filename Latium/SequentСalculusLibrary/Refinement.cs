using System.Collections.Generic;
using PropositionLibrary;

namespace SequentСalculusLibrary
{
    public static class Refinement
    {
        public static SequentFormula Apply(Formula additionalFormula, SequentFormula first)
        {
            if (first.Consequent != null)
                return null;
            if (additionalFormula == null)
                return null;

            var newAntecedents = new HashSet<Formula>();
            foreach (var antecedent in first)
                newAntecedents.Add(antecedent);

            return new SequentFormula(newAntecedents, additionalFormula);
        }
    }
}
