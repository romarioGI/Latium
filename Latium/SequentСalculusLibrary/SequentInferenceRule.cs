namespace SequentСalculusLibrary
{
    public abstract class SequentInferenceRule
    {
        private SequentFormula _result;

        public SequentFormula Result
        {
            get
            {
                if (_result != null)
                    return _result;
                return _result = Apply();
            }
        }

        public bool IsValid { get; protected set; }

        protected abstract SequentFormula Apply();
    }
}
