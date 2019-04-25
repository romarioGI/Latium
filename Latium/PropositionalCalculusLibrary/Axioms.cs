using PropositionLibrary;

namespace PropositionalCalculusLibrary
{
    public class Axiom : Formula
    {
        public readonly string Name;

        internal Axiom(string str, string name) : base(str)
        {
            Name = name;
        }
    }

    public static class Axioms
    {
        #region Axioms

        private static readonly Axiom FirstImplicative =
            new Axiom("(A" + Implication.Symbol + "(B" + Implication.Symbol + "A))",
                StringConstants.FirstImplicativeAxiom);

        private static readonly Axiom SecondImplicative =
            new Axiom("((A" + Implication.Symbol + "(B" + Implication.Symbol + "C))" + Implication.Symbol + "((A" +
                      Implication.Symbol + "B)" + Implication.Symbol + "(A" + Implication.Symbol + "C)))",
                StringConstants.SecondImplicativeAxiom);

        private static readonly Axiom FirstConjunctive =
            new Axiom("((A" + Conjunction.Symbol + "B)" + Implication.Symbol + "A)",
                StringConstants.FirstConjunctiveAxiom);

        private static readonly Axiom SecondConjunctive =
            new Axiom("((A" + Conjunction.Symbol + "B)" + Implication.Symbol + "B)",
                StringConstants.SecondConjunctiveAxiom);

        private static readonly Axiom ThirdConjunctive =
            new Axiom("((A" + Implication.Symbol + "B)" + Implication.Symbol + "((A" + Implication.Symbol + "C)" +
                      Implication.Symbol + "(A" + Implication.Symbol + "(B" + Conjunction.Symbol + "C))))",
                StringConstants.ThirdConjunctiveAxiom);

        private static readonly Axiom FirstDisjunctive =
            new Axiom("(A" + Implication.Symbol + "(A" + Disjunction.Symbol + "B))",
                StringConstants.FirstDisjunctiveAxiom);

        private static readonly Axiom SecondDisjunctive =
            new Axiom("(B" + Implication.Symbol + "(A" + Disjunction.Symbol + "B))",
                StringConstants.SecondDisjunctiveAxiom);

        private static readonly Axiom ThirdDisjunctive =
            new Axiom("((A" + Implication.Symbol + "C)" + Implication.Symbol + "((B" + Implication.Symbol + "C)" +
                      Implication.Symbol + "((A" + Disjunction.Symbol + "B)" + Implication.Symbol + "C)))",
                StringConstants.ThirdDisjunctiveAxiom);

        private static readonly Axiom FirstNegative =
            new Axiom("((A" + Implication.Symbol + "(" + Negation.Symbol + "B))" + Implication.Symbol + "(B" +
                      Implication.Symbol + "(" + Negation.Symbol + "A)))", StringConstants.FirstNegativeAxiom);

        private static readonly Axiom SecondNegative =
            new Axiom("((" + Negation.Symbol + "(" + Negation.Symbol + "A))" + Implication.Symbol + "A)",
                StringConstants.SecondNegativeAxiom);

        #endregion

        private static readonly Axiom[] List = {
            FirstImplicative, SecondImplicative, FirstConjunctive, SecondConjunctive, ThirdConjunctive,
            FirstDisjunctive, SecondDisjunctive, ThirdDisjunctive, FirstNegative, SecondNegative
        };

        public static bool Contains(Formula formula)
        {
            var res = FindAppropriateAxiom(formula);

            return res != null;
        }

        public static Axiom FindAppropriateAxiom(Formula formula)
        {
            foreach (var a in List)
                if ((new FormulaMatcher()).Match(formula, a))
                    return a;

            return null;
        }

        public static Axiom[] GetAxioms()
        {
            return List;
        }
    }
}
