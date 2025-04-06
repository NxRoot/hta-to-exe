using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

class Program
{
    static void Main()
    {
        try
        {
            // Retrieve all embedded resource names
            var assembly = Assembly.GetExecutingAssembly();
            var embeddedResources = assembly.GetManifestResourceNames()
                                            .Where(r => r.StartsWith("apphta.app.") && r.Contains("."))
                                            .ToArray();


            // Extract and save files to temp folder
            string tempFolder = Path.Combine(Path.GetTempPath(), "apphta");
            Directory.CreateDirectory(tempFolder);

            foreach (var resourceName in embeddedResources)
            {
                // Adjust resource path for the file system
                string fileName = resourceName.Replace("apphta.app.", string.Empty);
                string[] fileSplit = fileName.Split('.');
                string[] filesList = fileSplit.Take(fileSplit.Length - 2).ToArray();
                string last = fileSplit[fileSplit.Length - 2] + "." + fileSplit[fileSplit.Length - 1];

                string realPath = "";
                foreach (string p in filesList)
                {
                    if (!Directory.Exists(p))
                    {
                        Directory.CreateDirectory(Path.Combine(tempFolder, p));
                    }
                    realPath = Path.Combine(realPath, p);
                }

                string outputPath = Path.Combine(tempFolder, realPath, last);
                ExtractEmbeddedResource(resourceName, outputPath);
            }

            // Now, run the HTA using mshta
            string tempFilePath = Path.Combine(tempFolder, "gui.hta");
            Process.Start(new ProcessStartInfo
            {
                FileName = "mshta.exe",
                Arguments = $"\"{tempFilePath}\"",
                UseShellExecute = true
            });

            System.Threading.Thread.Sleep(3000);

            // Delete temp files after the HTA app starts
            DeleteTemporaryFiles(tempFolder);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void ExtractEmbeddedResource(string resourceName, string outputPath)
    {
        // Get the embedded resource stream
        using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
        {
            if (resourceStream == null)
            {
                throw new FileNotFoundException($"Embedded resource '{resourceName}' not found.");
            }

            // Write the resource stream to the file
            using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                resourceStream.CopyTo(fileStream);
            }
        }
    }

    static void DeleteTemporaryFiles(string folder)
    {
        if (Directory.Exists(folder))
        {
            foreach (var file in Directory.GetFiles(folder, "*", SearchOption.AllDirectories))
            {
                File.SetAttributes(file, FileAttributes.Normal); // Make sure the file is not read-only
                File.Delete(file);
            }

            Directory.Delete(folder, true);
        }
    }
}
