using PropositionLibrary;

namespace SequentСalculusLibrary
{
    public static class SequentAxiom
    {
        public static bool IsAxiom(SequentFormula formula)
        {
            Formula antecedent = null;
            var cnt = 0;
            foreach (var f in formula)
            {
                antecedent = f;
                cnt++;
                if (cnt > 1)
                    return false;
            }

            return cnt != 0 && formula.Consequent.Equals(antecedent);
        }
    }
}
