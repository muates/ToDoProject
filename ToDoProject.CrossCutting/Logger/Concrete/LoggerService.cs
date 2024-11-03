using Microsoft.Extensions.Logging;
using ToDoProject.CrossCutting.Logger.Abstract;

namespace ToDoProject.CrossCutting.Logger.Concrete;

public class LoggerService(ILogger<LoggerService> logger) : ILoggerService
{
    public void LogInfo(string message)
    {
        logger.LogInformation(message);
    }

    public void LogError(string message)
    {
        logger.LogError(message);
    }

    public void LogWarning(string message)
    {
        logger.LogWarning(message);
    }
}