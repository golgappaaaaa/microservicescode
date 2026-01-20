#FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
#WORKDIR /src
#COPY ["CustomerApi.csproj", "./"]
#RUN dotnet restore "CustomerApi.csproj"
#COPY . .
#RUN dotnet build "CustomerApi.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "CustomerApi.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "CustomerApi.dll"]

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# 1. Sirf file ka naam likho, kyunki Docker context ab sirf CustomerApi folder hai
COPY ["CustomerApi.csproj", "./"]
RUN dotnet restore "CustomerApi.csproj"

# 2. Pura code copy karein
COPY . .

# 3. Yahan WORKDIR badalne ki zaroorat nahi agar aap folder ke andar ho
RUN dotnet build "CustomerApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerApi.dll"]