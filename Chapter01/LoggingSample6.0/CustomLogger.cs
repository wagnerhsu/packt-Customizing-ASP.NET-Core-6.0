using System.Collections.Concurrent;

namespace LoggingSample;

public class ColoredConsoleLoggerConfiguration
{
    public LogLevel LogLevel { get; set; } = LogLevel.Warning;
    public int EventId { get; set; } = 0;
    public ConsoleColor Color { get; set; } = ConsoleColor.Yellow;
}

public class ColoredConsoleLoggerProvider : ILoggerProvider
{
    private readonly ColoredConsoleLoggerConfiguration _config;
    private readonly ConcurrentDictionary<string, ColoredConsoleLogger> _loggers =
        new ConcurrentDictionary<string, ColoredConsoleLogger>();

    public ColoredConsoleLoggerProvider(ColoredConsoleLoggerConfiguration config)
    {
        _config = config;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return _loggers.GetOrAdd(categoryName, name =>
            new ColoredConsoleLogger(name, _config));
    }

    public void Dispose()
    {
        _loggers.Clear();
    }
}

public class ColoredConsoleLogger : ILogger
{
    private static readonly object _lock = new Object();
    private readonly string _name;
    private readonly ColoredConsoleLoggerConfiguration _config;

    public ColoredConsoleLogger(
        string name,
        ColoredConsoleLoggerConfiguration config)
    {
        _name = name;
        _config = config;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == _config.LogLevel;
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception exception,
        Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        lock (_lock)
        {
            if (_config.EventId == 0 || _config.EventId == eventId.Id)
            {
                var color = Console.ForegroundColor;
                Console.ForegroundColor = _config.Color;
                Console.Write($"{logLevel} - ");
                Console.Write($"{eventId.Id} - {_name} - ");
                Console.Write($"{formatter(state, exception)}\n");
                Console.ForegroundColor = color;
            }
        }
    }
}