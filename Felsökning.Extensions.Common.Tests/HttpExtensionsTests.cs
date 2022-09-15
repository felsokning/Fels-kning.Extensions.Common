//-----------------------------------------------------------------------
// <copyright file="HttpExtensionsTests.cs" company="Felsökning">
//     Copyright (c) Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
//-----------------------------------------------------------------------
namespace Felsökning.Extensions.Common.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class HttpExtensionsTests
    {
        [TestMethod]
        public void ValidateHeaderAdded()
        {
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            HttpClient client = new HttpClient();
            client.AddHeader("Test", "testing", nullLoggerFactory.CreateLogger("science"));
            Assert.IsTrue(client.DefaultRequestHeaders.Contains("Test"));
            Assert.IsFalse(string.IsNullOrWhiteSpace(client.DefaultRequestHeaders.GetValues("Test").FirstOrDefault()));
            Assert.IsTrue(client?.DefaultRequestHeaders?.GetValues("Test")?.FirstOrDefault()?.Equals("testing"));
        }

        [TestMethod]
        public void ValidateRequestIdGenerated()
        {
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            HttpClient client = new HttpClient();
            client.GenerateNewRequestId(nullLoggerFactory.CreateLogger("science"));
            Assert.IsTrue(client.DefaultRequestHeaders.Contains("X-Request-ID"));
            Assert.IsFalse(string.IsNullOrWhiteSpace(client.DefaultRequestHeaders.GetValues("X-Request-ID").FirstOrDefault()));
            client.GenerateNewRequestId(nullLoggerFactory.CreateLogger("science"));
            Assert.IsTrue(client.DefaultRequestHeaders.Contains("X-Request-ID"));
            Assert.IsFalse(string.IsNullOrWhiteSpace(client.DefaultRequestHeaders.GetValues("X-Request-ID").FirstOrDefault()));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ValidateHeaderRemoved()
        {
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            HttpClient client = new HttpClient();
            client.GenerateNewRequestId(nullLoggerFactory.CreateLogger("science"));
            client.RemoveHeader("X-Request-ID", nullLoggerFactory.CreateLogger("science"));
            Assert.IsTrue(string.IsNullOrWhiteSpace(client.DefaultRequestHeaders.GetValues("X-Request-ID").FirstOrDefault()));
        }

        [TestMethod]
        public void ValidateNonExistingHeaderRemoved()
        {
            HttpClient client = new HttpClient();
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            client.RemoveHeader("X-Request-ID", nullLoggerFactory.CreateLogger("science"));
        }

        [TestMethod]
        public async Task GetDeserializedTypeData_Succeeds()
        {
            HttpClient client = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson content = await client.GetDeserializedTypeData<SampleJson>("https://jsonplaceholder.typicode.com/todos/1", nullLoggerFactory.CreateLogger("science"));
            Assert.IsNotNull(content);
            Assert.IsNotNull(content.Title);
            Assert.IsFalse(string.IsNullOrWhiteSpace(content.Title));
        }

        [TestMethod]
        public async Task GetDeserializedTypeData_Throws_StatusException()
        {
            HttpClient client = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.GetDeserializedTypeData<SampleJson>("https://jsonplaceholder.typicode.com/todos/3", nullLoggerFactory.CreateLogger("science")));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status response received. Status: NotFound. Message: The resource didn't exist, yo.");
            exception.InnerException.Should().BeNull();
        }

        [TestMethod]
        public async Task GetDeserializedTypeData_ThrowsStatusException_ForException()
        {
            HttpClient client = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.GetDeserializedTypeData<SampleJson>("https://jsonplaceholder.typicode.com/todos/1000", nullLoggerFactory.CreateLogger("science")));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status given in response: NotFound - Resource Not Found from 'https://jsonplaceholder.typicode.com/todos/1000'");
            var innerException = exception.InnerException;
            innerException.Should().BeOfType<HttpRequestException>();
            innerException?.Message.Should().Be("Resource Not Found");
        }

        [TestMethod]
        public async Task PatchDeserializedData_Succeeds()
        {
            HttpClient httpClient = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson patchTarget = new SampleJson();
            SampleJson content = await httpClient.PatchDeserializedData<SampleJson>("https://jsonplaceholder.typicode.com/todos/2", patchTarget, nullLoggerFactory.CreateLogger("science"));
            Assert.IsNotNull(content);
            Assert.IsNotNull(content.Title);
            Assert.IsFalse(string.IsNullOrWhiteSpace(content.Title));
        }

        [TestMethod]
        public async Task PatchDeserializedData_Throws_StatusException()
        {
            HttpClient client = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson patchTarget = new SampleJson();

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PatchDeserializedData<SampleJson>("https://jsonplaceholder.typicode.com/todos/3", patchTarget, nullLoggerFactory.CreateLogger("science")));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status response received. Status: NotFound. Message: The resource didn't exist, yo.");
            exception.InnerException.Should().BeNull();
        }

        [TestMethod]
        public async Task PatchDeserializedData_ThrowsStatusException_ForException()
        {
            HttpClient client = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson patchTarget = new SampleJson();

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PatchDeserializedData<SampleJson>("https://jsonplaceholder.typicode.com/todos/1000", patchTarget, nullLoggerFactory.CreateLogger("science")));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status given in response: NotFound - Resource Not Found from 'https://jsonplaceholder.typicode.com/todos/1000'");
            var innerException = exception.InnerException;
            innerException.Should().BeOfType<HttpRequestException>();
            innerException?.Message.Should().Be("Resource Not Found");
        }

        [TestMethod]
        public async Task PatchData_Succeeds()
        {
            HttpClient httpClient = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson patchTarget = new SampleJson();
            await httpClient.PatchData<SampleJson>("https://jsonplaceholder.typicode.com/todos/2", patchTarget, nullLoggerFactory.CreateLogger("science"));
        }

        [TestMethod]
        public async Task PatchData_Throws_StatusException()
        {
            HttpClient client = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson patchTarget = new SampleJson();

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PatchData<SampleJson>("https://jsonplaceholder.typicode.com/todos/3", patchTarget, nullLoggerFactory.CreateLogger("science")));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status response received. Status: NotFound. Message: The resource didn't exist, yo.");
            exception.InnerException.Should().BeNull();
        }

        [TestMethod]
        public async Task PatchData_Throws_StatusException_ForException()
        {
            HttpClient client = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson patchTarget = new SampleJson();

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PatchData<SampleJson>("https://jsonplaceholder.typicode.com/todos/1000", patchTarget, nullLoggerFactory.CreateLogger("science")));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status given in response: NotFound - Resource Not Found from 'https://jsonplaceholder.typicode.com/todos/1000'");
            var innerException = exception.InnerException;
            innerException.Should().BeOfType<HttpRequestException>();
            innerException?.Message.Should().Be("Resource Not Found");
        }

        [TestMethod]
        public async Task PostDeserializedTypeData_HttpContent_Succeeds()
        {
            HttpClient httpClient = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson patchTarget = new SampleJson();
            var httpContent = new StringContent(JsonSerializer.Serialize(patchTarget));

            var result = await httpClient.PostDeserializedTypeData<SampleJson>("https://jsonplaceholder.typicode.com/todos/1", httpContent, nullLoggerFactory.CreateLogger("science"));

            result.Should().NotBeNull();
            result.Completed.Should().BeTrue();
            result.Id.Should().Be(8675309);
            result.Title.Should().Be("Super Secret and Diabolical Plans");
            result.UserId.Should().Be(24);
        }

        [TestMethod]
        public async Task PostDeserializedTypeData_HttpContent_Throws_StatusException()
        {
            HttpClient client = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson patchTarget = new SampleJson();
            var httpContent = new StringContent(JsonSerializer.Serialize(patchTarget));

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PostDeserializedTypeData<SampleJson>("https://jsonplaceholder.typicode.com/todos/3", httpContent, nullLoggerFactory.CreateLogger("science")));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status response received. Status: NotFound. Message: The resource didn't exist, yo.");
            exception.InnerException.Should().BeNull();
        }

        [TestMethod]
        public async Task PostDeserializedTypeData_HttpContent_Throws_StatusException_ForException()
        {
            HttpClient client = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson patchTarget = new SampleJson();
            var httpContent = new StringContent(JsonSerializer.Serialize(patchTarget));

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PostDeserializedTypeData<SampleJson>("https://jsonplaceholder.typicode.com/todos/1000", httpContent, nullLoggerFactory.CreateLogger("science")));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status given in response: NotFound - Resource Not Found from 'https://jsonplaceholder.typicode.com/todos/1000'");
            var innerException = exception.InnerException;
            innerException.Should().BeOfType<HttpRequestException>();
            innerException?.Message.Should().Be("Resource Not Found");
        }

        [TestMethod]
        public async Task PostDeserializedTypeData_String_Succeeds()
        {
            HttpClient httpClient = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson patchTarget = new SampleJson();
            var httpContent = JsonSerializer.Serialize(patchTarget);
            var contentType = "application/json";

            var result = await httpClient.PostDeserializedTypeData<SampleJson>("https://jsonplaceholder.typicode.com/todos/1", httpContent, contentType, nullLoggerFactory.CreateLogger("science"));

            result.Should().NotBeNull();
            result.Completed.Should().BeTrue();
            result.Id.Should().Be(8675309);
            result.Title.Should().Be("Super Secret and Diabolical Plans");
            result.UserId.Should().Be(24);
        }

        [TestMethod]
        public async Task PostDeserializedTypeData_String_Throws_StatusException()
        {
            HttpClient client = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson patchTarget = new SampleJson();
            var httpContent = JsonSerializer.Serialize(patchTarget);
            var contentType = "application/json";

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PostDeserializedTypeData<SampleJson>("https://jsonplaceholder.typicode.com/todos/3", httpContent, contentType, nullLoggerFactory.CreateLogger("science")));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status response received. Status: NotFound. Message: The resource didn't exist, yo.");
            exception.InnerException.Should().BeNull();
        }

        [TestMethod]
        public async Task PostDeserializedTypeData_String_Throws_StatusException_ForException()
        {
            HttpClient client = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson patchTarget = new SampleJson();
            var httpContent = JsonSerializer.Serialize(patchTarget);
            var contentType = "application/json";

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PostDeserializedTypeData<SampleJson>("https://jsonplaceholder.typicode.com/todos/1000", httpContent, contentType, nullLoggerFactory.CreateLogger("science")));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status given in response: NotFound - Resource Not Found from 'https://jsonplaceholder.typicode.com/todos/1000'");
            var innerException = exception.InnerException;
            innerException.Should().BeOfType<HttpRequestException>();
            innerException?.Message.Should().Be("Resource Not Found");
        }

        [TestMethod]
        public async Task PutDeserializedTypeData_HttpContent_Succeeds()
        {
            HttpClient httpClient = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson patchTarget = new SampleJson();
            var httpContent = new StringContent(JsonSerializer.Serialize(patchTarget));

            var result = await httpClient.PutDeserializedTypeData<SampleJson>("https://jsonplaceholder.typicode.com/todos/1", httpContent, nullLoggerFactory.CreateLogger("science"));

            result.Should().NotBeNull();
            result.Completed.Should().BeTrue();
            result.Id.Should().Be(8675309);
            result.Title.Should().Be("Super Secret and Diabolical Plans");
            result.UserId.Should().Be(24);
        }

        [TestMethod]
        public async Task PutDeserializedTypeData_HttpContent_Throws_StatusException()
        {
            HttpClient client = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson patchTarget = new SampleJson();
            var httpContent = new StringContent(JsonSerializer.Serialize(patchTarget));

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PutDeserializedTypeData<SampleJson>("https://jsonplaceholder.typicode.com/todos/3", httpContent, nullLoggerFactory.CreateLogger("science")));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status response received. Status: NotFound. Message: The resource didn't exist, yo.");
            exception.InnerException.Should().BeNull();
        }

        [TestMethod]
        public async Task PutDeserializedTypeData_HttpContent_Throws_StatusException_ForException()
        {
            HttpClient client = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson patchTarget = new SampleJson();
            var httpContent = new StringContent(JsonSerializer.Serialize(patchTarget));

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PutDeserializedTypeData<SampleJson>("https://jsonplaceholder.typicode.com/todos/1000", httpContent, nullLoggerFactory.CreateLogger("science")));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status given in response: NotFound - Resource Not Found from 'https://jsonplaceholder.typicode.com/todos/1000'");
            var innerException = exception.InnerException;
            innerException.Should().BeOfType<HttpRequestException>();
            innerException?.Message.Should().Be("Resource Not Found");
        }

        [TestMethod]
        public async Task PutDeserializedTypeData_String_Succeeds()
        {
            HttpClient httpClient = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson patchTarget = new SampleJson();
            var httpContent = JsonSerializer.Serialize(patchTarget);
            var contentType = "application/json";

            var result = await httpClient.PutDeserializedTypeData<SampleJson>("https://jsonplaceholder.typicode.com/todos/1", httpContent, contentType, nullLoggerFactory.CreateLogger("science"));

            result.Should().NotBeNull();
            result.Completed.Should().BeTrue();
            result.Id.Should().Be(8675309);
            result.Title.Should().Be("Super Secret and Diabolical Plans");
            result.UserId.Should().Be(24);
        }

        [TestMethod]
        public async Task PutDeserializedTypeData_String_Throws_StatusException()
        {
            HttpClient client = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson patchTarget = new SampleJson();
            var httpContent = JsonSerializer.Serialize(patchTarget);
            var contentType = "application/json";

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PutDeserializedTypeData<SampleJson>("https://jsonplaceholder.typicode.com/todos/3", httpContent, contentType, nullLoggerFactory.CreateLogger("science")));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status response received. Status: NotFound. Message: The resource didn't exist, yo.");
            exception.InnerException.Should().BeNull();
        }

        [TestMethod]
        public async Task PutDeserializedTypeData_String_Throws_StatusException_ForException()
        {
            HttpClient client = new HttpClient(new TestingHttpMessageHandler());
            ILoggerFactory nullLoggerFactory = new NullLoggerFactory();
            SampleJson patchTarget = new SampleJson();
            var httpContent = JsonSerializer.Serialize(patchTarget);
            var contentType = "application/json";

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PutDeserializedTypeData<SampleJson>("https://jsonplaceholder.typicode.com/todos/1000", httpContent, contentType, nullLoggerFactory.CreateLogger("science")));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status given in response: NotFound - Resource Not Found from 'https://jsonplaceholder.typicode.com/todos/1000'");
            var innerException = exception.InnerException;
            innerException.Should().BeOfType<HttpRequestException>();
            innerException?.Message.Should().Be("Resource Not Found");
        }
    }
}