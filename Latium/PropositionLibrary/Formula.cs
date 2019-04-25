using System;
using System.Collections.Generic;
using System.Linq;

namespace PropositionLibrary
{
    public class Formula
    {
        private string _str;

        private readonly FormulaRpn _rpn;

        public PropositionalConnective TopConnective
        {
            get { return _rpn.Connective; }
        }

        public Formula GetOperand(int index)
        {
            return new Formula(_rpn.GetOperand(index));
        }

        public static bool IsFormula(string str)
        {
            try
            {
                var _ = new Formula(str);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public Formula(string str)
        {
            _rpn = new FormulaRpn(str);
            _str = str;
        }

        protected Formula(Formula formula)
        {
            _rpn = formula._rpn;
            _str = formula._str;
        }

        private Formula(FormulaRpn formulaRpn)
        {
            _rpn = formulaRpn;
            _str = "";
        }

        public override int GetHashCode()
        {
            return _rpn.GetHashCode();
        }

        public static bool Equals(Formula first, Formula second)
        {
            if (first == null && second == null)
                return true;

            if (first == null || second == null)
                return false;

            if (first.GetHashCode() != second.GetHashCode())
                return false;

            return first._rpn.Equals(second._rpn);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var other = obj as Formula;

            return Equals(this, other);
        }

        public override string ToString()
        {
            if (_str == "")
                _str = _rpn.ToString();
            return _str;
        }

        public static explicit operator PropositionalVariable(Formula formula)
        {
            if (formula.TopConnective == null)
                return formula._rpn[0] as PropositionalVariable;
            throw new ArgumentException();
        }

        public Formula SubstituteInFormula(Dictionary<PropositionalVariable, Formula> substitutions)
        {
            var sub = substitutions.ToDictionary(x => x.Key, x => x.Value._rpn);
            return new Formula(_rpn.SubstituteInFormula(sub));
        }
    }
}