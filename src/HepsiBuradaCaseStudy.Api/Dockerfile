#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/HepsiBuradaCaseStudy.Api/HepsiBuradaCaseStudy.Api.csproj", "src/HepsiBuradaCaseStudy.Api/"]
COPY ["src/HepsiBuradaCaseStudy.Business/HepsiBuradaCaseStudy.Business.csproj", "src/HepsiBuradaCaseStudy.Business/"]
COPY ["src/HepsiBuradaCaseStudy.Domain/HepsiBuradaCaseStudy.Domain.csproj", "src/HepsiBuradaCaseStudy.Domain/"]
COPY ["src/HepsiBuradaCaseStudy.Infrastructure/HepsiBuradaCaseStudy.Infrastructure.csproj", "src/HepsiBuradaCaseStudy.Infrastructure/"]
RUN dotnet restore "src/HepsiBuradaCaseStudy.Api/HepsiBuradaCaseStudy.Api.csproj"
COPY . .
WORKDIR "/src/src/HepsiBuradaCaseStudy.Api"
RUN dotnet build "HepsiBuradaCaseStudy.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HepsiBuradaCaseStudy.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HepsiBuradaCaseStudy.Api.dll"]
