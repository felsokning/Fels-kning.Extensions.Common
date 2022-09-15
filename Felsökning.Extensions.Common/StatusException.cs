// ----------------------------------------------------------------------
// <copyright file="StatusException.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning.Extensions.Common
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="StatusException"/> class.
    ///     <inheritdoc cref="Exception"/>
    /// </summary>
    [Serializable]
    public class StatusException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StatusException"/> class.
        ///     <inheritdoc cref="Exception"/>
        /// </summary>
        public StatusException() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StatusException"/> class.
        ///     <inheritdoc cref="Exception"/>
        /// </summary>
        /// <param name="status">The status that describes the error.</param>
        public StatusException(string status)
            : base($"Invalid status given in response: {status}")
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StatusException"/> class.
        ///     <inheritdoc cref="Exception"/>
        /// </summary>
        /// <param name="statusCode">The status code that describes the error.</param>
        /// <param name="message">The message from the content body.</param>
        public StatusException(string statusCode, string message)
            : base($"Invalid status response received. Status: {statusCode}. Message: {message}")
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StatusException"/> class.
        ///     <inheritdoc cref="Exception"/>
        /// </summary>
        /// <param name="status">The status that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public StatusException(string status, Exception innerException)
            : base($"Invalid status given in response: {status}", innerException)
        {
        }
    }
}