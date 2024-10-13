namespace FindKey
{
    internal class Program
    {
        static void Main()
        {
            for (int i = 0; i <= 0xFFFF; i++)
            {
                if (CheckIfMatchesCondition((ushort)i))
                {
                    Console.WriteLine($"Found match: 0x{i:X4}");
                    string filePath = "knowledge.is.power";
                    using (BinaryWriter writer = new(File.Open(filePath, FileMode.Create)))
                    {
                        writer.Write((ushort)i);
                    }
                    break;
                }
            }
        }
        static bool CheckIfMatchesCondition(ushort ax)
        {
            ushort dx = 0x0000; // sum
            const ushort target = 0xA17E; // target value
            int loopCount = 0x10; // loop count
            bool cf = false; //0

            while (loopCount > 0)
            {
                bool tcf = (ax & 0x8000) != 0; // 检查高位是否为 1
                ax = (ushort)(ax << 1 | (cf ? 1 : 0)); // 左旋转 ax
                cf = tcf; // 更新进位

                tcf = (ax + dx + (cf ? 1 : 0)) > 0xFFFF; // 检查加法是否产生进位
                dx += (ushort)(ax + (cf ? 1 : 0)); // 求和
                cf = tcf; // 更新进位
                loopCount--;
            }
            return dx == target;

        }
    }
}
