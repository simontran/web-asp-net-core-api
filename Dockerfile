# Use the official .NET Core runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory in the container
WORKDIR /app
# Copy the contents of the build folder to the working directory in the container
COPY server-side/WebApiRestful/bin/Release/net8.0/publish .

# Expose the port your app will run on
EXPOSE 8080

# Define the command to run your application
CMD ["dotnet", "WebApiRestful.dll"]