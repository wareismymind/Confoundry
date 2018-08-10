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
        public void Construct_WhitespaceName_Throws(string whitespace)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var _ = new Configuration<int>(whitespace, _intValueString);
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
