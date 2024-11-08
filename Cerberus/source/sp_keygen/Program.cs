namespace sp_keygen
{
    internal class Program
    {
        static void Main()
        {
            Console.Write("Name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Please input name.");
                return;
            }
            else if (name.Length <= 4)
            {
                Console.WriteLine("Name must be longer than 4 characters.");
                return;
            }
            Console.WriteLine($"Serial: {CalcSerial(name)}");
            Console.ReadKey();
        }

        public static string CalcSerial(string name)
        {
            int name_length = name.Length;
            int esi = 0x18;
            int ebx = 0x400;
            int ebp = 0x32;
            int ecx = name_length;
            int eax, edx, edi, v10 = 0;
            foreach (char c in name)
            {
                eax = c;
                eax += 0x56B;
                edx = eax;
                edx ^= 0x890428;
                esi += edx;
                edx = name[3];
                if (ecx <= 9)
                {
                    edx += ecx;
                    edx ^= 0x209;
                    edx *= esi;
                    ebx += edx;
                }
                else
                {
                    edx += ecx;
                    edx ^= 0x209;
                    edx *= ebx;
                    esi += edx;
                }
                edx = eax;
                edx <<= 7;
                edx += eax;
                eax += edx * 8;
                v10 = ebx + eax * 4;
                ebx = v10; //first loop not run
            }
            string temp = name;
            for (edi = 5; edi > 0; edi--)
            {
                eax = temp[edi];
                ebp = eax + ebp + 0x134A;
                temp = Fun_403c80(temp);
            }
            string temp2 = temp; //edx
            edi = 1;
            for (ebx = 5; ebx > 0; ebx--)
            {
                ecx = temp2[0];
                eax = temp2[edi];
                eax += 0x23;
                ebp += ecx + 0x134A;
                ecx = eax * 3;
                ecx *= 5;
                edx = ecx * 5;
                eax += edx * 4;
                esi += eax * 2;
                temp2 = Fun_403c80(temp2);
                edi++;
            }
            edx = temp2[5];
            eax = 0x18;
            ecx = v10;
            eax -= edx;
            ebp += ecx;
            edx = temp2[2];
            eax ^= ebp;
            esi += 0x3c;
            int eax2 = 0x1337;
            eax2 -= edx;
            eax2 ^= esi;
            return $"LNT-{eax2}-{eax}"; //user32._wsprintfA(edx,"%s-%d-%d","LNT",eax2,eax); //esi
        }

        //反转字符串
        public static string Fun_403c80(string name)
        {
            return new string(name.Reverse().ToArray());
        }
    }
}
