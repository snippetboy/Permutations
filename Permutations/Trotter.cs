using System.Collections.Generic;
using System.Diagnostics;

namespace SnippetBoy
{
  public static partial class Permutations
  {
    /// <summary>
    ///   Generates (non-recursive) an lexicographic unordered sequence of permutations
    ///   on a set of integers 0 to n-1 using Trotter's algorithm (1962).
    /// </summary>
    /// <param name="n">
    ///   The number of integers in the set.
    /// </param>
    /// <returns>
    ///   An IEnumerable&lt;int[]&gt; that contains the permutations.
    /// </returns>
    public static IEnumerable<int[]> Trotter(int n)
    {
      Debug.Assert(n > 0);

      int[] a = new int[n];
      int[] p = new int[n];
      int[] d = new int[n];
      bool first = true;
      int i, j, sig, tmp;

      for (i = 0; i < n; a[i] = i, i++)
        ;

      void next()
      {
        if (first)
        {
          for (i = n - 2; i >= 0; p[i] = 0, d[i] = 1, i--)
            ;
          first = false;
        }

        i = j = 0;
        sig = p[0] + d[0];
        p[0] = sig;

        while ((sig == n - j) || (sig == 0))
        {
          if (sig == 0)
          {
            d[j] = 1;
            i++;
          }
          else
          {
            d[j] = -1;
          }
          if (j == n - 2)
          {
            tmp = a[n - 2];
            a[n - 2] = a[n - 1];
            a[n - 1] = tmp;
            first = true;
            sig = 1;
          }
          else
          {
            j++;
            sig = p[j] + d[j];
            p[j] = sig;
          }
        }

        if (!first)
        {
          tmp = a[i + sig - 1];
          a[i + sig - 1] = a[i + sig];
          a[i + sig] = tmp;
        }
      };

      yield return a;

      if (n > 1)
      {
        for (next(); !first; next())
        {
          yield return a;
        }
      }
    }
  }
}
