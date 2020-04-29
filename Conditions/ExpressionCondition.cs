namespace HDO.Framework.RulesEngine
{
    using System;

    public class ExpressionCondition<T> : ICondition
    {
        private Func<T, bool> action;
        private T value;

        public ExpressionCondition(T value, Func<T, bool> action)
        {
            this.value = value;
            this.action = action;
        }

        public bool IsSatisfied()
        {
            return action(value);
        }
    }
}