namespace MPITesting.utils;

public struct TaskResult
{
    /// <summary>
    /// Номер задачи.
    /// </summary>
    public static string Title;

    /// <summary>
    /// Количество элементов в последовательности.
    /// </summary>
    public static int CountElements;

    /// <summary>
    /// Количество потоков.
    /// </summary>
    public int CountThreads;

    /// <summary>
    /// Время выполнения задачи.
    /// </summary>
    public string Time;

    /// <summary>
    /// Результат выполнения задачи.
    /// </summary>
    public string Results;

    public override string ToString()
    {
        return string.Format($"{Title},{CountElements},{CountThreads},{Time},{Results}" +
                             $"{Environment.OSVersion}," +
                             $"{Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE")}," +
                             $"{Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER")}," +
                             $"{Environment.ProcessorCount}");
    }
}