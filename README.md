# Permutations

Generating all N! permutations of the numbers 1 to N-1.

- [Algorithms](#Algorithms)
- [Performance](#Performance)
- [Usage](#Usage)
- [Restrictions](#Restrictions)

## Algorithms

This repository provides C# implementations for three well-known algorithms:

- *Trotter*'s algorithm (1962)
- *Heap*'s algorithm (1963)
- *Ord-Smith*'s algorithm (1968)

All implementations are non-recursive and take advantage of the **yield** keyword,
i.e. each permutation is calculated only when requested.
*Ord-Smith*'s algorithm is also lexicographically ordered.
 
 Algorithm | Non-recursive | Iterated | Ordered
:---------:|:-------------:|:--------:|:-------:
 Trotter   | yes           | yes      | no 
 Heap      | yes           | yes      | no 
 Ord-Smith | yes           | yes      | yes 

## Performance

If you don't need an ordered list of permutations, *Trotter*'s algorithm would be the best choice for large N.

  N |        N! |   Passes |  Trotter |     Heap | Ord-Smith
---:|----------:|---------:|---------:|---------:|----------:
  1 |         1 | 10000000 |  1572 ms |  1477 ms |   1383 ms
  2 |         2 | 10000000 |  2059 ms |  1698 ms |   1633 ms
  3 |         6 | 10000000 |  3196 ms |  2782 ms |   2774 ms
  4 |        24 | 10000000 |  8177 ms |  7606 ms |   7555 ms
  5 |       120 |  1000000 |  2934 ms |  3202 ms |   3188 ms
  6 |       720 |   100000 |  1796 ms |  1835 ms |   1785 ms
  7 |      5040 |   100000 | 12021 ms | 12736 ms |  12525 ms
  8 |     40320 |    10000 |  8604 ms | 10125 ms |  10172 ms  
  9 |    362880 |     1000 |  8437 ms |  9125 ms |   9035 ms
 10 |   3628800 |      100 |  7445 ms |  9116 ms |   9040 ms
 11 |  39916800 |       10 |  9112 ms | 10379 ms |   9816 ms
 12 | 479001600 |        1 | 10852 ms | 11993 ms |  11735 ms

## Usage

###### Example 1:

```c#
  foreach (int[] permutation in Permutations.OrdSmith(3))
  {
    Console.WriteLine(string.Join(',', permutation));
  }
```

Output:

```
0,1,2
0,2,1
1,0,2
1,2,0
2,0,1
2,1,0
```

###### Example 2:

It also works for large arguments:

```c#
  foreach (int[] permutation in Permutations.OrdSmith(50))
  {
    Console.WriteLine(string.Join(',', permutation));
  }
```

Output:

```
0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39
0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,39,38
0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,38,37,39
0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,38,39,37
0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,39,37,38
0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,39,38,37
...
```

## Restrictions

###### Don't change the iterated array!

You're not allowed to change the permutation array returned by the iterator:

```c#
  foreach (int[] permutation in Permutations.Trotter(10))
  {
    // Don't change the array!
    Array.Fill(permutation, 0);
  }
```

The iterator directly returns the internal array of the algorithm.
It would take too much performance to return a shadow copy of the array.

###### Don't convert enumerations to collections for large N!

Although example 2 works, it's strongly recommended not to do the following for obvious reasons:

```c#
  Permutations.OrdSmith(50).ToList()
```

It would (try to) generate a list of 30414093201713378043612608166064768844377641568960512000000000000 entries.
