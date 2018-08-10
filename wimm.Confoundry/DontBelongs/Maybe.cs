using System;

namespace wimm.Confoundry.DontBelongs
{
    internal struct Maybe<T> : IEquatable<Maybe<T>>
    {
        private readonly bool _hasValue;
        private readonly T _value;

        public static Maybe<T> None { get; }

        public T Value
        {
            get
            {
                if (!_hasValue)
                {
                    throw new InvalidOperationException($"This {GetType()} has no value");
                }

                return _value;
            }
        }

        public Maybe(T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            _value = value;
            _hasValue = true;
        }

        public override bool Equals(object obj) => obj is Maybe<T> && Equals((Maybe<T>)obj);

        public bool Equals(Maybe<T> other)
        {
            if (_hasValue == false && other._hasValue == false) return true;
            if (_hasValue == false || other._hasValue == false) return false;
            return Value.Equals(other.Value);
        }

        public override int GetHashCode() => (_hasValue ? Value : (this as object)).GetHashCode();

        public static bool operator ==(Maybe<T> maybe1, Maybe<T> maybe2) => maybe1.Equals(maybe2);

        public static bool operator !=(Maybe<T> maybe1, Maybe<T> maybe2) => !(maybe1 == maybe2);
    }
}
