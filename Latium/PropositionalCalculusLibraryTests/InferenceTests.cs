using Microsoft.VisualStudio.TestTools.UnitTesting;
using PropositionalCalculusLibrary;
using PropositionLibrary;

namespace PropositionalCalculusLibraryTests
{
    [TestClass]
    public class InferenceTests
    {
        [TestMethod]
        public void MinimizeRemovesUnusedStepsAndPreservesMpReferences()
        {
            var unused = new Formula("C");
            var premise = new Formula("A");
            var implication = new Formula("(A→B)");
            var conclusion = new Formula("B");
            var inference = new Inference(new Hypotheses(new[] {unused, premise, implication}));

            Assert.IsTrue(inference.Push(unused));
            Assert.IsTrue(inference.Push(premise));
            Assert.IsTrue(inference.Push(implication));
            Assert.IsTrue(inference.Push(conclusion));

            inference.Minimize();

            Assert.AreEqual(3, inference.Length);
            Assert.AreEqual(conclusion, inference.LastFormula);
            Assert.IsFalse(inference.Contains(unused));
            var expected = string.Join(StringConstants.LineEnd, new[]
            {
                "0. A" + StringConstants.IsHypothesis,
                "1. (A→B)" + StringConstants.IsHypothesis,
                "2. B" + StringConstants.IsMpFormula + "0, 1"
            });
            Assert.AreEqual(expected, inference.ToString());
        }

        [TestMethod]
        public void PushAfterMinimizeUsesConsecutiveIndices()
        {
            var first = new Formula("A");
            var second = new Formula("B");
            var inference = new Inference(new Hypotheses(new[] {first, second}));

            Assert.IsTrue(inference.Push(first));
            Assert.IsTrue(inference.Push(second));
            inference.Minimize();

            Assert.IsTrue(inference.Push(first));

            Assert.AreEqual(2, inference.Length);
            Assert.AreEqual(first, inference.LastFormula);
            var expected = string.Join(StringConstants.LineEnd, new[]
            {
                "0. B" + StringConstants.IsHypothesis,
                "1. A" + StringConstants.IsHypothesis
            });
            Assert.AreEqual(expected, inference.ToString());
        }
    }
}
