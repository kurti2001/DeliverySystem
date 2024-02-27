﻿using Microsoft.Extensions.Hosting;

namespace BLL.Singleton;

public interface ILoggerService
{
    Task LogError(Exception ex);
    Task LogError(string error);
    Task LogDebug(string message);
}

internal class LoggerService : ILoggerService
{
    private readonly string _logsPath;
    public LoggerService(IHostEnvironment host)
    {
        _logsPath = host.ContentRootPath + "Logs";
    }
    async Task WriteToFile(string fileName, string content)
    {
        fileName = $"{fileName}_{DateTime.Now:dd-MM-yyyy}.log";
        content = @$"{Environment.NewLine}" +
            $"==========================" +
            $"{Environment.NewLine}" +
            $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}" +
            $"{Environment.NewLine}" +
            $"{content}" +
            $"{Environment.NewLine}" +
            $"==========================" +
            $"{Environment.NewLine}";
        await File.AppendAllTextAsync(Path.Combine(_logsPath, fileName), content);
    }

    public Task LogDebug(string message)
    {
        WriteToFile("debug", message);
        return Task.CompletedTask;
    }

    public Task LogError(Exception ex)
    {
        LogError(ex.StackTrace);
        return Task.CompletedTask;
    }

    public Task LogError(string error)
    {
        WriteToFile("errors", error);
        return Task.CompletedTask;
    }
}