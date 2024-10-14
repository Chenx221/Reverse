namespace FindKey
{
    internal class Program
    {
        static void Main()
        {
            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = 6
            };
            Parallel.For(0, 10000, parallelOptions, (i, state) =>
            {
                for (int j = 0; j <= 9999; j++)
                {
                    if (CheckIfMatchesCondition(i, j))
                    {
                        Console.WriteLine($"Found match: {i:D4}-{j:D4}");
                        state.Stop();
                        break;
                    }
                }
            });
        }
        static bool CheckIfMatchesCondition(int p1, int p2)
        {
            uint edx = (uint)p1;
            uint ecx = (uint)p2;
            uint ebx;
            int eax = 0xD00B;
            do
            {
                edx ^= ecx;
                ebx = (ecx << 2) + (edx >> 2);
                ecx = ebx ^ edx;
            }
            while (--eax > 0);
            return ebx == 0x6F3AAD5A;

        }
    }
}
