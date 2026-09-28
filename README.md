# BinaryConverter

A C# console application that converts between binary and decimal numbers — both single 8-bit octets and full 4-octet addresses (like IPv4 addresses).

Built as part of **Projekt 2 – Programmering: "Binær kodeomformer"** at Mercantec (GF2, Datatekniker – Programmering).

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## How to run

```bash
git clone https://github.com/ruslanaonshchk/BinaryConverter.git
cd BinaryConverter/BinaryConverter
dotnet run
```

Or from the repository root:

```bash
dotnet run --project BinaryConverter
```

## Menu

```
1. Binary to decimal
2. Decimal to binary
3. Binary address to decimal
4. Decimal address to binary
0. Exit
```

## Examples

### Single octet (8 bits)

| Input | Option | Output |
|---|---|---|
| `10111011` | 1 | `187` |
| `01001011` | 1 | `75` |
| `187` | 2 | `10111011` |
| `75` | 2 | `01001011` |

### Full address (4 octets)

| Input | Option | Output |
|---|---|---|
| `10111011.01001011.10101010.01010101` | 3 | `187.75.170.85` |
| `187.75.170.85` | 4 | `10111011.01001011.10101010.01010101` |

### Example session

```
Binary Converter

1. Binary to decimal
2. Decimal to binary
3. Binary address to decimal
4. Decimal address to binary
0. Exit
Choose an option: 3
Enter a binary address (e.g. 10111011.01001011.10101010.01010101): 10111011.01001011.10101010.01010101
10111011.01001011.10101010.01010101 = 187.75.170.85

1. Binary to decimal
2. Decimal to binary
3. Binary address to decimal
4. Decimal address to binary
0. Exit
Choose an option: 0
Goodbye!
```

## How it works

Each bit in an octet has a place value:

| Bit position | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 |
|---|---|---|---|---|---|---|---|---|
| Place value | 128 | 64 | 32 | 16 | 8 | 4 | 2 | 1 |

**Binary → decimal** (`BinaryToDecimal`): go through the bits from left to right and add the place value of every bit that is `1`.

`10111011` → 128 + 32 + 16 + 8 + 2 + 1 = **187**

**Decimal → binary** (`DecimalToBinary`): go through the place values from 128 down to 1. If the place value fits into the remaining number, write `1` and subtract it; otherwise write `0`. The loop always runs 8 times, so leading zeros are added automatically.

`75` → 0·128 + 1·64 + 0·32 + 0·16 + 1·8 + 0·4 + 1·2 + 1·1 = **01001011**

**Full address**: the address is split into 4 parts with `Split('.')`, each part is validated and converted with the single-octet methods above, and the results are joined back together with dots.

## Input validation

- **Binary octet:** exactly 8 characters, only `0` and `1`.
- **Decimal octet:** digits only, value from 0 to 255 (no signs, no decimals).
- **Address:** exactly 4 octets separated by dots, each valid on its own.
- Spaces at the start and end of the input are ignored; spaces inside the input are not allowed.
- On invalid input the program shows what is wrong and asks again. Press **Enter** on an empty line to go back to the menu.

## Test cases

| Option | Input | Expected result |
|---|---|---|
| 1 | `00000000` | `0` (edge case) |
| 1 | `11111111` | `255` (edge case) |
| 1 | `1011` | invalid — too short |
| 1 | `10121011` | invalid — contains `2` |
| 2 | `0` | `00000000` (edge case) |
| 2 | `255` | `11111111` (edge case) |
| 2 | `256` | invalid — out of range |
| 2 | `-1` | invalid — not a whole number from 0 to 255 |
| 2 | `abc` | invalid — not a number |
| 3 | `00000000.00000000.00000000.00000000` | `0.0.0.0` |
| 3 | `11111111.11111111.11111111.11111111` | `255.255.255.255` |
| 3 | `10111011.01001011.10101010` | invalid — only 3 octets |
| 3 | `10111011.01001011.10101010.01010101.` | invalid — trailing dot |
| 4 | `0.0.0.0` | `00000000.00000000.00000000.00000000` |
| 4 | `255.255.255.255` | `11111111.11111111.11111111.11111111` |
| 4 | `187.300.170.85` | invalid — 300 is out of range |
| 4 | `187. 75.170.85` | invalid — space inside the address |

## Assignment constraints

- No built-in binary conversion is used (no `Convert.ToString(…, 2)` or `Convert.ToInt32(…, 2)`). All binary logic is written with `if/else` and loops.
- `int.TryParse` / `int.Parse` are only used to read decimal text (e.g. `"187"`) as a number — they do not do any binary conversion.
- Method names are in English. They match the methods from the assignment:

| Assignment | This project |
|---|---|
| `int BinaerTilDecimal(string binaer)` | `int BinaryToDecimal(string binary)` |
| `string DecimalTilBinaer(int decimal)` | `string DecimalToBinary(int number)` |

The parameter is named `number` because `decimal` is a reserved keyword in C#.

## Project structure

```
BinaryConverter/
├── BinaryConverter.slnx
├── README.md
└── BinaryConverter/
    ├── BinaryConverter.csproj
    └── Program.cs
```
