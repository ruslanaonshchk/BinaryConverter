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
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");
            string? choice = Console.ReadLine();

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
            else
            {
                Console.WriteLine("Invalid choice. Enter 1, 2 or 0.");
            }
        }

        Console.WriteLine("Goodbye!");
    }

    static void RunBinaryToDecimal()
    {
        Console.Write("Enter an 8-bit binary number: ");
        string? binaryInput = Console.ReadLine();

        if (binaryInput != null && IsValidBinary(binaryInput))
        {
            Console.WriteLine($"{binaryInput} = {BinaryToDecimal(binaryInput)}");
        }
        else
        {
            Console.WriteLine("Invalid input. Use exactly 8 characters, only 0 and 1.");
        }
    }

    static void RunDecimalToBinary()
    {
        Console.Write("Enter a decimal number (0-255): ");
        string? decimalInput = Console.ReadLine();

        if (decimalInput != null && TryParseOctet(decimalInput, out int number))
        {
            Console.WriteLine($"{number} = {DecimalToBinary(number)}");
        }
        else
        {
            Console.WriteLine("Invalid input. Enter a whole number from 0 to 255.");
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
        if (!int.TryParse(text, out number))
        {
            return false;
        }

        if (number < 0 || number > 255)
        {
            return false;
        }

        return true;
    }
}