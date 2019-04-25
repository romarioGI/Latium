using System.Collections.Generic;
using PropositionLibrary;

namespace SequentСalculusLibrary
{
    public static class ToContradiction
    {
        public static SequentFormula Apply(SequentFormula first, SequentFormula second)
        {
            if (first.Consequent == null || second.Consequent == null)
                return null;
            if (Negation.GetInstance().Equals(second.Consequent.TopConnective))
                return null;
            if (first.Consequent.Equals(second.Consequent.GetOperand(0)) == false)
                return null;

            var newAntecedents = new HashSet<Formula>();
            foreach (var antecedent in first)
                newAntecedents.Add(antecedent);
            foreach (var antecedent in second)
                newAntecedents.Add(antecedent);

            return new SequentFormula(newAntecedents, null);
        }
    }
}
