using System.Collections;
using System.Collections.Generic;
using System.Text;
using PropositionLibrary;

namespace SequentСalculusLibrary
{
    internal static class SequentFormulaHashCoder
    {
        private const int Mod = 1997;

        public static int CalcHashCode(SequentFormula formula)
        {
            var ret = 0;
            ret += formula.Consequent.GetHashCode();
            foreach (var f in formula)
            {
                ret *= Mod;
                ret += f.GetHashCode();
            }

            return ret;
        }
    }

    public class SequentFormula : IEnumerable<Formula>
    {
        public const char Turnstile = '⊢';

        // для начала забудем про правила перестановки и сокращения
        private readonly HashSet<Formula> _antecedents;
        public Formula Consequent { get; }

        private readonly int _hashcode;

        public SequentFormula(IEnumerable<Formula> antecedent, Formula consequent)
        {
            _antecedents = new HashSet<Formula>();
            foreach (var f in antecedent)
                _antecedents.Add(f);
            Consequent = consequent;
            _hashcode = SequentFormulaHashCoder.CalcHashCode(this);
        }

        public bool ContainsAntecedent(Formula formula)
        {
            return _antecedents.Contains(formula);
        }

        public override int GetHashCode()
        {
            return _hashcode;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Formula> GetEnumerator()
        {
            return _antecedents.GetEnumerator();
        }

        public override string ToString()
        {
            var ret = new StringBuilder();
            foreach (var f in _antecedents)
            {
                ret.Append(f);
                ret.Append(",");
            }

            ret[ret.Length - 1] = Turnstile;
            ret.Append(Consequent);

            return ret.ToString();
        }

        public static bool Equals(SequentFormula first, SequentFormula second)
        {
            if (first == null && second == null)
                return true;

            if (first == null || second == null)
                return false;

            if (first.GetHashCode() != second.GetHashCode())
                return false;

            if (Equals(first.Consequent, second.Consequent) == false)
                return false;

            if (first._antecedents.Count != second._antecedents.Count)
                return false;

            foreach (var f in first)
                if (second.ContainsAntecedent(f) == false)
                    return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var other = obj as SequentFormula;

            return Equals(this, other);
        }
    }
}
