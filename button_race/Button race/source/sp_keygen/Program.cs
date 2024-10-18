namespace sp_keygen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 2)
            {
                Console.WriteLine("Usage: sp_keygen.exe <name> <random value>");
            }
            else
            {
                string s1 = args[1];
                string name = args[0];
                string reply = "";
                int length = Math.Min(name.Length, s1.Length);

                int vd8 = 0;
                string vd8s = "";
                string v94 = "";
                for (int i = 0; i < length; i++)
                {
                    vd8 = s1[i] * 3;
                    int vd9 = name[i] * 3;
                    int n = vd8 + vd9;
                    v94 += n.ToString("X");
                }
                for (int k = 0; k < name.Length; k++)
                {
                    vd8s += vd8.ToString()[^2..];
                }
                for (int i = 0, j = 0; i < v94.Length; i++, j++)
                {
                    reply += v94[i].ToString() + vd8s[j] + vd8s[j + 1];
                    if (i == vd8s.Length - 2)
                    {
                        j = -1;
                    }
                }
                Console.WriteLine(reply);
            }
            return;
        }
    }
}
