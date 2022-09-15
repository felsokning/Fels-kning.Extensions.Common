//-----------------------------------------------------------------------
// <copyright file="TestingHttpMessageHandler.cs" company="Felsökning">
//     Copyright (c) Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
//-----------------------------------------------------------------------
namespace Felsökning.Extensions.Common.Tests
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="TestingHttpMessageHandler"/> class,
    ///     which is used to model HTTP Response messages back to the caller, based on URL.
    /// </summary>
    /// <inheritdoc cref="HttpMessageHandler"/>
    [ExcludeFromCodeCoverage]
    internal class TestingHttpMessageHandler : HttpMessageHandler
    {
        /// <summary>
        ///     Overrides the <see cref="SendAsync(HttpRequestMessage, CancellationToken)"/> method in <see cref="HttpMessageHandler"/>
        ///     to return a specified <see cref="HttpResponseMessage"/>, based on the URL called.
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task{TResult}"/> of <see cref="HttpResponseMessage"/> for the test class[es] to consume.</returns>
        /// <exception cref="HttpRequestException">A response to mock exceptions thrown on request.</exception>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(); // Scoped accessibility
            if (httpRequestMessage?.RequestUri?.AbsoluteUri == "https://jsonplaceholder.typicode.com/todos/1")
            {
                responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                responseMessage.StatusCode = System.Net.HttpStatusCode.OK;
                responseMessage.Content = new StringContent(JsonSerializer.Serialize(new SampleJson
                {
                    Completed = true,
                    Id = 8675309,
                    Title = "Super Secret and Diabolical Plans",
                    UserId = 24
                }));

                return Task<HttpResponseMessage>.Factory.StartNew(() => responseMessage, cancellationToken);
            }

            if (httpRequestMessage?.RequestUri?.AbsoluteUri == "https://jsonplaceholder.typicode.com/todos/2")
            {
                responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                responseMessage.StatusCode = System.Net.HttpStatusCode.OK;
                responseMessage.Content = new StringContent(JsonSerializer.Serialize(new SampleJson
                {
                    Completed = true,
                    Id = 8675309,
                    Title = "Super Secret and Diabolical Plans",
                    UserId = 24
                }));

                return Task<HttpResponseMessage>.Factory.StartNew(() => responseMessage, cancellationToken);
            }

            if (httpRequestMessage?.RequestUri?.AbsoluteUri == "https://jsonplaceholder.typicode.com/todos/3")
            {
                responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                responseMessage.StatusCode = System.Net.HttpStatusCode.NotFound;
                responseMessage.Content = new StringContent("The resource didn't exist, yo.");
                return Task<HttpResponseMessage>.Factory.StartNew(() => responseMessage, cancellationToken);
            }

            throw new HttpRequestException("Resource Not Found", null, System.Net.HttpStatusCode.NotFound);
        }
    }
}
