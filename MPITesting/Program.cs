using System.Diagnostics;
using MPI;
using MPITesting.utils;

namespace MPITesting;

internal static class Program
{
    private static int Calculate(IReadOnlyList<int> array, int rank, int size)
    {
        int nt, beg, h, end;
        int sum = 0;
        nt = rank;
        h = array.Count / size;
        beg = h * nt;
        end = beg + h - 1;
        if (nt == size - 1)
        {
            end = array.Count - 1;
        }
        Console.WriteLine("Process rank {0} - beg: {1} end: {2}", rank, beg, end);
        for (var i = beg; i <= end; i++)
        {
            if (array[i] > sum) sum = array[i];
        }

        return sum;
    }

    private static void Main(string[] args)
    {
        var sWatch = new Stopwatch();
        sWatch.Start();
        var utils = new TaskUtils("Вариант 5", "Дана последовательность натуральных чисел {a0…an–1}. Создать многопоточное приложение для поиска максимального ai.");
        utils.Run();
        
        using (new MPI.Environment(ref args))
        {
            var comm = Communicator.world;
            var rank = comm.Rank;
            var size = comm.Size;
            Console.WriteLine("{0} connected", rank);
            comm.Barrier();
            if (rank == 0)
            {
                var n =  utils.GetArray().Length;
                var array = utils.GetArray();

                for (var i = 1; i < size; i++)
                {
                    comm.Send(n, i, 7);
                }

                for (var i = 1; i < size; i++)
                {
                    comm.Send(array, i, 3);
                }

                var sum = Calculate(array, rank, size);
                for (int i = 1; i < size; i++)
                {
                    var psum = 0;
                    comm.Receive(i, 4, out psum);
                    if (psum > sum) sum = psum;
                }

                Console.WriteLine("Sum: {0}", sum);
                sWatch.Stop();
                Console.WriteLine("Time: {0} ms", sWatch.ElapsedMilliseconds);
            }
            else
            {
                comm.Receive(0, 7, out int n1);
                var array = new int[n1];
                comm.Receive(0, 3, ref array);
                var psum = Calculate(array, comm.Rank, comm.Size);
                Console.WriteLine("Psum: {0} rank {1}", psum, comm.Rank);
                comm.Send(psum, 0, 4);
                sWatch.Stop();
                Console.WriteLine("Time: {0} rank {1}",sWatch.ElapsedMilliseconds,comm.Rank);
            }
        }
        Console.ReadLine();
    }
}
