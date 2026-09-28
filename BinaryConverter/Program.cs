using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Binary Converter");

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1. Binary to decimal");
            Console.WriteLine("2. Decimal to binary");
            Console.WriteLine("3. Binary address to decimal");
            Console.WriteLine("4. Decimal address to binary");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");
            string? choice = Console.ReadLine()?.Trim();

            if (choice == null || choice == "0")
            {
                break;
            }
            else if (choice == "1")
            {
                RunBinaryToDecimal();
            }
            else if (choice == "2")
            {
                RunDecimalToBinary();
            }
            else if (choice == "3")
            {
                RunBinaryAddressToDecimal();
            }
            else if (choice == "4")
            {
                RunDecimalAddressToBinary();
            }
            else
            {
                Console.WriteLine("Invalid choice. Enter 1, 2, 3, 4 or 0.");
            }
        }

        Console.WriteLine("Goodbye!");
    }

    static void RunBinaryToDecimal()
    {
        while (true)
        {
            Console.Write("Enter an 8-bit binary number (e.g. 10111011): ");
            string? input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            if (IsValidBinary(input))
            {
                Console.WriteLine($"{input} = {BinaryToDecimal(input)}");
                return;
            }

            Console.WriteLine("Invalid input. Use exactly 8 characters, only 0 and 1.");
            Console.WriteLine("Try again, or press Enter to go back to the menu.");
        }
    }

    static void RunDecimalToBinary()
    {
        while (true)
        {
            Console.Write("Enter a decimal number from 0 to 255 (e.g. 187): ");
            string? input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            if (TryParseOctet(input, out int number))
            {
                Console.WriteLine($"{number} = {DecimalToBinary(number)}");
                return;
            }

            Console.WriteLine("Invalid input. Enter a whole number from 0 to 255.");
            Console.WriteLine("Try again, or press Enter to go back to the menu.");
        }
    }

    static void RunBinaryAddressToDecimal()
    {
        while (true)
        {
            Console.Write("Enter a binary address (e.g. 10111011.01001011.10101010.01010101): ");
            string? input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            if (IsValidBinaryAddress(input))
            {
                Console.WriteLine($"{input} = {BinaryAddressToDecimal(input)}");
                return;
            }

            Console.WriteLine("Invalid input. Use 4 groups of 8 binary digits separated by dots.");
            Console.WriteLine("Try again, or press Enter to go back to the menu.");
        }
    }

    static void RunDecimalAddressToBinary()
    {
        while (true)
        {
            Console.Write("Enter a decimal address (e.g. 187.75.170.85): ");
            string? input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            if (IsValidDecimalAddress(input))
            {
                Console.WriteLine($"{input} = {DecimalAddressToBinary(input)}");
                return;
            }

            Console.WriteLine("Invalid input. Use 4 numbers from 0 to 255 separated by dots.");
            Console.WriteLine("Try again, or press Enter to go back to the menu.");
        }
    }

    static bool IsValidBinary(string binary)
    {
        if (binary.Length != 8)
        {
            return false;
        }

        foreach (char character in binary)
        {
            if (character != '0' && character != '1')
            {
                return false;
            }
        }

        return true;
    }

    static int BinaryToDecimal(string binary)
    {
        int result = 0;
        int placeValue = 128;

        for (int i = 0; i < binary.Length; i++)
        {
            if (binary[i] == '1')
            {
                result += placeValue;
            }

            placeValue /= 2;
        }

        return result;
    }

    static string DecimalToBinary(int number)
    {
        string result = "";
        int placeValue = 128;

        for (int i = 0; i < 8; i++)
        {
            if (number >= placeValue)
            {
                result += "1";
                number -= placeValue;
            }
            else
            {
                result += "0";
            }

            placeValue /= 2;
        }

        return result;
    }

    static bool TryParseOctet(string text, out int number)
    {
        number = 0;

        foreach (char character in text)
        {
            if (character < '0' || character > '9')
            {
                return false;
            }
        }

        if (!int.TryParse(text, out number))
        {
            return false;
        }

        if (number > 255)
        {
            return false;
        }

        return true;
    }

    static bool IsValidBinaryAddress(string address)
    {
        string[] parts = address.Split('.');

        if (parts.Length != 4)
        {
            return false;
        }

        foreach (string part in parts)
        {
            if (!IsValidBinary(part))
            {
                return false;
            }
        }

        return true;
    }

    static string BinaryAddressToDecimal(string address)
    {
        string[] parts = address.Split('.');
        string result = "";

        for (int i = 0; i < parts.Length; i++)
        {
            result += BinaryToDecimal(parts[i]);

            if (i < parts.Length - 1)
            {
                result += ".";
            }
        }

        return result;
    }

    static bool IsValidDecimalAddress(string address)
    {
        string[] parts = address.Split('.');

        if (parts.Length != 4)
        {
            return false;
        }

        foreach (string part in parts)
        {
            if (!TryParseOctet(part, out _))
            {
                return false;
            }
        }

        return true;
    }

    static string DecimalAddressToBinary(string address)
    {
        string[] parts = address.Split('.');
        string result = "";

        for (int i = 0; i < parts.Length; i++)
        {
            int number = int.Parse(parts[i]);
            result += DecimalToBinary(number);

            if (i < parts.Length - 1)
            {
                result += ".";
            }
        }

        return result;
    }
}