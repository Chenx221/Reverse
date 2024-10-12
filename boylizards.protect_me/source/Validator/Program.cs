namespace Validator
{
    internal class Program
    {
        static void Main()
        {
            Console.Write("Please enter a Name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
                Console.WriteLine("Name cannot be empty.");
            else
            {
                Console.Write("Please enter a Serial: ");
                string? serial = Console.ReadLine();
                string ErrReason = "";
                if (ValidateNameSerial(name, serial ?? "", ref ErrReason))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Passed");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed: " + ErrReason);
                }
            }
            Console.ResetColor();
            Console.ReadLine();
            return;
        }

        static bool ValidateNameSerial(string name, string serial, ref string ErrReason)
        {
            int length = name.Length;
            if (length <= 7)
            {
                ErrReason = "Name length must be greater than 7";
                return false;
            }
            else
            {
                string temp2 = length + name.Remove(3, 5);
                if ((length / (double)temp2.Length) + temp2 == serial)
                    return true;
                ErrReason = "Wrong Serial";
                return false;

            }
        }
    }
}
