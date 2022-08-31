namespace Felsökning.Extensions.Common.Tests
{ 
    [ExcludeFromCodeCoverage]
    public class TestingDateTimeProvider : DateTimeProvider
    {
        private readonly DateTime _dateTime;

        public TestingDateTimeProvider(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public override DateTime Now => _dateTime;
    }
}