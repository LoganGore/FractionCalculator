FROM mcr.microsoft.com/dotnet/core/runtime:3.1

COPY FractionCalculator/bin/Release/netcoreapp3.1/publish fractionCalc/

ENTRYPOINT ["dotnet", "fractionCalc/FractionCalculator.App.dll"]