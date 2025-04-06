# HTA-TO-EXE - GUI PACKER

This project provides a template to pack [HTA applications](https://en.wikipedia.org/wiki/HTML_Application) into a single executable.

<img src="https://i.ibb.co/cKxktysK/ccccb.png"></img>

## Features
* Simulated router navigation
* Small application size (20kb)
* Improved browser functions
* Extreme low memory usage
* Secure code bundle

## Requirements

* [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet)

## Where to place HTA files
* The `app` folder must contain your HTML application.

## How to Build an Executable
* After making changes in the `app` folder, build the app:
```
dotnet publish -c Release
```

> Output folder: `bin/Release/net47/win-x64/publish` 

## &nbsp;
⭐ If you find this useful!
