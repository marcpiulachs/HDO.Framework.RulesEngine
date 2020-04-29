namespace HDO.Framework.RulesEngine
{
    public class ContainsString : ICondition
    {
        private readonly string _actual;
        private readonly string _threshold;

        public ContainsString(string threshold, string actual)
        {
            _threshold = threshold;
            _actual = actual;
        }

        public bool IsSatisfied()
        {
            return _actual.Contains(_threshold);
        }
    }
}