using System.Text;
using System.Text.RegularExpressions;

namespace Kamel.Primitives.Extensions;

public static partial class ExceptionExtensions
{
    [GeneratedRegex("\\s*at\\s+(?<class>[\\w+.]+)\\.(?<method>\\w+)\\([^)]*\\)\\s+in\\s+.*:(line\\s+(?<line>\\d+))?")]
    private static partial Regex StackTraceRegex();

    public static string ExtractData(this Exception? ex, string? correlationId = null)
    {
        var sb = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(correlationId))
        {
            sb.AppendLine($"Correlation ID: {correlationId}");
        }

        var currentException = ex;
        while (currentException != null)
        {
            sb.AppendLine($"Message: {currentException.Message}");
            sb.AppendLine("Stack Trace:");
            AppendCustomStackTrace(sb, currentException.StackTrace);

            currentException = currentException.InnerException;
            if (currentException != null)
                sb.AppendLine("Caused by:");
        }

        return sb.ToString();
    }

    private static readonly char[] separator = ['\n'];

    private static void AppendCustomStackTrace(StringBuilder sb, string? stackTrace)
    {
        if (string.IsNullOrEmpty(stackTrace))
        {
            sb.AppendLine("N/A");
            return;
        }

        var lines = stackTrace.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var formattedLine = FormatStackTraceLine(line);
            if (!string.IsNullOrEmpty(formattedLine))
            {
                sb.AppendLine(formattedLine);
            }
        }
    }

    private static string FormatStackTraceLine(string stackTraceLine)
    {
        var match = StackTraceRegex().Match(stackTraceLine);
        if (match.Success)
        {
            var fullClassName = match.Groups["class"].Value;
            var className = fullClassName.Split('.').Last();
            var methodName = match.Groups["method"].Value;
            var line = match.Groups["line"].Success ? $"line {match.Groups["line"].Value}" : "line info not available";
            return $"{className} | {methodName} | {line}";
        }

        return string.Empty;
    }
}