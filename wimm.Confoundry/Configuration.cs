using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using wimm.Confoundry.DontBelongs;

namespace wimm.Confoundry
{
    /// <summary>
    /// A configuration setting.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public class Configuration<T>
    {
        /// <summary>
        /// The name of the configuration.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The value of the configuration.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Initializes a new <see cref="T:wimm.Confoundry.Configuration{T}"/>
        /// </summary>
        /// <param name="name">The name of the configuration.</param>
        /// <param name="value">The value of the configuration as a string.</param>
        /// <exception cref="T:System.InvalidOperationException">
        /// <typeparamref name="T"/> can not be instantiated from a <see cref="string"/>.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="name"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="name"/> is whitespace.
        /// </exception>
        /// <exception cref="T:System.FormatException">
        /// 
        /// </exception>
        public Configuration(string name, string value)
        {
            var mapFunctions = GetMapFunctions();
            if (mapFunctions.Count == 0)
            {
                throw new InvalidOperationException(
                    $"No suitable methods found to instantiate a {GetType()} from a string");
            }

            Name = name ?? throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentOutOfRangeException(nameof(name),
                    "Must contain at least one non-whitespace character");

            if (value == null) throw new ArgumentNullException(nameof(value));

            var errors = new List<Exception>();

            foreach (var mapFunction in mapFunctions)
            {
                try
                {
                    Value = mapFunction.Invoke(value);
                    return;
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

            throw new FormatException($"The value of {nameof(value)} could not be mapped to a {nameof(T)}",
                new AggregateException(errors));
        }

        private IList<Func<string, T>> GetMapFunctions() => new[] { StringConstructor(), ParseMethod() }
            .Where(m => m != Maybe<Func<string, T>>.None).Select(m => m.Value).ToList();

        private Maybe<Func<string, T>> StringConstructor() => 
            GetMapMethod(typeof(T).GetConstructor(new[] {typeof(string)}));

        private Maybe<Func<string, T>> ParseMethod() =>
            GetMapMethod(typeof(T).GetMethod("Parse", new[] { typeof(string) }));

        private Maybe<Func<string, T>> GetMapMethod(MethodBase method) =>
            method == null
                ? new Maybe<Func<string, T>>()
                : new Maybe<Func<string, T>>(s => (T)method.Invoke(null, new object[] { s }));
    }
}

