using System.Collections.Generic;
using PropositionLibrary;

namespace SequentСalculusLibrary
{
    public static class DisjunctionIntroductionLeft
    {
        private static readonly Formula DisjunctionTerm = new Formula("(A" + Disjunction.Symbol + "B)");
        private static readonly PropositionalVariable A = (PropositionalVariable) DisjunctionTerm.GetOperand(0);
        private static readonly PropositionalVariable B = (PropositionalVariable) DisjunctionTerm.GetOperand(1);

        public static SequentFormula Apply(Formula additionalFormula, SequentFormula first)
        {
            if (additionalFormula == null || first.Consequent == null)
                return null;

            var newAntecedents = new HashSet<Formula>();
            foreach (var antecedent in first)
                newAntecedents.Add(antecedent);

            var subs = new Dictionary<PropositionalVariable, Formula> {{A, first.Consequent}, {B, additionalFormula}};
            var newConsequent = DisjunctionTerm.SubstituteInFormula(subs);

            return new SequentFormula(newAntecedents, newConsequent);
        }
    }

    public static class DisjunctionIntroductionRight
    {
        private static readonly Formula DisjunctionTerm = new Formula("(A" + Disjunction.Symbol + "B)");
        private static readonly PropositionalVariable A = (PropositionalVariable) DisjunctionTerm.GetOperand(0);
        private static readonly PropositionalVariable B = (PropositionalVariable) DisjunctionTerm.GetOperand(1);

        public static SequentFormula Apply(Formula additionalFormula, SequentFormula first)
        {
            if (additionalFormula == null || first.Consequent == null)
                return null;

            var newAntecedents = new HashSet<Formula>();
            foreach (var antecedent in first)
                newAntecedents.Add(antecedent);

            var subs = new Dictionary<PropositionalVariable, Formula> {{B, first.Consequent}, {A, additionalFormula}};
            var newConsequent = DisjunctionTerm.SubstituteInFormula(subs);

            return new SequentFormula(newAntecedents, newConsequent);
        }
    }
}
