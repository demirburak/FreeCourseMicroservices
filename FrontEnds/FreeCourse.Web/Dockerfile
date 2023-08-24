FROM mcr.microsoft.com/dotnet/sdk:7.0-bookworm-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/FreeCourse.Shared/*.csproj Shared/FreeCourse.Shared/
COPY Shared/FreeCourse.SharedCore7/*.csproj Shared/FreeCourse.SharedCore7/

COPY FrontEnds/FreeCourse.Web/*.csproj FrontEnds/FreeCourse.Web/
RUN dotnet restore FrontEnds/FreeCourse.Web/*.csproj
COPY . .
RUN dotnet publish FrontEnds/FreeCourse.Web/*.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FreeCourse.Web.dll" ]