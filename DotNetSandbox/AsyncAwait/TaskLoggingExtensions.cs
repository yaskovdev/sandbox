using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AsyncAwait;

public static class TaskLoggingExtensions
{
    public static void ForgetAndLog<T>(
        this Task<T> taskToForget,
        ILogger logger,
        TaskCompletionSource<T>? taskCompletionSource = null,
        [CallerMemberName] string taskName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        ForgetAndLog(taskToForget, (Func<T>)(() => taskToForget.Result), logger,
            taskCompletionSource, taskName, sourceFilePath, sourceLineNumber);
    }

    public static void ForgetAndLog(
        this Task taskToForget,
        ILogger logger,
        TaskCompletionSource<bool>? taskCompletionSource = null,
        [CallerMemberName] string taskName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        ForgetAndLog(taskToForget, (Func<bool>)(() => true), logger, taskCompletionSource,
            taskName, sourceFilePath, sourceLineNumber);
    }

    private static void ForgetAndLog<T>(
        Task taskToForget,
        Func<T> resultFunc,
        ILogger logger,
        TaskCompletionSource<T>? taskCompletionSource,
        string taskName,
        string sourceFilePath,
        int sourceLineNumber)
    {
        taskToForget.ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                logger.Warning("Background task was cancelled: " + taskName, taskName, sourceFilePath,
                    sourceLineNumber);
                taskCompletionSource?.SetCanceled();
            }
            else if (task.IsFaulted)
            {
                logger.Error((Exception)task.Exception, "Exception in background task: " + taskName, taskName,
                    sourceFilePath, sourceLineNumber);
                if (task.Exception == null)
                    return;
                taskCompletionSource?.SetException((Exception)task.Exception);
            }
            else
                taskCompletionSource?.SetResult(resultFunc());
        }, TaskContinuationOptions.ExecuteSynchronously);
    }
}