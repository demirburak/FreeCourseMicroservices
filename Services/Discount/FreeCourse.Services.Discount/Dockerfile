FROM mcr.microsoft.com/dotnet/sdk:7.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/FreeCourse.Shared/*.csproj Shared/FreeCourse.Shared/
COPY Shared/FreeCourse.SharedCore7/*.csproj Shared/FreeCourse.SharedCore7/
COPY Services/Discount/FreeCourse.Services.Discount/*.csproj Services/Discount/FreeCourse.Services.Discount/
RUN dotnet restore Services/Discount/FreeCourse.Services.Discount/*.csproj
COPY . .
RUN dotnet publish Services/Discount/FreeCourse.Services.Discount/*.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FreeCourse.Services.Discount.dll" ]