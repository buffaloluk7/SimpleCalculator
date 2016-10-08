# Installation
1. cd src/SimpleCalculator
2. dotnet restore
3. dotnet build
4. dotnet run

# Supported runtimes
* osx.10.11-x64
* win8-x64
* win10-x64
* ubuntu.14.04-x64
* ubuntu.16.04-x64
* debian.8-x64

# Publish for runtimes
```
dotnet publish -r runtime-goes-here
#e.g.
dotnet publish -r win10-x64 -c Release
```
Ouptut contained in bin/{configuration}/netcoreapp1.0/{runtime}/publish
