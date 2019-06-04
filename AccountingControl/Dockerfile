FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["AccountingControl.csproj", "AccountingControl/"]
RUN dotnet restore "AccountingControl/AccountingControl.csproj"
WORKDIR "/src/AccountingControl"
COPY . .
RUN dotnet build "AccountingControl.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AccountingControl.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AccountingControl.dll"]