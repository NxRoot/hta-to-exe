# HTA-TO-EXE - GUI PACKER

This project provides a template to pack [HTA applications](https://en.wikipedia.org/wiki/HTML_Application) into a single executable.

<img src="https://i.ibb.co/cKxktysK/ccccb.png"></img>

<img src="https://i.ibb.co/bg50nkGS/Captura-de-ecr-2025-04-06-214352.png"></img>

## Features
* Simulated router navigation
* Small application size (20kb)
* Improved browser functions
* Extreme low memory usage
* Secure code bundle

## How to Install
| Exe    | Description | Releases |
| -------- | ------- | ------- |
| <a href="https://github.com/NxRoot/hta-to-exe/releases"><img style="min-width: 40px;min-height: 40px; width: 40px;" src="https://i.ibb.co/xtk0drwX/fav.png"/></a> | Download the latest version   | [Download](https://github.com/NxRoot/hta-to-exe/releases)    |

## Requirements

* [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet)

## Build manually from source
* The `app` folder must contain your HTML application.
```py
# Clone this repo
git clone https://github.com/NxRoot/hta-to-exe.git

# go to folder
cd hta-to-exe

# Generate executable
dotnet publish -c Release
```

> Output folder: `bin/Release/net47/win-x64/publish` 

## &nbsp;
⭐ If you find this useful!
