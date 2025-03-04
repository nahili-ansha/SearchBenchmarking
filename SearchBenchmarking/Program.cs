using System;
using System.Diagnostics;

public class SearchBenchmark
{
    public static void BenchmarkSearches(int[] data, int target, Func<int[], int, int> searchMethod, string searchMethodName)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        int result = searchMethod(data, target);

        stopwatch.Stop();
        Console.WriteLine($"{searchMethodName} found target at index {result} in {stopwatch.Elapsed.TotalMilliseconds} ms");
    }

    public static void Main(string[] args)
    {
        int[] sizes = { 100000, 200000, 300000, 400000, 500000, 600000, 700000, 800000, 900000, 1000000 };
        Random random = new Random();

        foreach (int size in sizes)
        {
            int[] data = new int[size];
            for (int i = 0; i < size; i++)
            {
                data[i] = random.Next(1, 1000000);
            }
            Array.Sort(data);
            int target = data[random.Next(0, size)];
            Console.WriteLine($"\nArray Size: {size}");
            BenchmarkSearches(data, target, LinearSearch, "Linear Search");
            BenchmarkSearches(data, target, BinarySearch, "Binary Search");
        }
    }
    public static int LinearSearch(int[] arr, int target)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            if(arr[i] == target)
            {
                return i;
            }
        }
        return -1;
    }

    public static int BinarySearch(int[] arr, int target)
    {
        int left = 0;
        int right = arr.Length - 1;
        int middle = left + (right - left) / 2;

        while(left <= right)
        {
            if(arr[middle] == target)
            {
                return middle;
            }
            if(target < arr[middle])
            {
                right = middle - 1;
                middle = left + (right - left) / 2;
            }
            if(target > arr[middle])
            {
                left = middle + 1;
            }

        }
        return -1;
    }

}
