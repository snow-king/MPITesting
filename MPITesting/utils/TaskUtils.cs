using System.Diagnostics;
using static System.Console;

namespace MPITesting.utils;

public class TaskUtils
{
    /// <summary>
    /// Название задачи.
    /// </summary>
    private readonly string _title;
	
    /// <summary>
    /// Описание задачи.
    /// </summary>
    private readonly string _description;
    private const string IntFormat = "{0:## ##0}";
    /// <summary>
    ///Данные о времени запуске программы.
    /// </summary>
    protected Stopwatch TimeExecution;
	
    /// <summary>
    /// Количество элементов массивов.
    /// </summary>
    protected int CountElements;
	
    /// <summary>
    /// Минимальное количество элеметов массива.
    /// </summary>
    private const int MinCountElements = 100000;
	
    /// <summary>
    /// Максимальное количество элеметов массива.
    /// </summary>
    private const int MaxCountElements = 1000000;

    public TaskUtils(string title, string description)
    {
        _title = title;
        _description = description;
        TimeExecution = new Stopwatch();
    }
    /// <summary>
    /// Последовательность натуральных чисел.
    /// </summary>
    private int[] Array = null!;
    
    protected virtual void ReadInputData()
    {
        Console.WriteLine("Считывание входных параметров.");
        CountElements = ReadElementsFromConsole(
            $"Введите количество элементов [{FormatInt(MinCountElements)}; {FormatInt(MaxCountElements)}]: ",
            MinCountElements, MaxCountElements);
    }
    private string FormatInt(int number)
    {
        return string.Format(IntFormat, number);
    }

    private int ReadElementsFromConsole(string message, int minValue, int maxValue)
    {
        bool error = true;
        int resultRead = 0;
        do
        {
            try
            {
                WriteLine(message);
                resultRead = Convert.ToInt32(ReadLine());
				
                error = (resultRead < minValue || resultRead > maxValue);
				
                if (error)
                {
                    WriteLine($"Вводимое значение должно быть в промежутке [{minValue}; {maxValue}]");
                }
            }
            catch (FormatException formatException)
            {
                WriteLine(formatException);
            }
        } while (error);
		
        return resultRead;
    }
    
    /// <summary>
    /// Инициализация массива случайными числами.
    /// </summary>
    /// <returns> массива заполненный случайными числами.</returns>
    private int[] InitialArrayRandomData()
    {
        var array = new int[CountElements];
        Random random;
        for (var i = 0; i < CountElements; i++)
        {
            random = new Random();
            array[i] = random.Next(100,10000000);
        }
            		
        return array;
    }
    
    public void Run()
    {
        ReadInputData();
        TaskResult.Title = _title;
        TaskResult.CountElements = CountElements;
        Array = new int[CountElements];
        Array = InitialArrayRandomData();
        TimeExecution.Reset();
    }

    public int[] GetArray()
    {
        return Array;
    }
}