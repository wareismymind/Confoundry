using System;
using wimm.Confoundry.Exceptions;
using Xunit;

namespace wimm.Confoundry.UnitTests
{
    public class ConfigurationTest
    {
        private const string _valueName = "Configuration Value";

        private const string _intValueString = "42";
        private const string _intInvalidValue = "forty-two";

        private const string _uriValueString = "https://github.com/wareismymind/Confoundry";
        private const string _uriInvalidValue = "42";

        [Fact]
        public void Construct_InvalidType_Throws()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                var _ = new Configuration<object>(_valueName, _intValueString);
            });
        }

        // The argument validation tests depend on the fact that the type is not invalid. The type int is valid because
        // it has a static Parse(string) method that returns an int.

        [Fact]
        public void Construct_NullName_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new Configuration<int>(null, _intValueString);
            });

            Assert.Equal("name", ex.ParamName);
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
                var _ = new Configuration<int>(whiteSpace, _intValueString);
            });

            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void Construct_NullValue_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new Configuration<int>(_valueName, null);
            });

            Assert.Equal("value", ex.ParamName);
        }

        [Fact]
        public void Construct_InvalidValueParseType_Throws()
        {
            var ex = Assert.Throws<ConfigurationValueFormatException>(() =>
            {
                var _ = new Configuration<int>(_valueName, _intInvalidValue);
            });

            Assert.Equal(_valueName, ex.Name);
            Assert.Equal(_intInvalidValue, ex.Value);
        }

        [Fact]
        public void Construct_InvalidValueConstructType_Throws()
        {
            var ex = Assert.Throws<ConfigurationValueFormatException>(() =>
            {
                var _ = new Configuration<Uri>(_valueName, _uriInvalidValue);
            });

            Assert.Equal(_valueName, ex.Name);
            Assert.Equal(_uriInvalidValue, ex.Value);
        }

        [Fact]
        public void Construct_ValidArgsParseType_Constructs()
        {
            var _ = new Configuration<int>(_valueName, _intValueString);
        }

        [Fact]
        public void Construct_ValidArgsStringConstructorType_Constructs()
        {
            var _ = new Configuration<Uri>(_valueName, _uriValueString);
        }

        [Fact]
        public void Name_Constructed_HasNameArgumentValue()
        {
            var underTest = new Configuration<int>(_valueName, _intValueString);

            var actual = underTest.Name;

            Assert.Equal(_valueName, actual);
        }

        [Fact]
        public void Value_ConstructedParseType_EqualsParseResult()
        {
            var underTest = new Configuration<int>(_valueName, _intValueString);

            var actual = underTest.Value;

            Assert.Equal(int.Parse(_intValueString), actual);
        }

        [Fact]
        public void Value_ConstructedStringConstructorType_EqualsConstructed()
        {
            var underTest = new Configuration<Uri>(_valueName, _uriValueString);

            var actual = underTest.Value;

            Assert.Equal(new Uri(_uriValueString), actual);
        }
    }
}
