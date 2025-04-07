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



<br></br>

## HTA Tutorials

> The app folder exists for only 5 seconds before it gets deleted.<br>
> To access a file that is packed inside the app you need to read it on the `window.onload` event.

#### Access File System
```jsx
var fs = new ActiveXObject("Scripting.FileSystemObject");
```
#### Get App Folder
```jsx
var tmpFolder = fs.GetParentFolderName(window.location.pathname);
```
#### Read File
```jsx
var fileContent = "";
var filePath = tmpFolder + "\\file_inside_packed_app.json";

if (fs.FileExists(filePath)) {
  var file = fs.OpenTextFile(filePath, 1);
  fileContent = JSON.parse(file.ReadAll());
  file.Close();
}
```
#### Write File
```jsx
var data = { message: "Hello World!" }
var file = fs.CreateTextFile("config.json", true);
file.Write(JSON.stringify(data, null, 4));
file.Close();
```

#### Access Shell System
```jsx
var shell = new ActiveXObject("WScript.Shell");
```
#### Run Shell Commands
```jsx
shell.Run("cmd.exe /C ls -a", 1, true);
```

#### Folder Picker
```jsx
var picker = new ActiveXObject("Shell.Application").BrowseForFolder(0, "Select a folder", 0);

if (picker){
  var folder = folderPicker.Items().Item().Path;
  console.log("Selected Folder: " + folder)
}
```

<br></br>

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
