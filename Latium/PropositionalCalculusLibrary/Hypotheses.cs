using System.Collections;
using System.Collections.Generic;
using PropositionLibrary;

namespace PropositionalCalculusLibrary
{
    public class Hypotheses : IEnumerable<Formula>
    {
        private readonly HashSet<Formula> _hypotheses;

        public Hypotheses()
        {
            _hypotheses = new HashSet<Formula>();
        }

        public Hypotheses(IEnumerable<Formula> formulas)
        {
            _hypotheses = new HashSet<Formula>(formulas);
        }

        public bool Contains(Formula formula)
        {
            return _hypotheses.Contains(formula);
        }

        public void Union(Hypotheses other)
        {
            _hypotheses.UnionWith(other._hypotheses);
        }

        public static Hypotheses Union(Hypotheses first, Hypotheses second)
        {
            var ret = new Hypotheses();
            ret.Union(first);
            ret.Union(second);

            return ret;
        }

        public IEnumerator<Formula> GetEnumerator()
        {
            return _hypotheses.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
