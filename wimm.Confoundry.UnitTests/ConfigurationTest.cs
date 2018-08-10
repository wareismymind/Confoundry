using System;
using Xunit;

namespace wimm.Confoundry.UnitTests
{
    public class ConfigurationTest
    {
        private const string _valueName = "Configuration Value";

        private const string _intValueString = "42";
        private const int _intExpectedValue = 42;

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
    }
}
