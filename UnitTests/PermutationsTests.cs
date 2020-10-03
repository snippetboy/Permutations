using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SnippetBoy
{
  [TestClass]
  public class PermutationsTests
  {
    [TestMethod]
    [DynamicData(nameof(Trotter), DynamicDataSourceType.Property)]
    [DynamicData(nameof(Heap), DynamicDataSourceType.Property)]
    [DynamicData(nameof(OrdSmith), DynamicDataSourceType.Property)]
    public void HowManyPermutations(string method, int n)
    {
      long expectedCount = Factorial(n);
      Assert.AreEqual(expectedCount,
        Permutate(method, n)
          .LongCount());
    }

    [TestMethod]
    [DynamicData(nameof(Trotter), DynamicDataSourceType.Property)]
    [DynamicData(nameof(Heap), DynamicDataSourceType.Property)]
    [DynamicData(nameof(OrdSmith), DynamicDataSourceType.Property)]
    public void HowManyDistinctPermutations(string method, int n)
    {
      Assert.IsTrue(n <= 10, "Test not suitable for n > 10.");
      long expectedCount = Factorial(n);
      Assert.AreEqual(expectedCount,
        Permutate(method, n)
          .Select(permutation => string.Join(',', permutation))
          .Distinct()
          .LongCount());
    }

    [TestMethod]
    [DynamicData(nameof(Trotter), DynamicDataSourceType.Property)]
    [DynamicData(nameof(Heap), DynamicDataSourceType.Property)]
    [DynamicData(nameof(OrdSmith), DynamicDataSourceType.Property)]
    public void HowManyElementsInPermutations(string method, int n)
    {
      int expectedLength = n;
      Assert.IsTrue(
        Permutate(method, n)
          .All(permutation => permutation.Length == expectedLength));
    }

    [TestMethod]
    [DynamicData(nameof(Trotter), DynamicDataSourceType.Property)]
    [DynamicData(nameof(Heap), DynamicDataSourceType.Property)]
    [DynamicData(nameof(OrdSmith), DynamicDataSourceType.Property)]
    public void MinAndMaxInEachPermutation(string method, int n)
    {
      int expectedMin = 0;
      int expectedMax = n - 1;
      Assert.IsTrue(
        Permutate(method, n)
          .All(p => p.Min() == expectedMin && p.Max() == expectedMax));
    }

    [TestMethod]
    [DynamicData(nameof(OrdSmith), DynamicDataSourceType.Property)]
    public void LexicographicOrderedPermutation(string method, int n)
    {
      Assert.IsTrue(
        Permutate(method, n)
          .Select(permutation => (z: string.Join(string.Empty, permutation), isAscending: true))
          .Aggregate((x, y) => (y.z, y.isAscending && string.Compare(x.z, y.z) < 0))
          .isAscending);
    }

    #region --- Little Helpers ---

    private static IEnumerable<object[]> Trotter => GetData("Trotter", 1, 9);
    private static IEnumerable<object[]> Heap => GetData("Heap", 1, 9);
    private static IEnumerable<object[]> OrdSmith => GetData("OrdSmith", 1, 9);

    private static IEnumerable<object[]> GetData(string method, int nmin, int nmax) =>
      Enumerable
        .Range(nmin, nmax - nmin + 1)
        .Select(n => new object[] { method, n });

    private static long Factorial(int n) =>
      Enumerable
        .Range(1, n)
        .Select(x => (long)x)
        .Aggregate((x, y) => x * y);

    private static IEnumerable<int[]> Permutate(string method, int n) => (IEnumerable<int[]>)
      typeof(Permutations)
        .GetMethod(method, BindingFlags.Public | BindingFlags.Static, null, CallingConventions.Any, new Type[] { typeof(int) }, null)
        .Invoke(null, new object[] { n });

    #endregion
  }
}
