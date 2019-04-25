using System.Collections.Generic;
using PropositionLibrary;

namespace SequentСalculusLibrary
{
    public static class ImplicationRemoving
    {
        public static SequentFormula Apply(SequentFormula first, SequentFormula second)
        {
            if (first.Consequent == null || second.Consequent == null)
                return null;
            if (Implication.GetInstance().Equals(first.Consequent.TopConnective) == false)
                return null;
            if (first.Consequent.GetOperand(0).Equals(second.Consequent) == false)
                return null;

            var newAntecedents = new HashSet<Formula>();
            foreach (var antecedent in first)
                newAntecedents.Add(antecedent);
            foreach (var antecedent in second)
                newAntecedents.Add(antecedent);

            var newConsequent = second.Consequent;

            return new SequentFormula(newAntecedents, newConsequent);
        }
    }
}
