# Create the build environment image
FROM mcr.microsoft.com/dotnet/core/runtime:3.1
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build
WORKDIR /app
 
# Copy the project file and restore the dependencies
COPY FractionCalculator/*.csproj ./
RUN dotnet restore
 
# Copy the remaining source files and build the application
COPY . ./
FROM build AS publish
RUN dotnet publish FractionCalculator.sln -c Release -o out
 
# Build the runtime image
FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /app
COPY --from=publish /app/out .
ENTRYPOINT ["dotnet", "FractionCalculator.App.dll"]