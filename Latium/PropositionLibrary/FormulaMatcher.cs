using System.Collections.Generic;

namespace PropositionLibrary
{
    public class FormulaMatcher
    {
        private readonly Dictionary<PropositionalVariable, Formula> _variableSubstitution;

        public FormulaMatcher()
        {
            _variableSubstitution = new Dictionary<PropositionalVariable, Formula>();
        }

        public bool Match(Formula formula, Formula pattern)
        {
            if (formula == null || pattern == null)
                return false;

            _variableSubstitution.Clear();
            return Solve(formula, pattern);
        }

        private bool Solve(Formula formula, Formula pattern)
        {
            if (pattern.TopConnective == null)
            {
                var patternVariable = (PropositionalVariable) pattern;
                if (_variableSubstitution.ContainsKey(patternVariable))
                    return _variableSubstitution[patternVariable].Equals(formula);

                _variableSubstitution[patternVariable] = formula;
                return true;
            }

            if (formula.TopConnective == null)
                return false;

            if (formula.TopConnective != pattern.TopConnective)
                return false;

            for (var i = 0; i < formula.TopConnective.OperandsCount; i++)
                if (Solve(formula.GetOperand(i), pattern.GetOperand(i)) == false)
                    return false;

            return true;
        }
    }
}
