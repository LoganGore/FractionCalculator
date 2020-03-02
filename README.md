# Fraction Calculator Program
This program will perform basic math operations on fraction numbers. 

## Program Info
- Interface: Command Line
- Language: C#
- Runtime: .NET Core 3.1
- Containerization: Docker
- Memory footprint: 'around' 30MB, but this will vary on length of input.

## How to build the program
First, clone this repo.

Docker is the recommended build/run method.

**Either method allows running the program via command-line argument input, or via interactive console.

### Build/Run with Docker (easy)
- Download and install docker if necessary: https://www.docker.com/get-started
- cd to this repos root directory where the `Dockerfile` is located.
- Run `docker build -t fraction -f Dockerfile .`
- You can invoke the program with command-line arguments 
  like this: `docker run fraction 1/64 + 2/3`.
- Alternatively, you can run the program as an interactive console, by just running `docker run -i fraction`,
 then supplying the expressions when prompted. 

### Build/Run Locally with .NET (semi-easy...)
- Install .NET Core 3.1 on your machine: https://dotnet.microsoft.com/download/dotnet-core/3.1
- cd to the root directory of this repo and run `dotnet restore` ,then `dotnet build`, then `dotnet publish -c Release -f netcoreapp3.1`. Reference this link for info on the publish command, or for architecture-specific publishing https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish.
- cd to the publish directory of the console application via `cd FractionCalculator/bin/Release/netcoreapp3.1/publish
- Invoke the program with its DLL: `dotnet FractionCalculator.App.dll 1/64 + 2/3`
- Alternatively, you can run the program as an interactive console by just running `dotnet FractionCalculator.App.dll`,
 then supplying the expressions when prompted. 

## Correct Input/Argument Formatting
Correct format for arguments: `2_1/2 - 1/124`, or `2 / 1/2`, or `-5_3/5 * -1/4`.

Rules:
- Only +,-,/,* are valid math operators. **Some CLIs will need escaping of the "*" operator. 
- No spaces in fractions. i.e. 2 /3 is invalid as 2/3.
- Math operators must be separated from fractions by a space. e.g., 2/4 +2/9 is invalid; 2/4 + 2/9 is correct.
- Negatives must be attached as a "-" to the furthest left part of the fraction. -2_3/4 is valid. 2_-3/4 is invalid.
- Proper separators: Fractions must be represented using a 'fraction separator' value (default is /), and
a mixed number separator value (default is \_). These can be configured. See later section on configuration.
- Left to right execution. No order of operations is enforced. 

### Examples
- 2 + 1          -> 3
- 2_1/2 - 1/2    -> 2
- -2_3/12 + 4/8  -> -1_3/4
- 64/4 \* -3 + 9 -> -39

## Configuration in `appsettings.json` file
Numerical separators and athmatical operators can also be configured through the `appsettings.json` file of the project by setting the respective JSON
values.
Current file is set to:
`{
  "fraction_separator": "/",
  "whole_separator": "_",
  "multiplication_operator": "*",
  "subtraction_operator": "-",
  "division_operator": "/",
  "addition_operator": "+",
  "negative_character": "-"
}`

## Solution Structure
The solution is broken down into three pieces:
1. The console app
2. The fraction calculator library
3. The fraction calculator unit tests

The solution also contains a docker and README file at the root directory. 

## Unit Tests
The program contains a unit tests project that can be executed in Visual Studio or via a `dotnet test` in the root
solution directory.

## Resources
-If you want a dedicated EXE for your architecture:
https://dzone.com/articles/generate-an-exe-for-net-core-console-apps-net-core

