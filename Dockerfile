#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./TweetBook/TweetBook.csproj" --disable-parallel
RUN dotnet publish "./TweetBook/TweetBook.csproj" -c release -o /app --no-restore

#Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5001

ENTRYPOINT ["dotnet", "TweetBook.dll"] 
