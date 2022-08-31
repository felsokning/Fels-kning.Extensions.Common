//-----------------------------------------------------------------------
// <copyright file="HttpExtensions.cs" company="Felsökning">
//     Copyright (c) Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
//-----------------------------------------------------------------------
using System.Net.Http;

namespace Felsökning.Extensions.Common
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="HttpExtensions"/> class, 
    ///     which is used to extend Http-Related classes.
    /// </summary>
    public static class HttpExtensions
    {
        /// <summary>
        ///     Adds the given header name and value to the <see cref="HttpClient"/>.
        ///     WARNING: The existing header of the same name will be removed, if it exists.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="name">The header to add to the collection.</param>
        /// <param name="value">The content of the header.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        public static void AddHeader(this HttpClient httpClient, string name, string value, [Optional] ILogger azLogger)
        {
            httpClient.RemoveHeader(name);
            httpClient.DefaultRequestHeaders.Add(name: name, value: value);
            if (azLogger != null)
            {
                azLogger.LogInformation(message: $"Successfully added the '{name}' header with the value of '{value}'");
            }
        }

        /// <summary>
        ///     Generates a new request id for the given <see cref="HttpClient"/> for tracking/tracing reasons.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        public static void GenerateNewRequestId(this HttpClient httpClient, [Optional] ILogger azLogger)
        {
            var generatedRequestId = Guid.NewGuid().ToString();
            httpClient.AddHeader("X-Request-ID", generatedRequestId);
        }

        /// <summary>
        ///     Obtains the HTTP response from the given URL and deserializes it into the given object of <typeparamref name="T"/>.
        ///     <para>We only check for successful HTTP responses. Any continuations must be handled by the caller.</para>
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        /// <returns>An awaitable <see cref="Task{T}"/> of <see cref="{T}"/></returns>
        public static async Task<T> GetDeserializedTypeData<T>(this HttpClient httpClient, string requestUrl, [Optional] ILogger azLogger)
        {
            if (azLogger != null)
            {
                azLogger.LogInformation(message: $"Requesting data from '{requestUrl}'");
            }

            try
            {
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(requestUrl);
                string httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    if (azLogger != null)
                    {
                        azLogger.LogInformation(message: $"Received and parsing/returning data from '{requestUrl}'");
                    }

                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    return JsonSerializer.Deserialize<T>(httpResponseMessageContent, options);
                }
                else
                {
                    if (azLogger != null)
                    {
                        azLogger.LogError(message: $"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}': {httpResponseMessageContent}");
                    }

                    throw new StatusException(httpResponseMessage.StatusCode.ToString(), httpResponseMessageContent);
                }
            }
            catch(HttpRequestException thrownException)
            {
                if (azLogger != null)
                {
                    azLogger.LogError(message: $"Received {thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'.");
                }

                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException);
            }
        }

        /// <summary>
        ///     Deserializes data and sends the patch request to update the object.
        /// </summary>
        /// <typeparam name="T">The base type to be deserialized and patched.</typeparam>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="typeObject">The object to be deserialized and patched.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        /// <returns>An awaitable <see cref="Task{T}"/> of <see cref="{T}"/></returns>
        public static async Task<T> PatchDeserializedData<T>(this HttpClient httpClient, string requestUrl, T typeObject, [Optional] ILogger azLogger)
        {
            if (azLogger != null)
            {
                azLogger.LogInformation(message: $"Requesting data from '{requestUrl}'");
            }

            try
            {
                HttpRequestMessage httpRequestMessage = new(method: new HttpMethod("PATCH"), requestUri: requestUrl)
                {
                    Content = new StringContent(content: JsonSerializer.Serialize(typeObject))
                };
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request: httpRequestMessage);
                string? httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    if (azLogger != null)
                    {

                        azLogger.LogInformation(message: $"Received and parsing/returning data from '{requestUrl}' at {DateTime.UtcNow} (UTC)");
                    }

                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    return JsonSerializer.Deserialize<T>(httpResponseMessageContent, options);
                }
                else
                {
                    if (azLogger != null)
                    {
                        azLogger.LogError(message: $"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}': {httpResponseMessageContent}");
                    }

                    throw new StatusException(httpResponseMessage.StatusCode.ToString(), httpResponseMessageContent);
                }
            }
            catch (HttpRequestException thrownException)
            {
                if (azLogger != null)
                {
                    azLogger.LogError(message: $"Received {thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'.");
                }

                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException);
            }
        }

        /// <summary>
        ///     Deserializes data and sends the patch request to update the object.
        /// </summary>
        /// <typeparam name="T">The base type to be deserialized and patched.</typeparam>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="typeObject">The object to be deserialized and patched.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        /// <returns>An awaitable <see cref="Task{T}"/></returns>
        public static async Task PatchData<T>(this HttpClient httpClient, string requestUrl, T typeObject, [Optional] ILogger azLogger)
        {
            if (azLogger != null)
            {
                azLogger.LogInformation(message: $"Patching data from '{requestUrl}'");
            }

            try
            {
                HttpRequestMessage httpRequestMessage = new(method: new HttpMethod("PATCH"), requestUri: requestUrl)
                {
                    Content = new StringContent(content: JsonSerializer.Serialize(typeObject))
                };

                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request: httpRequestMessage);
                if (!httpResponseMessage.IsSuccessStatusCode)
                {

                    string? httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();

                    if (azLogger != null)
                    {
                        azLogger.LogError(message: $"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}': {httpResponseMessageContent}");
                    }

                    throw new StatusException(httpResponseMessage.StatusCode.ToString(), httpResponseMessageContent);
                }
            }
            catch (HttpRequestException thrownException)
            {
                if (azLogger != null)
                {
                    azLogger.LogError(message: $"Received {thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'.");
                }

                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException);
            }
        }

        /// <summary>
        ///     Obtains the HTTP response from the given URL and deserializes it into the given object of <typeparamref name="T"/>.
        ///     We only check for successful HTTP responses. Any continuations must be handled by the caller.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="httpContent">The content to be posted, in string form.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        /// <returns>An awaitable <see cref="Task{T}"/></returns>
        public static async Task<T> PostDeserializedTypeData<T>(this HttpClient httpClient, string requestUrl, HttpContent httpContent, [Optional] ILogger azLogger)
        {
            if (azLogger != null)
            {
                azLogger.LogInformation($"Requesting data from '{requestUrl}'");
            }

            try
            {
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(requestUri: requestUrl, content: httpContent);
                string httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    if (azLogger != null)
                    {
                        azLogger.LogInformation($"Received and parsing/returning data from '{requestUrl}'");
                    }

                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    return JsonSerializer.Deserialize<T>(httpResponseMessageContent, options);
                }
                else
                {
                    if (azLogger != null)
                    {
                        azLogger.LogError(message: $"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}': {httpResponseMessageContent}");
                    }

                    throw new StatusException(httpResponseMessage.StatusCode.ToString(), httpResponseMessageContent);
                }
            }
            catch (HttpRequestException thrownException)
            {
                if (azLogger != null)
                {
                    azLogger.LogError(message: $"Received {thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'.");
                }

                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException);
            }
        }

        /// <summary>
        ///     Obtains the HTTP response from the given URL and deserializes it into the given object of <typeparamref name="T"/>.
        ///     We only check for successful HTTP responses. Any continuations must be handled by the caller.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="stringContent">The content to be posted, in string form.</param>
        /// <param name="contentType">The content type the server should be expecting.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        /// <returns>An awaitable <see cref="Task{T}"/></returns>
        public static async Task<T> PostDeserializedTypeData<T>(this HttpClient httpClient, string requestUrl, string stringContent, string contentType, [Optional] ILogger azLogger)
        {
            if (azLogger != null)
            {
                azLogger.LogInformation(message: $"Requesting data from '{requestUrl}'");
            }

            try
            {
                HttpContent httpContent = new StringContent(content: stringContent);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType: contentType);
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(requestUri: requestUrl, content: httpContent);
                string? httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    if (azLogger != null)
                    {
                        azLogger.LogInformation(message: $"Received and parsing/returning data from '{requestUrl}'");
                    }

                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    return JsonSerializer.Deserialize<T>(httpResponseMessageContent, options);
                }
                else
                {
                    if (azLogger != null)
                    {
                        azLogger.LogError(message: $"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}': {httpResponseMessageContent}");
                    }

                    throw new StatusException(httpResponseMessage.StatusCode.ToString(), httpResponseMessageContent);
                }
            }
            catch (HttpRequestException thrownException)
            {
                if (azLogger != null)
                {
                    azLogger.LogError(message: $"Received {thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'.");
                }

                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException);
            }
        }

        /// <summary>
        ///     Obtains the HTTP response from the given URL and deserializes it into the given object of <typeparamref name="T"/>.
        ///     We only check for successful HTTP responses. Any continuations must be handled by the caller.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="httpContent">The content to be put.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        /// <returns>An awaitable <see cref="Task{T}"/></returns>
        public static async Task<T> PutDeserializedTypeData<T>(this HttpClient httpClient, string requestUrl, HttpContent httpContent, [Optional] ILogger azLogger)
        {
            if (azLogger != null)
            {
                azLogger.LogInformation(message: $"Requesting data from '{requestUrl}'");
            }

            try
            {
                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(requestUri: requestUrl, content: httpContent);
                string? httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    if (azLogger != null)
                    {
                        azLogger.LogInformation(message: $"Received and parsing/returning data from '{requestUrl}'");
                    }

                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    return JsonSerializer.Deserialize<T>(httpResponseMessageContent, options);
                }
                else
                {
                    if (azLogger != null)
                    {
                        azLogger.LogError(message: $"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}': {httpResponseMessageContent}");
                    }

                    throw new StatusException(httpResponseMessage.StatusCode.ToString(), httpResponseMessageContent);
                }
            }
            catch (HttpRequestException thrownException)
            {
                if (azLogger != null)
                {
                    azLogger.LogError(message: $"Received {thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'.");
                }

                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException);
            }
        }

        /// <summary>
        ///     Obtains the HTTP response from the given URL and deserializes it into the given object of <typeparamref name="T"/>.
        ///     We only check for successful HTTP responses. Any continuations must be handled by the caller.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="requestUrl">The web url to do the request from.</param>
        /// <param name="stringContent">The content to be put, in string form.</param>
        /// <param name="contentType">The content type the server should be expecting.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        /// <returns>An awaitable <see cref="Task{T}"/></returns>
        public static async Task<T> PutDeserializedTypeData<T>(this HttpClient httpClient, string requestUrl, string stringContent, string contentType, [Optional] ILogger azLogger)
        {
            if (azLogger != null)
            {
                azLogger.LogInformation(message: $"Requesting data from '{requestUrl}'");
            }

            try
            {
                HttpContent httpContent = new StringContent(content: stringContent);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType: contentType);
                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(requestUri: requestUrl, content: httpContent);
                string? httpResponseMessageContent = await httpResponseMessage.Content.ReadAsStringAsync();
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    if (azLogger != null)
                    {
                        azLogger.LogInformation(message: $"Received and parsing/returning data from '{requestUrl}'");
                    }

                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new JsonStringEnumConverter());
                    return JsonSerializer.Deserialize<T>(httpResponseMessageContent, options);
                }
                else
                {
                    if (azLogger != null)
                    {
                        azLogger.LogError(message: $"Received {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} from '{requestUrl}': {httpResponseMessageContent}");
                    }

                    throw new StatusException(httpResponseMessage.StatusCode.ToString(), httpResponseMessageContent);
                }
            }
            catch (HttpRequestException thrownException)
            {
                if (azLogger != null)
                {
                    azLogger.LogError(message: $"Received {thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'.");
                }

                throw new StatusException($"{thrownException.StatusCode} - {thrownException.Message} from '{requestUrl}'", thrownException);
            }
        }

        /// <summary>
        ///     Removes the given header, if it exists.
        /// </summary>
        /// <param name="httpClient">The current <see cref="HttpClient"/> context.</param>
        /// <param name="name">The header to be removed from the <see cref="HttpClient.DefaultRequestHeaders"/> context.</param>
        /// <param name="azLogger">An <see cref="OptionalAttribute"/> <see cref="ILogger"/> for logging in Azure contexts.</param>
        public static void RemoveHeader(this HttpClient httpClient, string name, [Optional] ILogger azLogger)
        {
            if (httpClient.DefaultRequestHeaders.Contains(name: name))
            {
                httpClient.DefaultRequestHeaders.Remove(name: name);
                if (azLogger != null)
                {
                    azLogger.LogInformation(message: $"Successfully removed the '{name}' header");
                }
            }
        }
    }
}
