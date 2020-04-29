﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDO.Framework.RulesEngine
{
    /// <summary>
    /// Returns the result of an applied business rule by the <see cref="RulesEngine{T}"/>
    /// </summary>
    public class RuleResult
    {
        /// <summary>
        /// Returns true if the rule was successfully applied.
        /// </summary>
        public bool Applied { get; private set; }

        /// <summary>
        /// The message generated by the <see cref="RuleResult"/>.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// The name of the Rule
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="RuleResult"/> class.
        /// </summary>
        /// <param name="result">True if the rule was a success, False if it failed to be applied.</param>
        /// <param name="message">The result friendly message.</param>
        public RuleResult(bool result, string message = null)
        {
            Applied = result;
            Message = message;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="RuleResult"/> class.
        /// </summary>
        /// <param name="result">True if the rule was a success, False if it failed to be applied.</param>
        /// <param name="name">The name of the rule.</param>
        /// <param name="message">The result friendly message.</param>
        public RuleResult(bool result, string name, string message)
        {
            Applied = result;
            Message = message;
            Name = name;
        }

        /// <summary>
        /// Creates and gets a success result.
        /// </summary>
        /// <returns></returns>
        public static RuleResult Success()
        {
            return new RuleResult(true);
        }

        /// <summary>
        /// Creates and gets a fail result.
        /// </summary>
        /// <returns></returns>
        public static RuleResult Fail()
        {
            return new RuleResult(false);
        }

        /// <summary>
        /// Sets a result message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public RuleResult WithMessage(string message)
        {
            return new RuleResult(Applied, message);
        }

        /// <summary>
        /// Sets a result message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public RuleResult WithMessage(string message, params object[] args)
        {
            return new RuleResult(Applied, string.Format(message, args));
        }
    }
}
