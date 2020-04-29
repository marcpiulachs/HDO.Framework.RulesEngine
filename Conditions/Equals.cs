namespace HDO.Framework.RulesEngine
{
    public class Equals<T> : ICondition
    {
        private readonly T _actual;
        private readonly T _threshold;

        public Equals(T threshold, T actual)
        {
            _threshold = threshold;
            _actual = actual;
        }

        public bool IsSatisfied()
        {
            return _actual.Equals(_threshold);
        }
    }
}