using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Binary Converter");
        
        Console.WriteLine("Enter an 8-bit binary number:");
        string? input = Console.ReadLine();
        
        if (input != null && IsValidBinary(input))
        {
            int decimalValue = BinaryToDecimal(input);
            Console.WriteLine($"{input} = {decimalValue}");
        }
        else
        {
            Console.WriteLine("Invalid input. Use exactly 8 characters, only 0 and 1.");
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
}