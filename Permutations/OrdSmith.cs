using System.Collections.Generic;
using System.Diagnostics;

namespace SnippetBoy
{
  public static partial class Permutations
  {
    /// <summary>
    ///   Generates (non-recursive) an lexicographic ordered sequence of permutations
    ///   on a set of integers 0 to n-1 using Ord-Smith's algorithm (1968).
    /// </summary>
    /// <param name="n">
    ///   The number of integers in the set.
    /// </param>
    /// <returns>
    ///   An IEnumerable&lt;int[]&gt; that contains the permutations.
    /// </returns>
    public static IEnumerable<int[]> OrdSmith(int n)
    {
      Debug.Assert(n > 0);

      int[] a = new int[n];
      int[] d = new int[n];
      bool first = true, od = false;
      int i, j, tmp;

      for (i = 0; i < n; a[i] = i, i++)
        ;

      void next()
      {
        if (first)
        {
          first = false;
          od = true;
          for (i = n - 1; i > 0; d[i--] = 0)
            ;
        }
        if (od)
        {
          od = false;
          tmp = a[n - 1];
          a[n - 1] = a[n - 2];
          a[n - 2] = tmp;
        }
        else
        {
          od = true;
          d[j = 1]++;
          while (d[j] > j + 1)
          {
            d[j++] = 0;
            d[j]++;
          }
          if (j >= n - 1)
          {
            first = true;
          }
          else
          {
            i = n - d[j];
            tmp = a[n - j - 2];
            a[n - j - 2] = a[i];
            a[i] = tmp;

            i = 0;
            do
            {
              tmp = a[n - i - 1];
              a[n - i - 1] = a[n - j - 1];
              a[n - j - 1] = tmp;
            } while (++i < --j);
          }
        }
      }

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
