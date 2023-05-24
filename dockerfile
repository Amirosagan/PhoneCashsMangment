# Make Docker file for dockerize dotnet 7 and sqlite

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app
EXPOSE 80

# Copy csproj and restore as distinct layers
COPY VodafoneCashApi/*.csproj ./
RUN dotnet restore 

# Copy everything else and build
COPY ./VodafoneCashApi ./
RUN dotnet publish -c Release -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "VodafoneCashApi.dll"]

