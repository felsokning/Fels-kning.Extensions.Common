namespace Felsökning.Extensions.Common.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class StatusExceptionTests
    {
        [TestMethod]
        public void StatusException_ctor()
        {
            var sut = new StatusException();

            sut.Should().BeOfType<StatusException>();
        }

        [TestMethod]
        public void StatusException_Message()
        {
            var sut = new StatusException("On Fire");

            sut.Should().BeOfType<StatusException>();
            var message = sut.Message;
            message.Should().NotBeNullOrWhiteSpace();
            message.Should().Be("Invalid status given in response: On Fire");
        }

        [TestMethod]
        public void StatusException_WrappedException()
        {
            var baseException = new Exception("The Science is leaking out");

            var sut = new StatusException("On Fire", baseException);

            sut.Should().BeOfType<StatusException>();
            var message = sut.Message;
            message.Should().NotBeNullOrWhiteSpace();
            message.Should().Be("Invalid status given in response: On Fire");
            var innerException = sut.InnerException;
            innerException.Should().BeOfType<Exception>();
            innerException?.Message.Should().Be("The Science is leaking out");
        }
    }
}