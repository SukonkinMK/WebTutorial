FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore ./WebTutorial.csproj
COPY . ./
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet","/app/WebTutorial.dll"]