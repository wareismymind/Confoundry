namespace wimm.Confoundry.UnitTests.Configuration
{
    public class Int_Test : Configuration_Test<int>
    {
        protected override string ValueString => "42";

        protected override int ExpectedValue => 42;
    }
}
