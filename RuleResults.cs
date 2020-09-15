using System.Collections.Generic;
using System.Linq;

namespace HDO.Framework.RulesEngine
{
    /// <summary>
    /// The resulting <see cref="RuleResult"/> list.
    /// </summary>
    public class RuleResults : List<RuleResult>
    {
        /// <summary>
        /// Get the number of failed rules.
        /// </summary>
        public int Failed
        {
            get
            {
                return this.Count(r => !r.Applied);
            }
        }

        /// <summary>
        ///  Gets the number of passed rules with success.
        /// </summary>
        public int Passed
        {
            get
            {
                return this.Count(r => r.Applied);
            }
        }
    }
}
