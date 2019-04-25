using PropositionLibrary;

namespace PropositionalCalculusLibrary
{
    internal class MpFormula : Formula
    {
        public readonly Formula A;
        public readonly Formula B;

        private MpFormula(Formula formula, Formula a, Formula b) : base(formula)
        {
            A = a;
            B = b;
        }

        /// <summary>
        /// if a,b isn't MP pair then return null
        /// </summary>
        public static MpFormula GetMpFormula(Formula a, Formula b)
        {
            if (IsMpPair(a, b))
                return new MpFormula(b.GetOperand(1), a, b);

            return null;
        }

        public static bool IsMpPair(Formula a, Formula b)
        {
            if (b.TopConnective is Implication == false)
                return false;
            return Equals(a, b.GetOperand(0));
        }
    }
}
