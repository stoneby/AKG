using UnityEngine;

/// <summary>
/// Logger wrapper of unity log, which will take care of removing log from release build.
/// </summary>
public static class Logger
{
    /// <summary>
    /// Flag indicates if current build is debug build.
    /// </summary>
    /// <remarks>
    /// If set, this flag should be the very beginning of game start in main thread.
    /// Currently it will be set when the first time get used.
    /// </remarks>
    public static bool IsDebugBuild = Debug.isDebugBuild;

    /// <summary>
    /// Wrapper Debug.Log
    /// </summary>
    /// <param name="message">Message to log</param>
    public static void Log(object message)
    {
        if (IsDebugBuild)
        {
            Debug.Log(message);
        }
    }

    /// <summary>
    /// Wrapper Debug.Log.
    /// </summary>
    /// <param name="message">Message to log</param>
    /// <param name="content">Content to log</param>
    public static void Log(object message, Object content)
    {
        if (IsDebugBuild)
        {
            Debug.Log(message, content);
        }
    }

    /// <summary>
    /// Wrapper Debug.LogWarning.
    /// </summary>
    /// <param name="message">Message to log</param>
    public static void LogWarning(object message)
    {
        if (IsDebugBuild)
        {
            Debug.LogWarning(message);
        }
    }

    /// <summary>
    /// Wrapper Debug.LogWarning.
    /// </summary>
    /// <param name="message">Message to log</param>
    /// <param name="content">Content to log</param>
    public static void LogWarning(object message, Object content)
    {
        if (IsDebugBuild)
        {
            Debug.LogWarning(message, content);
        }
    }

    /// <summary>
    /// Wrapper Debug.LogError
    /// </summary>
    /// <param name="message">Message to log</param>
    public static void LogError(object message)
    {
        if (IsDebugBuild)
        {
            Debug.LogError(message);
        }
    }

    /// <summary>
    /// Wrapper Debug.LogError.
    /// </summary>
    /// <param name="message">Message to log</param>
    /// <param name="content">Content to log</param>
    public static void LogError(object message, Object content)
    {
        if (IsDebugBuild)
        {
            Debug.LogError(message, content);
        }
    }
}
