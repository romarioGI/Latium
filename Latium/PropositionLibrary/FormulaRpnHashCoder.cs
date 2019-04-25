namespace PropositionLibrary
{
    internal static class FormulaRpnHashCoder
    {
        public static int CalcHashCode(FormulaRpn rpn)
        {
            var res = 0;
            for (var i = rpn.Length - 1; i >= 0; i--)
            {
                //res *= 1997;
                res += rpn[i].GetHashCode() ^ (-res + i);
            }

            return res;
        }
    }
}
