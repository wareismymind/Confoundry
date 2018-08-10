using System;

namespace wimm.Confoundry.Exceptions
{
    /// <summary>
    /// Thrown when the value given for a <see cref="Configuration{T}"/> cannot be used to instantiate an instance of
    /// the configuration's value type.
    /// </summary>
    public class ConfigurationValueFormatException : Exception
    {
        /// <summary>
        /// The name of the <see cref="Configuration{T}"/>.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The value that was given.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Initializes a new <see cref="ConfigurationValueFormatException"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="Configuration{T}"/> that was being constructed.</param>
        /// <param name="value">The value that was given.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="name"/> is white space.
        /// </exception>
        public ConfigurationValueFormatException(string name, string value)
            : base($"Invalid value format for Configuration<T> '{name}'")
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentOutOfRangeException(nameof(name),
                    "Must contain at least one non-whitespace character");

            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Initializes a new <see cref="ConfigurationValueFormatException"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="Configuration{T}"/> that was being constructed.</param>
        /// <param name="value">The value that was given.</param>
        /// <param name="innerException">The exception that occured while attempting to initialize the value.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name"/>, <paramref name="value"/>, or <paramref name="innerException"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="name"/> is white space.
        /// </exception>
        public ConfigurationValueFormatException(string name, string value, Exception innerException)
            : base($"Invalid value format for Configuration<T> '{name}'", 
                  innerException ?? throw new ArgumentNullException(nameof(innerException)))
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentOutOfRangeException(nameof(name),
                    "Must contain at least one non-whitespace character");

            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
