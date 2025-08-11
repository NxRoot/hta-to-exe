# HTA-TO-EXE - GUI PACKER

This project provides a template to pack [HTA applications](https://en.wikipedia.org/wiki/HTML_Application) into a single executable.

<img src="https://i.ibb.co/cKxktysK/ccccb.png"></img>

<img src="https://i.ibb.co/bg50nkGS/Captura-de-ecr-2025-04-06-214352.png"></img>

## Requirements

* [Microsoft .NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet) to build executables.

## How to Install
<a href="https://github.com/NxRoot/hta-to-exe/releases">
<img src="https://i.ibb.co/v4tLGTdP/button.png"></img>
</a>
<br></br>

## HTA Tutorials

> The app folder exists for 5 seconds before it gets deleted.<br>
> To access a file that is packed inside the app you need to read it on the `window.onload` event.

#### Access File System
```jsx
var fs = new ActiveXObject("Scripting.FileSystemObject");
```
#### Get App Folder
```jsx
var tmpFolder = fs.GetParentFolderName(window.location.pathname);
```
#### Get Exe Folder
```jsx
var exeFolder = window.location.href.split("?path=")[1];
```
#### Read File
```jsx
var fileContent = "";
var filePath = tmpFolder + "\\file_packed_inside_app.json";

if (fs.FileExists(filePath)) {
  var file = fs.OpenTextFile(filePath, 1);
  fileContent = JSON.parse(file.ReadAll());
  file.Close();
}
```
#### Write File
```jsx
var data = { message: "Hello World!" }
var file = fs.CreateTextFile(exeFolder + "\\config.json", true);
file.Write(JSON.stringify(data, null, 4));
file.Close();
```

#### Access Shell System
```jsx
var shell = new ActiveXObject("WScript.Shell");
```
#### Run Shell Commands
```jsx
shell.Run("cmd.exe /C start tree", 1, true);
```

#### Folder Picker
```jsx
var picker = new ActiveXObject("Shell.Application").BrowseForFolder(0, "Select a folder", 0);

if (picker){
  var folder = picker.Items().Item().Path;
  alert("Selected Folder: " + folder)
}
```


## &nbsp;
⭐ If you find this useful!
