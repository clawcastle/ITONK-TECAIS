FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["TECAIS.StatusReporting/TECAIS.StatusReporting.csproj", "TECAIS.StatusReporting/"]
RUN dotnet restore "TECAIS.StatusReporting/TECAIS.StatusReporting.csproj"
COPY . .
WORKDIR "/src/TECAIS.StatusReporting"
RUN dotnet build "TECAIS.StatusReporting.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TECAIS.StatusReporting.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TECAIS.StatusReporting.dll"]