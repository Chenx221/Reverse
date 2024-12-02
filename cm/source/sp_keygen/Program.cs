using System.Globalization;
using System.Text;

class Program
{
    static void Main()
    {
        CalcSerial();
        Console.ReadKey();
    }

    public static void CalcSerial()
    {
        StringBuilder result = new();
        Random random = new();
        do
        {
            result.Clear();
            string[] prefix = ["1A", "3A", "1C", "3C"];
            string[] suffix = ["2A", "1B", "2B", "3B", "2C"];
            while (result.Length < 64)
            {
                // 从prefix数组中随机选取一个元素
                int prefixIndex = random.Next(prefix.Length);
                string prefixSelected = prefix[prefixIndex];

                // 从suffix数组中随机选取一个元素
                int suffixIndex = random.Next(suffix.Length);
                string suffixSelected = suffix[suffixIndex];

                // 将选中的元素交换
                prefix[prefixIndex] = suffixSelected;
                suffix[suffixIndex] = prefixSelected;

                // 组合选中的两个元素并添加到结果
                result.Append(prefixSelected);
                result.Append(suffixSelected);
            }
        } while(!Check(result.ToString()));

        // 打印结果
        Console.WriteLine(result.ToString());
    }

    public static bool Check(string serial)
    {
        int[,] trueData = {
            { 1,0,1 },
            { 0,0,0 },
            { 2,0,2 }
        };
        int[,] Data = {
            { 2,0,2 },
            { 0,0,0 },
            { 1,0,1 }
        };
        //EX: 1A2A3A1A2A3A 1C2C3C1C2C3C //24
        //EX: 1A3B3B1A1A3B3B1A1A3B3B1A1A3B3B1A1A3B3B1A //40
        if (serial.Length != 64)
            return false;
        for (int i = 0; i < 16; i++)
        {
            int y1 = serial[i * 4] - '1';
            int x1 = serial[i * 4 + 1] - 'A';
            int y2 = serial[i * 4 + 2] - '1';
            int x2 = serial[i * 4 + 3] - 'A';            
            Data[y2, x2] = Data[y1, x1];
            Data[y1, x1] = 0;
        }
        if (Data[0, 0] == 1 && Data[0, 2] == 1 && Data[2, 0] == 2 && Data[2, 2] == 2)
            return true;
        return false;
    }




}
