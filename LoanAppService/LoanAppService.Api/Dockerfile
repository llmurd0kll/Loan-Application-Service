FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["LoanAppService.Api/LoanAppService.Api.csproj", "LoanAppService.Api/"]
RUN dotnet restore "LoanAppService.Api/LoanAppService.Api.csproj"
COPY . .
WORKDIR "/src/LoanAppService.Api"
RUN dotnet build "LoanAppService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoanAppService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoanAppService.Api.dll"]
