//-----------------------------------------------------------------------
// <copyright file="ILogging.cs" company="Felsökning">
//     Copyright (c) Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
//-----------------------------------------------------------------------
namespace Felsökning.Extensions.Common
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ILogging"/> class.
    /// </summary>
    public static class ILogging
    {
        /// <summary>
        ///     Formats the string before logging the critical event.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogCritical(this ILogger logger, string message, [Optional] object[] args)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            logger.Log(LogLevel.Critical, $"[{assemblyName}.{methodName}]: {message} at {DateTime.UtcNow} (UTC).", args);
        }

        /// <summary>
        ///     Formats the string before logging the critical event.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogCritical(this ILogger logger, EventId eventId, string message, [Optional] object[] args)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            logger.Log(LogLevel.Critical, eventId, message: $"[{assemblyName}.{methodName}]: {message} at {DateTime.UtcNow} (UTC).", args);
        }

        /// <summary>
        ///     Formats the string before logging the critical event.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogCritical(this ILogger logger, Exception exception, string message, [Optional] object[] args)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            logger.Log(LogLevel.Critical, exception, message: $"[{assemblyName}.{methodName}]: {message} at {DateTime.UtcNow} (UTC).", args);
        }

        /// <summary>
        ///     Formats the string before logging the critical event.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogCritical(this ILogger logger, EventId eventId, Exception exception, string message, [Optional] object[] args)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            logger.Log(LogLevel.Critical, eventId, exception, $"[{assemblyName}.{methodName}]: {message} at {DateTime.UtcNow} (UTC).", args);
        }

        /// <summary>
        ///     Formats the string before logging the debug event.
        ///     The log line will show as '[{Executing AssemblyName}]: {message} at {DateTime.UtcNow.FormattedString} (UTC)'.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogDebug(this ILogger logger, string message, [Optional] object[] args)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            logger.Log(LogLevel.Debug, $"[{assemblyName}.{methodName}]: {message} at {DateTime.UtcNow} (UTC).", args);
        }

        /// <summary>
        ///     Formats the string before logging the error event.
        ///     The log line will show as '[{Executing AssemblyName}]: {message} at {DateTime.UtcNow.FormattedString} (UTC)'.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogError(this ILogger logger, string message, [Optional] object[] args)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            logger.Log(LogLevel.Error, $"[{assemblyName}.{methodName}]: {message} at {DateTime.UtcNow} (UTC).", args: args);
        }

        /// <summary>
        ///     Formats the string before logging the information event.
        ///     The log line will show as '[{Executing AssemblyName}]: {message} at {DateTime.UtcNow.FormattedString} (UTC)'.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogInformation(this ILogger logger, string message, [Optional] object[] args)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            logger.Log(LogLevel.Information, $"[{assemblyName}.{methodName}]: {message} at {DateTime.UtcNow} (UTC).", args);
        }

        /// <summary>
        ///     Formats the string before logging the warning event.
        ///     The log line will show as '[{Executing AssemblyName}]: {message} at {DateTime.UtcNow.FormattedString} (UTC)'.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogWarning(this ILogger logger, string message, [Optional] object[] args)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            logger.Log(LogLevel.Warning, $"[{assemblyName}.{methodName}]: {message} at {DateTime.UtcNow} (UTC).", args);
        }

        /// <summary>
        ///     Formats the string before logging the critical event.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogCritical<T>(this ILogger<T> logger, string message, [Optional] object[] args)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            logger.Log(LogLevel.Critical, $"[{assemblyName}.{methodName}]: {message} at {DateTime.UtcNow} (UTC).", args);
        }

        /// <summary>
        ///     Formats the string before logging the critical event.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogCritical<T>(this ILogger<T> logger, EventId eventId, string message, [Optional] object[] args)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            logger.Log(LogLevel.Critical, eventId, message: $"[{assemblyName}.{methodName}]: {message} at {DateTime.UtcNow} (UTC).", args);
        }

        /// <summary>
        ///     Formats the string before logging the critical event.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogCritical<T>(this ILogger<T> logger, Exception exception, string message, [Optional] object[] args)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            logger.Log(LogLevel.Critical, exception, message: $"[{assemblyName}.{methodName}]: {message} at {DateTime.UtcNow} (UTC).", args);
        }

        /// <summary>
        ///     Formats the string before logging the critical event.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogCritical<T>(this ILogger<T> logger, EventId eventId, Exception exception, string message, [Optional] object[] args)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            logger.Log(LogLevel.Critical, eventId, exception, $"[{assemblyName}.{methodName}]: {message} at {DateTime.UtcNow} (UTC).", args);
        }

        /// <summary>
        ///     Formats the string before logging the debug event.
        ///     The log line will show as '[{Executing AssemblyName}]: {message} at {DateTime.UtcNow.FormattedString} (UTC)'.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogDebug<T>(this ILogger<T> logger, string message, [Optional] object[] args)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            logger.Log(LogLevel.Debug, $"[{assemblyName}.{methodName}]: {message} at {DateTime.UtcNow} (UTC).", args);
        }

        /// <summary>
        ///     Formats the string before logging the error event.
        ///     The log line will show as '[{Executing AssemblyName}]: {message} at {DateTime.UtcNow.FormattedString} (UTC)'.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogError<T>(this ILogger<T> logger, string message, [Optional] object[] args)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            logger.Log(LogLevel.Error, $"[{assemblyName}.{methodName}]: {message} at {DateTime.UtcNow} (UTC).", args: args);
        }

        /// <summary>
        ///     Formats the string before logging the information event.
        ///     The log line will show as '[{Executing AssemblyName}]: {message} at {DateTime.UtcNow.FormattedString} (UTC)'.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to formet.</param>
        public static void LogInformation<T>(this ILogger<T> logger, string message, [Optional] object[] args)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            logger.Log(LogLevel.Information, $"[{assemblyName}.{methodName}]: {message} at {DateTime.UtcNow} (UTC).", args);
        }

        /// <summary>
        ///     Formats the string before logging the warning event.
        ///     The log line will show as '[{Executing AssemblyName}]: {message} at {DateTime.UtcNow.FormattedString} (UTC)'.
        /// </summary>
        /// <param name="logger">The current <see cref="ILogger"/> context.</param>
        /// <param name="message">String of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogWarning<T>(this ILogger<T> logger, string message, [Optional] object[] args)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string assemblyName = Assembly.GetCallingAssembly()?.GetName()?.Name;
            StackFrame stackFrame = new(1);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string methodName = stackFrame.GetMethod().Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            logger.Log(LogLevel.Warning, $"[{assemblyName}.{methodName}]: {message} at {DateTime.UtcNow} (UTC).", args);
        }
    }
}
