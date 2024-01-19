@ECHO OFF
ECHO Building Avalonia browser project for release at "../Szakdolgozat.Browser"
cd ../../Szakdolgozat.Browser
ECHO dotnet publish -c Release --sc true
dotnet publish -c Release --sc true
ECHO Starting server...
cd ../Szakdolgozat/WASM Host
dotnet-serve.exe --directory ../../Szakdolgozat.Browser/bin/Release/net7.0/browser-wasm/AppBundle/
PAUSE