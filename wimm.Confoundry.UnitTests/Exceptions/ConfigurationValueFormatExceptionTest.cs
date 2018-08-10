using System;
using wimm.Confoundry.Exceptions;
using Xunit;

namespace wimm.Confoundry.UnitTests.Exceptions
{
    public class ConfigurationValueFormatExceptionTest
    {
        private const string _name = "Configuration Name";
        private const string _value = "Configuration Value";
        private static readonly Exception _exception = new Exception();

        [Fact]
        public void ConstructNameValue_NullName_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new ConfigurationValueFormatException(null, _value);
            });
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        [InlineData("\r\n")]
        public void ConstructNameValue_WhiteSpaceName_Throws(string whiteSpace)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var _ = new ConfigurationValueFormatException(whiteSpace, _value);
            });
        }

        [Fact]
        public void ConstructNameValue_NullValue_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new ConfigurationValueFormatException(_name, null);
            });
        }

        [Fact]
        public void ConstructNameValue_ValidArgs_Constructs()
        {
            var _ = new ConfigurationValueFormatException(_name, _value);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        [InlineData("\r\n")]
        public void ConstructNameValue_WhiteSpaceValue_Constructs(string whiteSpace)
        {
            var _ = new ConfigurationValueFormatException(_name, whiteSpace);
        }

        [Fact]
        public void Name_ConstructNameValue_HasNameArgumentValue()
        {
            var underTest = new ConfigurationValueFormatException(_name, _value);

            var actual = underTest.Name;

            Assert.Equal(_name, actual);
        }

        [Fact]
        public void Value_ConstructNameValue_HasValueArgumentValue()
        {
            var underTest = new ConfigurationValueFormatException(_name, _value);

            var actual = underTest.Value;

            Assert.Equal(_value, actual);
        }





        [Fact]
        public void ConstructNameValueException_NullName_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new ConfigurationValueFormatException(null, _value, _exception);
            });
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        [InlineData("\r\n")]
        public void ConstructNameValueException_WhiteSpaceName_Throws(string whiteSpace)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var _ = new ConfigurationValueFormatException(whiteSpace, _value, _exception);
            });
        }

        [Fact]
        public void ConstructNameValueException_NullValue_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new ConfigurationValueFormatException(_name, null, _exception);
            });
        }

        [Fact]
        public void ConstructNameValueException_ValidArgs_Constructs()
        {
            var _ = new ConfigurationValueFormatException(_name, _value, _exception);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        [InlineData("\r\n")]
        public void ConstructNameValueException_WhiteSpaceValue_Constructs(string whiteSpace)
        {
            var _ = new ConfigurationValueFormatException(_name, whiteSpace, _exception);
        }

        [Fact]
        public void ConstructNameValueException_NullException_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new ConfigurationValueFormatException(_name, _value, null);
            });
        }

        [Fact]
        public void Name_ConstructNameValueException_HasNameArgumentValue()
        {
            var underTest = new ConfigurationValueFormatException(_name, _value, _exception);

            var actual = underTest.Name;

            Assert.Equal(_name, actual);
        }

        [Fact]
        public void Value_ConstructNameValueException_HasValueArgumentValue()
        {
            var underTest = new ConfigurationValueFormatException(_name, _value, _exception);

            var actual = underTest.Value;

            Assert.Equal(_value, actual);
        }

        [Fact]
        public void InnerException_ConstructNameValueException_HasInnerExceptionArgumentValue()
        {
            var underTest = new ConfigurationValueFormatException(_name, _value, _exception);

            var actual = underTest.InnerException;

            Assert.Same(_exception, actual);
        }
    }
}
