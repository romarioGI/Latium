namespace SequentСalculusLibrary
{
    public static class StringConstants
    {
        private static IStringConstants _stringConstants = new RussianStringConstants();

        public static void ChangeLanguage(IStringConstants language)
        {
            _stringConstants = language;
        }

        public static string ConjunctionIntroduction => _stringConstants.ConjunctionIntroduction;

        public static string ConjunctionRemoving => _stringConstants.ConjunctionRemoving;

        public static string LineEnd => _stringConstants.LineEnd;
    }

    public interface IStringConstants
    {
        string ConjunctionIntroduction { get; }
        string ConjunctionRemoving { get; }


        string LineEnd { get; }
    }

    public class EnglishStringConstants : IStringConstants
    {
        public string ConjunctionIntroduction => "Conjunction introduction";
        public string ConjunctionRemoving => "Conjunction removing";

        public string LineEnd => "\r\n";
    }

    public class RussianStringConstants : IStringConstants
    {
        public string ConjunctionIntroduction => "Введение конъюнкции";
        public string ConjunctionRemoving => "Удалению конъюнкции";

        public string LineEnd => "\r\n";
    }
}
