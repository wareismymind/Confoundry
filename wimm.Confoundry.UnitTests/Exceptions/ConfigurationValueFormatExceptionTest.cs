using System;
using wimm.Confoundry.Exceptions;
using Xunit;

namespace wimm.Confoundry.UnitTests.Exceptions
{
    public class ConfigurationValueFormatExceptionTest
    {
        private const string _name = "Configuration Name";
        private const string _value = "Configuration Value";

        [Fact]
        public void Construct_NullName_Throws()
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
        public void Construct_WhiteSpaceName_Throws(string whiteSpace)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var _ = new ConfigurationValueFormatException(whiteSpace, _value);
            });
        }

        [Fact]
        public void Construct_NullValue_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new ConfigurationValueFormatException(_name, null);
            });
        }

        [Fact]
        public void Construct_ValidArgs_Constructs()
        {
            var _ = new ConfigurationValueFormatException(_name, _value);
        }

        [Fact]
        public void Name_Constructed_HasNameArgumentValue()
        {
            var underTest = new ConfigurationValueFormatException(_name, _value);

            var actual = underTest.Name;

            Assert.Equal(_name, actual);
        }

        [Fact]
        public void Value_Constructed_HasValueArgumentValue()
        {
            var underTest = new ConfigurationValueFormatException(_name, _value);

            var actual = underTest.Value;

            Assert.Equal(_value, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        [InlineData("\r\n")]
        public void Construct_WhiteSpaceValue_Constructs(string whiteSpace)
        {
            var _ = new ConfigurationValueFormatException(_name, whiteSpace);
        }
    }
}
