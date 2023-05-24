# Base image with the ASP.NET 7 SDK
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory
WORKDIR /app

# Copy the project files
COPY ./VodafoneCashApi .

# Restore the NuGet packages
RUN dotnet restore
RUN mkdir /db
RUN dotnet tool restore
RUN dotnet ef database update

# Publish the app
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the published app from the build image
COPY --from=build /app/out .
COPY --from=build /db /db

# Install SQLite runtime dependencies
RUN  apt-get -y update
RUN  apt-get -y upgrade
RUN  apt-get install -y sqlite3 libsqlite3-dev


# Set the entry point
CMD ["dotnet", "VodafoneCashApi.dll"]
