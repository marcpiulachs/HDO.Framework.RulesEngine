using System.Collections.Generic;

namespace HDO.Framework.RulesEngine
{
    public interface IRule<T>
    {
        void ClearConditions();
        void Initialize(T obj);
        bool IsValid();
        IEnumerable<RuleResult> Apply(T obj);
        int Priority { get; }
        bool ShouldEnd { get; }
    }
}