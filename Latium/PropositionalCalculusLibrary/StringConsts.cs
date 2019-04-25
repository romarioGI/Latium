namespace PropositionalCalculusLibrary
{
    public static class StringConstants
    {
        private static IStringConstants _stringConstants= new RussianStringConstants();

        public static void ChangeLanguage(IStringConstants language)
        {
            _stringConstants = language;
        }

        public static string FirstImplicativeAxiom => _stringConstants.FirstImplicativeAxiom;
        public static string SecondImplicativeAxiom => _stringConstants.SecondImplicativeAxiom;
               
        public static string FirstConjunctiveAxiom => _stringConstants.FirstConjunctiveAxiom;
        public static string SecondConjunctiveAxiom => _stringConstants.SecondConjunctiveAxiom;
        public static string ThirdConjunctiveAxiom => _stringConstants.ThirdConjunctiveAxiom;
               
        public static string FirstDisjunctiveAxiom => _stringConstants.FirstDisjunctiveAxiom;
        public static string SecondDisjunctiveAxiom => _stringConstants.SecondDisjunctiveAxiom;
        public static string ThirdDisjunctiveAxiom => _stringConstants.ThirdDisjunctiveAxiom;
               
        public static string FirstNegativeAxiom => _stringConstants.FirstNegativeAxiom;
        public static string SecondNegativeAxiom => _stringConstants.SecondNegativeAxiom;
               
        public static string IsHypothesis => _stringConstants.IsHypothesis;
        public static string IsMpFormula => _stringConstants.IsMpFormula;
               
        public static string LineEnd => _stringConstants.LineEnd;
    }

    public interface IStringConstants
    {
        string FirstImplicativeAxiom { get; }
        string SecondImplicativeAxiom { get; }

        string FirstConjunctiveAxiom { get; }
        string SecondConjunctiveAxiom { get; }
        string ThirdConjunctiveAxiom { get; }

        string FirstDisjunctiveAxiom { get; }
        string SecondDisjunctiveAxiom { get; }
        string ThirdDisjunctiveAxiom { get; }

        string FirstNegativeAxiom { get; }
        string SecondNegativeAxiom { get; }

        string IsHypothesis { get; }
        string IsMpFormula { get; }

        string LineEnd { get; }
    }

    public class EnglishStringConstants : IStringConstants
    {
        public string FirstImplicativeAxiom => "first implicative axiom";
        public string SecondImplicativeAxiom => "second implicative axiom";

        public string FirstConjunctiveAxiom => "first conjunctive axiom";
        public string SecondConjunctiveAxiom => "second conjunctive axiom";
        public string ThirdConjunctiveAxiom => "third conjunctive axiom";

        public string FirstDisjunctiveAxiom => "first disjunctive axiom";
        public string SecondDisjunctiveAxiom => "second disjunctive axiom";
        public string ThirdDisjunctiveAxiom => "third disjunctive axiom";

        public string FirstNegativeAxiom => "first negative axiom";
        public string SecondNegativeAxiom => "second negative axiom";

        public string IsHypothesis => "; hypothesis";
        public string IsMpFormula => "; MP ";

        public string LineEnd => "\n\r";
    }

    public class RussianStringConstants : IStringConstants
    {
        public string FirstImplicativeAxiom => "первая аксиома импликации";
        public string SecondImplicativeAxiom => "вторая аксиома импликации";

        public string FirstConjunctiveAxiom => "первая аксиома конъюнкции";
        public string SecondConjunctiveAxiom => "вторая аксиома конъюнкции";
        public string ThirdConjunctiveAxiom => "третья аксиома конъюнкции";

        public string FirstDisjunctiveAxiom => "первая аксиома дизъюнкции";
        public string SecondDisjunctiveAxiom => "вторая аксиома дизъюнкции";
        public string ThirdDisjunctiveAxiom => "третья аксиома дизъюнкции";

        public string FirstNegativeAxiom => "первая аксиома отрицания";
        public string SecondNegativeAxiom => "вторая аксиома отрицания";

        public string IsHypothesis => "; гипотеза";
        public string IsMpFormula => "; MP ";

        public string LineEnd => "\n\r";

    }
}
