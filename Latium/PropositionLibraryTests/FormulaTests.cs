using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropositionLibrary;

namespace PropositionLibraryTests
{
    [TestClass]
    public class FormulaTests
    {
        [TestMethod]
        public void EmptyStringIsNotFormula()
        {
            var ok = Formula.IsFormula("");

            Assert.IsFalse(ok);
        }

        [TestMethod]
        public void VariableIsCorrectFormula1()
        {
            var ok = Formula.IsFormula("A");

            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void VariableIsCorrectFormula2()
        {
            var ok = Formula.IsFormula("A1");

            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void VariableIsCorrectFormula3()
        {
            var ok = Formula.IsFormula("B1234");

            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void VariableInBracketsIsNotCorrectFormula()
        {
            var ok = Formula.IsFormula("(A)");

            Assert.IsFalse(ok);
        }

        [TestMethod]
        public void AloneConnectiveIsNotCorrectFormula()
        {
            var formula1 = Negation.Symbol.ToString();
            var formula2 = Disjunction.Symbol.ToString();
            var formula3 = Conjunction.Symbol.ToString();
            var formula4 = Implication.Symbol.ToString();

            Assert.IsFalse(Formula.IsFormula(formula1) || Formula.IsFormula(formula2) || Formula.IsFormula(formula3) ||
                           Formula.IsFormula(formula4));
        }

        [TestMethod]
        public void SimpleTermIsCorrectFormula()
        {
            var terms = new[]
            {
                "(" + Negation.Symbol + "A)", "(A" + Disjunction.Symbol + "B)", "(A" + Conjunction.Symbol + "B)",
                "(A" + Implication.Symbol + "B)"
            };

            foreach (var t in terms)
                Assert.IsTrue(Formula.IsFormula(t));
        }

        [TestMethod]
        public void SimpleTermWithoutBracketsIsNotCorrectFormula()
        {
            var terms = new[]
            {
                Negation.Symbol + "A", "A" + Disjunction.Symbol + "B", "A" + Conjunction.Symbol + "B",
                "A" + Implication.Symbol + "B"
            };

            foreach (var t in terms)
                Assert.IsFalse(Formula.IsFormula(t));

        }

        [TestMethod]
        public void NegationAfterVarIsNotCorrect()
        {
            var formula1 = "A" + Negation.Symbol;
            var formula2 = "(A" + Negation.Symbol + ")";

            Assert.IsFalse(Formula.IsFormula(formula1));
            Assert.IsFalse(Formula.IsFormula(formula2));
        }

        [TestMethod]
        public void ConnectiveAfterVarIsNotCorrect()
        {
            var formula1 = "(A&B)(A&B)" + Conjunction.Symbol;
            var formula2 = "((A&B)(A&B)" + Disjunction.Symbol + ")";

            Assert.IsFalse(Formula.IsFormula(formula1));
            Assert.IsFalse(Formula.IsFormula(formula2));
        }

        [TestMethod]
        public void ConnectiveFrontVarIsNotCorrect()
        {
            var formula1 = Conjunction.Symbol + "(A&B)(A&B)";
            var formula2 = "(" + Disjunction.Symbol + "(A&B)(A&B))";

            Assert.IsFalse(Formula.IsFormula(formula1));
            Assert.IsFalse(Formula.IsFormula(formula2));
        }

        //нужно много общих тестов
    }
}
