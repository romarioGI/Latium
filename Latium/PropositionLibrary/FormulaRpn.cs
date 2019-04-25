using System;
using System.Collections.Generic;
using System.Text;

namespace PropositionLibrary
{
    internal class FormulaRpn
    {
        private readonly object[] _rpn;

        private readonly int _hashCode;

        public PropositionalConnective Connective
        {
            get { return _rpn[Length - 1] as PropositionalConnective; }
        }

        public FormulaRpn GetOperand(int index)
        {
            if (Connective == null)
                throw new ArgumentException();

            if (index >= Connective.OperandsCount || index < 0)
                throw new ArgumentOutOfRangeException();

            return _rpn[index] as FormulaRpn;
        }

        public int Length
        {
            get { return _rpn.Length; }
        }

        public object this[int index]
        {
            get
            {
                if (index >= _rpn.Length || index < 0)
                    throw new ArgumentOutOfRangeException();

                return _rpn[index];
            }
        }

        private static void AddVariableToResult(StringBuilder variableSb, List<object> result)
        {
            if (variableSb.Length == 0)
                return;

            var variable = new PropositionalVariable(variableSb.ToString());
            result.Add(variable);
            variableSb.Clear();
        }

        private static List<object> Tokenize(string formula)
        {
            var result = new List<object>();
            var variableSb = new StringBuilder();
            foreach (var c in formula)
            {
                if (c == '(' || c == ')')
                {
                    AddVariableToResult(variableSb, result);
                    result.Add(c);
                    continue;
                }

                if (PropositionalConnective.IsThis(c))
                {
                    AddVariableToResult(variableSb, result);
                    result.Add(PropositionalConnective.GetInstance(c));
                    continue;
                }

                variableSb.Append(c);
            }

            AddVariableToResult(variableSb, result);

            return result;
        }

        private static void CheckTokensOrder(List<object> tokens)
        {
            for (var i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] is Negation)
                {
                    var left = (char) tokens[i - 1] == '(';
                    var right = tokens[i + 1] is PropositionalVariable || (char) tokens[i + 1] == '(';
                    if (left == false || right == false)
                        throw new ArgumentException();
                    continue;
                }

                if (tokens[i] is PropositionalConnective)
                {
                    var left = tokens[i - 1] is PropositionalVariable || (char) tokens[i - 1] == ')';
                    var right = tokens[i + 1] is PropositionalVariable || (char) tokens[i + 1] == '(';
                    if (left == false || right == false)
                        throw new ArgumentException();
                }
            }
        }

        private static void AddCharToRpn(char c, Stack<object> stack, List<object> result)
        {
            switch (c)
            {
                case '(':
                    stack.Push(c);
                    break;
                case ')':
                    if (stack.Count < 2)
                        throw new ArgumentException();
                    var top = stack.Pop();
                    if (top is PropositionalConnective)
                    {
                        result.Add(top);
                        top = stack.Pop();
                        if ((char) top != '(')
                            throw new ArgumentException();
                    }
                    else
                        throw new ArgumentException();

                    break;
                default:
                    throw new ArgumentException();
            }
        }

        private static List<object> TokensToRpn(List<object> tokens)
        {
            var stack = new Stack<object>();
            var result = new List<object>();

            foreach (var token in tokens)
            {
                switch (token)
                {
                    case char c:
                        AddCharToRpn(c, stack, result);
                        break;
                    case PropositionalConnective pc:
                        stack.Push(pc);
                        break;
                    case PropositionalVariable v:
                        result.Add(v);
                        break;
                    default:
                        throw new ArgumentException();
                }
            }

            if (stack.Count != 0)
                throw new ArgumentException();

            return result;
        }

        private static object[] MinimizeRpn(List<object> rpn)
        {
            var stack = new Stack<object>();
            foreach (var token in rpn)
            {
                switch (token)
                {
                    case PropositionalVariable _:
                        var varRpn = new[] {token};
                        stack.Push(new FormulaRpn(varRpn));
                        break;
                    case PropositionalConnective c when stack.Count < c.OperandsCount:
                        throw new ArgumentException();
                    case PropositionalConnective c:
                        var smallRpn = new object[c.OperandsCount + 1];
                        smallRpn[c.OperandsCount] = c;
                        for (var i = c.OperandsCount - 1; i >= 0; i--)
                            smallRpn[i] = stack.Pop();
                        stack.Push(new FormulaRpn(smallRpn));
                        break;
                    default:
                        throw new ArgumentException();
                }
            }

            if (stack.Count != 1)
                throw new ArgumentException();

            var f = stack.Pop();

            if (f is FormulaRpn formula)
                return formula._rpn;
            return new[] {f};
        }

        public FormulaRpn(string formula)
        {
            var tokens = Tokenize(formula);
            CheckTokensOrder(tokens);
            var rpn = TokensToRpn(tokens);
            _rpn = MinimizeRpn(rpn);
            _hashCode = FormulaRpnHashCoder.CalcHashCode(this);
        }

        private FormulaRpn(object[] rpn)
        {
            _rpn = rpn;
            _hashCode = FormulaRpnHashCoder.CalcHashCode(this);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is FormulaRpn == false)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var formula = (FormulaRpn) obj;

            var firstBack = this[Length - 1];
            var secondBack = formula[formula.Length - 1];

            switch (firstBack)
            {
                case PropositionalConnective firstConnective:
                {
                    if (!(secondBack is PropositionalConnective secondConnective))
                        return false;

                    if (firstConnective != secondConnective)
                        return false;

                    var res = true;
                    for (var i = 0; i < firstConnective.OperandsCount; i++)
                        res &= this[i].Equals(formula[i]);

                    return res;
                }
                case PropositionalVariable firstVariable
                    when secondBack is PropositionalVariable secondVariable:
                    return firstVariable == secondVariable;
                case PropositionalVariable _:
                    return false;
                default:
                    throw new NotImplementedException();
            }
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override string ToString()
        {
            if (Connective == null)
                return _rpn[0].ToString();

            if (Connective is Negation)
                return "(" + Connective + GetOperand(0) + ")";

            if (Connective.OperandsCount == 2)
                return "(" + GetOperand(0) + Connective + GetOperand(1) + ")";

            throw new NotImplementedException();
        }

        public FormulaRpn SubstituteInFormula(Dictionary<PropositionalVariable, FormulaRpn> substitutions)
        {
            return Substitute(substitutions);
        }

        private FormulaRpn Substitute(Dictionary<PropositionalVariable, FormulaRpn> substitutions)
        {
            if (Connective == null)
            {
                var v = (PropositionalVariable) _rpn[0];
                return substitutions[v];
            }

            var retRpn = new object[Length];
            for (var i = 0; i < Connective.OperandsCount; i++)
                retRpn[i] = GetOperand(i).Substitute(substitutions);

            retRpn[Length - 1] = Connective;

            return new FormulaRpn(retRpn);
        }
    }
}
