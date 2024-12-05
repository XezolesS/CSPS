<h1 align='center'>
    CSPS - C# Problem Solving
</h1>

<p align='center'>
    <img src='https://img.shields.io/badge/VSCode-0078D4?style=for-the-badge&logo=visual%20studio%20code&logoColor=white' />
    <img src='https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white' />
    <img src='https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white' />
</p>

<p align='center'>
    The .NET C# workspace setup for <a href='https://code.visualstudio.com/'>Visual Studio Code</a> to solve problems from online judge platforms like <a href='https://codeforces.com/'>Codeforces</a>, <a href='https://atcoder.jp/'>AtCoder</a>, <a href='https://www.acmicpc.net/'>Baekjoon Online Judge</a>, etc.
</p>

<p align='center'>
    It is intended to provide useful configurations and snippets for problem solving in C#.
</p>

<p align='center'>
Since .NET cannot compile or run single *.cs file <span style='font-size:11px'>(there are workarounds but not suitable for PS)</span>, it is annoying to build with multiple source files in the same workspace directory. With this workspace setup, you can easily disable/archive source files and compiling a single source file.
</p>

## Usage
*Instructions will be added later.*
### Basics
### Compiling and Running
### Disabling Sources
Put source files(`*.cs`) under `./Disabled`.  
The files under `./Disabled` will not be recognized by the project(`CSPS.csproj`).

### Archiving Sources
Put source files(`*.cs`) under `./ProblemCodes`.  
The files under `./ProblemCodes` will not be recognized by the project(`CSPS.csproj`).

## Requirements
- [.NET 6.0 SDK](https://dotnet.microsoft.com/ko-kr/download/dotnet/6.0)
- [Visual Studio Code](https://code.visualstudio.com/)

## Recommended VSCode Extensions
- [**C#**](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) for basic language support for C#, such as IntelliSense.
- [**Competitive Programming Helper**](https://marketplace.visualstudio.com/items?itemName=DivyanshuAgrawal.competitive-programming-helper) for running test cases easily.