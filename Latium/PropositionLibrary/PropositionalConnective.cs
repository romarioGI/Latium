using System;

namespace PropositionLibrary
{
    public abstract class PropositionalConnective
    {
        public static PropositionalConnective GetInstance(char symbol)
        {
            if (Negation.IsThis(symbol))
                return Negation.GetInstance();

            if (Disjunction.IsThis(symbol))
                return Disjunction.GetInstance();

            if (Conjunction.IsThis(symbol))
                return Conjunction.GetInstance();

            if (Implication.IsThis(symbol))
                return Implication.GetInstance();

            throw new ArgumentException();
        }

        public static bool IsThis(char symbol)
        {
            return Negation.IsThis(symbol) || Disjunction.IsThis(symbol) || Conjunction.IsThis(symbol) ||
                   Implication.IsThis(symbol);
        }

        public abstract int OperandsCount { get; }

        public abstract override string ToString();
    }

    public class Negation : PropositionalConnective
    {
        private static readonly Negation Unique = new Negation();

        public const char Symbol = '¬';

        public static Negation GetInstance()
        {
            return Unique;
        }

        public new static PropositionalConnective GetInstance(char c)
        {
            if (IsThis(c))
                return GetInstance();

            throw new ArgumentException();
        }

        private Negation()
        {
        }

        public new static bool IsThis(char symbol)
        {
            return symbol == Symbol;
        }

        public override int OperandsCount
        {
            get { return 1; }
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }
    }

    public class Disjunction : PropositionalConnective
    {
        private static readonly Disjunction Unique = new Disjunction();

        public const char Symbol = '∨';

        public static Disjunction GetInstance()
        {
            return Unique;
        }

        public new static PropositionalConnective GetInstance(char c)
        {
            if (IsThis(c))
                return GetInstance();

            throw new ArgumentException();
        }

        private Disjunction()
        {
        }

        public new static bool IsThis(char symbol)
        {
            return symbol == Symbol;
        }

        public override int OperandsCount
        {
            get { return 2; }
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }
    }

    public class Conjunction : PropositionalConnective
    {
        private static readonly Conjunction Unique = new Conjunction();

        public const char Symbol = '&';

        public static Conjunction GetInstance()
        {
            return Unique;
        }

        public new static PropositionalConnective GetInstance(char c)
        {
            if (IsThis(c))
                return GetInstance();

            throw new ArgumentException();
        }

        private Conjunction()
        {
        }

        public new static bool IsThis(char symbol)
        {
            return symbol == Symbol;
        }

        public override int OperandsCount
        {
            get { return 2; }
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }
    }

    public class Implication : PropositionalConnective
    {
        private static readonly Implication Unique = new Implication();

        public const char Symbol = '→';

        public static Implication GetInstance()
        {
            return Unique;
        }

        public new static PropositionalConnective GetInstance(char c)
        {
            if (IsThis(c))
                return GetInstance();

            throw new ArgumentException();
        }

        private Implication()
        {
        }

        public new static bool IsThis(char symbol)
        {
            return symbol == Symbol;
        }

        public override int OperandsCount
        {
            get { return 2; }
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }
    }
}
