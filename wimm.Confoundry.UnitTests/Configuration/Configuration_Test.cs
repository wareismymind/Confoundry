using System;
using Xunit;

namespace wimm.Confoundry.UnitTests.Configuration
{
    public abstract class Configuration_Test<T>
    {
        private const string ValueName = "ConfigurationValue";

        protected abstract string ValueString { get; }

        protected abstract T ExpectedValue { get; }

        [Fact]
        public void Construct_NullName_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new Configuration<T>(null, ValueString);
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
                var _ = new Configuration<T>(whitespace, ValueString);
            });

            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void Construct_NullValue_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new Configuration<T>(ValueName, null);
            });

            Assert.Equal("value", ex.ParamName);
        }
    }
}
