using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropositionalCalculusLibrary;
using PropositionLibrary;

namespace PropositionalCalculusLibraryTests
{
    [TestClass]
    public class AxiomsTests
    {
        [TestMethod]
        public void AxiomsAreCorrectFormulas()
        {
            foreach (var axiom in Axioms.GetAxioms())
            {
                Assert.IsTrue(axiom != null);
            }
        }

        [TestMethod]
        public void FindAppropriateAxiomForAxiomIsSame()
        {

            foreach (var axiom in Axioms.GetAxioms())
            {
                var appropriateAxiom = Axioms.FindAppropriateAxiom(axiom);
                Assert.IsTrue(axiom.Equals(appropriateAxiom));
            }

        }

        [TestMethod]
        public void NotAxiom1()
        {
            var formula = new Formula("(A" + Implication.Symbol + "(B" + Implication.Symbol + "C))");

            Assert.IsFalse(Axioms.Contains(formula));
        }

        [TestMethod]
        public void IsAxiom1()
        {
            var formula = new Formula("(A" + Implication.Symbol + "((A" + Implication.Symbol +"A)"+ Implication.Symbol + "A))");

            Assert.IsTrue(Axioms.Contains(formula));
        }

        //тестики: подаем аксимоу и узнаем аксиома ли это
    }
}
