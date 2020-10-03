using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SnippetBoy
{
  static class Sample
  {
    static void Main(string[] args)
    {
      ShowStatus();
      ShowPermutations();
      SpeedTest(1, 12, 1);
    }

    static void ShowStatus()
    {
      Console.Write($"{IntPtr.Size * 8}-bit application in ");
#if DEBUG
      Console.WriteLine("Debug configuration.");
#else
      Console.WriteLine("Release configuration");
#endif
      Console.WriteLine();
    }

    static void ShowPermutations()
    {
      Console.WriteLine($"Permutations.Trotter(5)");
      Permutations.Trotter(5)
        .ForEach((permutation, index) => Console.WriteLine(string.Join(",", permutation)));
      Console.WriteLine();

      Console.WriteLine($"Permutations.Heap(4)");
      Permutations.Heap(4)
        .ForEach((permutation, index) => Console.WriteLine(string.Join(",", permutation)));
      Console.WriteLine();

      Console.WriteLine($"Permutations.OrdSmith(3)");
      Permutations.OrdSmith(3)
        .ForEach((permutation, index) => Console.WriteLine(string.Join(",", permutation)));
      Console.WriteLine();
    }

    static void SpeedTest(int nmin, int nmax, int passes)
    {
      Console.Write("Speed test is running. Please wait...\r");
      GetPerformanceTable(nmin, nmax, passes)
        .DrawTable();
      Console.WriteLine();
    }

    static IEnumerable<IEnumerable<string>> GetPerformanceTable(int nmin, int nmax, int passes)
    {
      List<IEnumerable<string>> table = new List<IEnumerable<string>>
      {
        new string[] { "n", "n!", "passes", "Trotter", "Heap", "Ord-Smith" }
      };

      for (int n = nmin; n <= nmax; n++)
      {
        table.Add(new string[]
        {
          n.ToString(),
          Factorial(n).ToString(),
          passes.ToString(),
          GetPerformanceFor(Permutations.Trotter, n, passes),
          GetPerformanceFor(Permutations.Heap, n, passes),
          GetPerformanceFor(Permutations.OrdSmith, n, passes)
        });
      }

      return table;
    }

    static string GetPerformanceFor(Func<int, IEnumerable<int[]>> method, int n, int loops)
    {
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      for (int loop = 0; loop < loops; loop++)
      {
        method(n).Count();
      }
      stopwatch.Stop();

      return stopwatch.ElapsedMilliseconds.ToString() + " ms";
    }

    #region --- Little helpers ---

    static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
    {
      int index = 0;
      foreach (T item in source)
      {
        action(item, index++);
      }
      return source;
    }

    private static long Factorial(int n) =>
      Enumerable
        .Range(1, n)
        .Select(x => (long)x)
        .Aggregate((x, y) => x * y);

    static IEnumerable<IEnumerable<string>> DrawTable(this IEnumerable<IEnumerable<string>> table) =>
      table.DrawTable(table.Aggregate((x, y) => x.Zip(y, (a, b) => (a.Length < b.Length) ? b : a)).Select(cell => cell.Length));

    static IEnumerable<IEnumerable<string>> DrawTable(this IEnumerable<IEnumerable<string>> table, IEnumerable<int> widths) =>
      table.JustifyContent(widths).ForEach((row, index) => row.DrawRow(index));

    static IEnumerable<IEnumerable<string>> JustifyContent(this IEnumerable<IEnumerable<string>> table, IEnumerable<int> widths) =>
      table.Select(row => row.Zip(widths, (column, width) => column.PadLeft(width + 1).PadRight(width + 2)));

    static void DrawRow(this IEnumerable<string> row, int index) =>
      Console.WriteLine((index == 0)
        ? string.Join("|", row) + Environment.NewLine + string.Join("|", row.Select(cell => new String('-', cell.Length)))
        : string.Join("|", row));

    #endregion
  }
}
