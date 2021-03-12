cd RemoteController
dotnet build -c Release
cd ..
cd RemoteWebServer
cls
node index.js ../RemoteController\bin\Release\net5.0\RemoteController.exe