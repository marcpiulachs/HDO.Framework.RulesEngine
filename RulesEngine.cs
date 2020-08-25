namespace HDO.Framework.RulesEngine
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Provides the base class for a RuleEngine implementation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RulesEngine<T> where T : class
    {
        /// <summary>
        /// Contains the list of rules to be applied.
        /// </summary>
        private List<IRule<T>> rules = new List<IRule<T>>();

        public IRuleEngineContext Context { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="RulesEngine"/> class.
        /// </summary>
        public RulesEngine()
        {
            //LoadRules();
        }

        /// <summary>
        /// Loads the Rules.
        /// </summary>
        protected virtual void LoadRules()
        {

        }

        protected void LoadRulesFor<E>() where E : IRule<T>
        {
            var rules = LoadPlugins<E>();

            foreach (E rule in rules)
            {
                AddRule(rule);
            }
        }

        public List<E> LoadPlugins<E>()
        {
            return LoadPlugins<E>(Directory.GetCurrentDirectory());
        }

        public List<E> LoadPlugins<E>(string path)
        {
            string[] dllFileNames = null;

            if (Directory.Exists(path))
            {
                dllFileNames = Directory.GetFiles(path, "*Rules.dll");

                ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
                foreach (string dllFile in dllFileNames)
                {
                    AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                    Assembly assembly = Assembly.Load(an);
                    assemblies.Add(assembly);
                }

                Type pluginType = typeof(IRule<T>);
                ICollection<Type> pluginTypes = new List<Type>();
                foreach (Assembly assembly in assemblies)
                {
                    if (assembly != null)
                    {
                        Type[] types = assembly.GetTypes();

                        foreach (Type type in types)
                        {
                            if (type.IsInterface || type.IsAbstract)
                            {
                                continue;
                            }
                            else
                            {
                                if (type.IsSubclassOf(typeof(E)))
                                {
                                    pluginTypes.Add(type);
                                }
                            }
                        }
                    }
                }

                List<E> plugins = new List<E>(pluginTypes.Count);
                foreach (Type type in pluginTypes)
                {
                    var plugin = (E)Activator.CreateInstance(type);
                    plugins.Add(plugin);

                    Console.WriteLine(string.Format("RuleEngine plugin {0} loaded from {1}", type.Name, type.Assembly.GetName().Name));
                }

                return plugins;
            }

            return null;
        }

        public void AddRule(IRule<T> rule)
        {
            // Set the current context
            rule.Context = Context;

            // Add the rule to the list
            rules.Add(rule);
        }

        public List<RuleResult> Run(T obj)
        {
            List<RuleResult> results = new List<RuleResult>();

            foreach (IRule<T> rule in rules.OrderByDescending(r => r.Priority))
            {
                rule.ClearConditions();
                rule.Initialize(obj);

                if (rule.IsValid())
                {
                    var ruleResults = rule.Apply(obj);

                    results.AddRange(ruleResults);

                    if (rule.ShouldEnd)
                        return results;
                }
            }

            return results;
        }
    }
}