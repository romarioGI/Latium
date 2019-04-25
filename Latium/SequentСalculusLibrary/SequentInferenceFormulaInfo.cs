using System;

namespace SequentСalculusLibrary
{
    internal struct SequentInferenceFormulaInfo
    {
        public readonly SequentInferenceRule InferenceRule;
        public readonly int Number;

        public SequentFormula Formula
        {
            get { return InferenceRule.Result; }
        }

        public SequentInferenceFormulaInfo(SequentInferenceRule inferenceRule, int number)
        {
            InferenceRule = inferenceRule;
            Number = number;
        }

        public override string ToString()
        {
            return Formula + "; " + InferenceRule;
        }
    }
}
