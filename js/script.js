
var fs, 
    shell,
    fileContent, 
    fileContent2, 
    appName = "MyApplication",
    folderName = "";


window.onload = function () {

    document.getElementById("build").disabled = true;
    document.getElementById("nameInput").value = appName;

    fs = new ActiveXObject("Scripting.FileSystemObject");
    shell = new ActiveXObject("WScript.Shell");

    var tmpFolder = fs.GetParentFolderName(window.location.pathname);

    var filePath = tmpFolder + "\\ext\\cs.txt";
    if (fs.FileExists(filePath)) {
        var file = fs.OpenTextFile(filePath, 1);
        fileContent = file.ReadAll();
        file.Close();
    }

    var filePath2 = tmpFolder + "\\ext\\proj.txt";
    if (fs.FileExists(filePath2)) {
        var file = fs.OpenTextFile(filePath2, 1);
        fileContent2 = file.ReadAll();
        file.Close();
    }
}


function openFolderPicker() {
    document.getElementById("folderInput").value = "";
    document.getElementById("build").disabled = true;

    var folderPicker = new ActiveXObject("Shell.Application").BrowseForFolder(0, "Select a folder", 0)
    if (folderPicker) {
        var folderPath = folderPicker.Items().Item().Path;
        document.getElementById("folderInput").value = folderPath;
        document.getElementById("build").disabled = false;
    } else {
        document.getElementById("folderInput").value = "";
        document.getElementById("build").disabled = true;
    }
}


function buildExecutable() {
    
    document.getElementById("built").style.display = "none";
    document.getElementById("build").style.display = "none";
    document.getElementById("building").style.display = "flex";
    document.getElementById("nameInput").disabled = true;
    document.getElementById("folderInput").disabled = true;
    document.getElementById("built").content = "";

    var tempDir = (fs.GetSpecialFolder(2) + "\\HTA_Temp_" + new Date().getTime());
    fs.CreateFolder(tempDir);

    try{

        appName = document.getElementById("nameInput").value
        folderName = document.getElementById("folderInput").value

        if (!appName) {
            throw new Error("Missing \"Application Name\".");
        }

        if (!fs.FileExists(folderName+ "\\gui.hta")) {
            throw new Error("gui.hta not found in the selected folder.");
        }

        var command = "cmd.exe /C robocopy \"" + folderName + "\" \"" + tempDir + "\\app\" /mir";
        shell.Run(command, 0, true);

        var updatedCsContent = fileContent.replace("TEMP_NAME", appName + "_" + new Date().getTime());
        var tempFile = fs.CreateTextFile(tempDir + "\\Program.cs", true);
        tempFile.Write(updatedCsContent);
        tempFile.Close();

        var updatedCsprojContent = fileContent2.replace("APPNAME", appName);
        var tempFile2 = fs.CreateTextFile(tempDir + "\\apphta.csproj", true);
        tempFile2.Write(updatedCsprojContent);
        tempFile2.Close();

        var output = (window.location.href.split("?path=")[1] || ".\\") + "output";

        var command = "cmd.exe /C dotnet publish \"" + tempDir.replace(/\\/g, "\\\\") + "\" -c Release -o \"" + output.replace(/\\/g, "\\\\") + "\"";
        shell.Run(command, 0, true);

        fs.DeleteFolder(tempDir, true);

        var filePath = output + "\\" + appName + ".exe.config";
        if (fs.FileExists(filePath)) fs.DeleteFile(filePath);

        document.getElementById("built").style.color = "rgb(0,150,0)";
        document.getElementById("built").innerText = "Success";

    }catch(err){
        document.getElementById("built").style.color = "rgb(200,0,0)";
        document.getElementById("built").innerText = "Error: " + err.message;
        fs.DeleteFolder(tempDir, true);
    }
    
    document.getElementById("built").style.display = "flex";
    document.getElementById("build").style.display = "flex";
    document.getElementById("building").style.display = "none";
    document.getElementById("nameInput").disabled = false;
    document.getElementById("folderInput").disabled = false;
    
}