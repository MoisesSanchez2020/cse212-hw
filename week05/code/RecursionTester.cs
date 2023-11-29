
public static class RecursionTester
{

     /// <summary>
    /// Entry point for the Prove 8 tests
    /// </summary>
    public static void Run()
    {
      // Sample Test Cases (may not be comprehensive) 
        Console.WriteLine("\n=========== PROBLEM 1 TESTS ===========");
        Console.WriteLine(SumSquaresRecursive(10)); // 385
        Console.WriteLine(SumSquaresRecursive(100)); // 338350

        // Sample Test Cases (may not be comprehensive) 
        Console.WriteLine("\n=========== PROBLEM 2 TESTS ===========");
        PermutationsChoose("ABCD", 3);
        // Expected Result (order may be different):
        // ABC
        // ABD
        // ACB
        // ACD
        // ADB
        // ADC
        // BAC
        // BAD
        // BCA
        // BCD
        // BDA
        // BDC
        // CAB
        // CAD
        // CBA
        // CBD
        // CDA
        // CDB
        // DAB
        // DAC
        // DBA
        // DBC
        // DCA
        // DCB

        Console.WriteLine("---------");
        PermutationsChoose("ABCD", 2);
        // Expected Result (order may be different):
        // AB
        // AC
        // AD
        // BA
        // BC
        // BD
        // CA
        // CB
        // CD
        // DA
        // DB
        // DC

        Console.WriteLine("---------");
        PermutationsChoose("ABCD", 1);
        // Expected Result (order may be different):
        // A
        // B
        // C
        // D

       // Sample Test Cases (may not be comprehensive) 
        Console.WriteLine("\n=========== PROBLEM 3 TESTS ===========");
        Console.WriteLine(CountWaysToClimb(5)); // 13
        Console.WriteLine(CountWaysToClimb(20)); // 121415
        // Uncomment out the test below after implementing memoization.  It won't work without it.
        // TODO Problem 3
        // Console.WriteLine(CountWaysToClimb(100));  // 180396380815100901214157639

        // Problem 4 Tests
        Console.WriteLine("\n=========== PROBLEM 4 TESTS ===========");
        WildcardBinary("110*0*");
        WildcardBinary("***");
    }

    public static int SumSquaresRecursive(int n)
    {
        if (n <= 0) return 0;
        return n * n + SumSquaresRecursive(n - 1);
    }

    public static void PermutationsChoose(string letters, int size, string word = "")
    {
        if (size == 0)
        {
            Console.WriteLine(word);
            return;
        }

        for (int i = 0; i < letters.Length; i++)
            PermutationsChoose(letters.Remove(i, 1), size - 1, word + letters[i]);
    }

    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        if (remember == null)
            remember = new Dictionary<int, decimal>();

        if (remember.ContainsKey(s))
            return remember[s];

        if (s <= 0)
            return 0;
        if (s == 1)
            return 1;
        if (s == 2)
            return 2;
        if (s == 3)
            return 4;

        remember[s] = CountWaysToClimb(s - 1, remember) + CountWaysToClimb(s - 2, remember) + CountWaysToClimb(s - 3, remember);
        return remember[s];
    }

    public static void WildcardBinary(string pattern)
    {
        WildcardBinaryRecursive(pattern, 0);
    }

    private static void WildcardBinaryRecursive(string pattern, int index)
    {
        if (index == pattern.Length)
        {
            Console.WriteLine(pattern);
            return;
        }

        if (pattern[index] == '*')
        {
            foreach (char c in new char[] { '0', '1' })
            {
                WildcardBinaryRecursive(pattern.Substring(0, index) + c + pattern.Substring(index + 1), index + 1);
            }
        }
        else
        {
            WildcardBinaryRecursive(pattern, index + 1);
        }
    }

    // Problem 5 method placeholder (not implemented)
    public static void SolveMaze(Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // Implementation for Problem 5
    }
}
