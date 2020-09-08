namespace HDO.Framework.RulesEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BaseRule<T> : IRule<T>
    {
        public readonly int LOW_PRIORITY = 0;
        public readonly int MEDIUM_PRIORITY = 50;
        public readonly int HIGH_PRIORITY = 100;

        public IRuleEngineContext Context { get; set; }

        protected BaseRule()
        {
            Conditions = new List<ICondition>();
        }

        public IList<ICondition> Conditions { get; set; }

        public virtual int Priority
        {
            get { return 0; }
        }

        public virtual bool ShouldEnd
        {
            get { return false; }
        }

        public void ClearConditions()
        {
            Conditions.Clear();
        }

        public bool IsValid()
        {
            return Conditions.All(x => x.IsSatisfied());
        }

        public virtual void Initialize(T obj)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<RuleResult> Apply(T obj)
        {
            throw new NotImplementedException();
        }
    }
}