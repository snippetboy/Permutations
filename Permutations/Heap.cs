using System.Collections.Generic;
using System.Diagnostics;

namespace SnippetBoy
{
  public static partial class Permutations
  {
    /// <summary>
    ///   Generates (non-recursive) an lexicographic unordered sequence of permutations
    ///   on a set of integers 0 to n-1 using Heap's algorithm (1963).
    /// </summary>
    /// <param name="n">
    ///   The number of integers in the set.
    /// </param>
    /// <returns>
    ///   An IEnumerable&lt;int[]&gt; that contains the permutations.
    /// </returns>
    public static IEnumerable<int[]> Heap(int n)
    {
      Debug.Assert(n > 0);

      int[] a = new int[n];
      int[] c = new int[n];
      int i, j, tmp;

      for (i = 0; i < n; a[i] = i, c[i] = 0, i++)
        ;

      yield return a;

      for (i = 0; i < n;)
      {
        if (c[i] < i)
        {
          tmp = a[j = (i & 1) == 0 ? 0 : c[i]];
          a[j] = a[i];
          a[i] = tmp;

          yield return a;

          c[i]++;
          i = 0;
        }
        else
        {
          c[i++] = 0;
        }
      }
    }
  }
}
