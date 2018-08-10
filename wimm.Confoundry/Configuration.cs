using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using wimm.Confoundry.DontBelongs;
using wimm.Confoundry.Exceptions;

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
        /// Initializes a new <see cref="Configuration{T}"/>
        /// </summary>
        /// <param name="name">The name of the configuration.</param>
        /// <param name="value">The value of the configuration as a string.</param>
        /// <exception cref="InvalidOperationException">
        /// <typeparamref name="T"/> can not be instantiated from a <see cref="string"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="name"/> is white space.
        /// </exception>
        /// <exception cref="ConfigurationValueFormatException">
        /// <paramref name="value"/> can not be used to initialize an instance of <typeparamref name="T"/>.
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

            if (value == null)
                throw new ArgumentNullException(nameof(value));

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

            throw new ConfigurationValueFormatException(name, value, new AggregateException(errors));
        }

        private IList<Func<string, T>> GetMapFunctions() => new[] { StringConstructor(), ParseMethod() }
            .Where(m => m != Maybe<Func<string, T>>.None).Select(m => m.Value).ToList();

        private Maybe<Func<string, T>> StringConstructor()
        {
            var constructor = typeof(T).GetConstructor(new[] { typeof(string) });
            return constructor == null
                ? Maybe<Func<string, T>>.None
                : new Func<string, T>(s => (T)constructor.Invoke(new object[] { s }));
        }

        private Maybe<Func<string, T>> ParseMethod()
        {
            var parseMethod = typeof(T).GetMethod("Parse", BindingFlags.Public | BindingFlags.Static, null,
                new[] { typeof(string) }, null);

            if (parseMethod == null || parseMethod.ReturnType != typeof(T))
            {
                return Maybe<Func<string, T>>.None;
            }

            return new Func<string, T>(s => (T)parseMethod.Invoke(null, new object[] { s }));
        }
    }
}

