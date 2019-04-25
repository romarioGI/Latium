using System.Collections.Generic;
using PropositionLibrary;

namespace SequentСalculusLibrary
{
    public static class DisjunctionRemoving
    {
        public static SequentFormula Apply(SequentFormula first, SequentFormula second, SequentFormula third)
        {
            if (Disjunction.GetInstance().Equals(first.Consequent.TopConnective) == false)
                return null;
            if (second.Consequent == null || third.Consequent == null)
                return null;
            if (second.Consequent.Equals(third.Consequent) == false)
                return null;

            var newAntecedents = new HashSet<Formula>();
            foreach (var antecedent in first)
                newAntecedents.Add(antecedent);
            foreach (var antecedent in second)
                newAntecedents.Add(antecedent);
            foreach (var antecedent in third)
                newAntecedents.Add(antecedent);

            var newConsequent = second.Consequent;

            return new SequentFormula(newAntecedents, newConsequent);
        }
    }
}