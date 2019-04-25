using System;

namespace PropositionLibrary
{
    public class PropositionalVariable
    {
        protected bool Equals(PropositionalVariable other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((PropositionalVariable) obj);
        }

        public readonly string Name;
        private readonly int _hashCode;

        public static bool operator ==(PropositionalVariable first, PropositionalVariable second)
        {
            if (first is null && second is null)
                return true;
            if (first is null || second is null)
                return false;
            if (first._hashCode != second._hashCode)
                return false;
            return first.Name == second.Name;
        }

        public static bool operator !=(PropositionalVariable first, PropositionalVariable second)
        {
            return !(first == second);
        }

        public static bool IsThis(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            if (char.IsLetter(str[0]) == false)
                return false;

            for (var i = 1; i < str.Length; i++)
                if (char.IsDigit(str[i]) == false)
                    return false;

            return true;
        }

        public PropositionalVariable(string name)
        {
            if (IsThis(name) == false)
                throw new ArgumentException();

            Name = name;
            _hashCode = Name.GetHashCode();
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override string ToString()
        {
            return Name;
        }

        public static implicit operator Formula(PropositionalVariable variable)
        {
            return new Formula(variable.Name);
        }
    }
}
