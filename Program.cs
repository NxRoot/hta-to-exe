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
                string outputPath = Path.Combine(tempFolder, fileName);

                ExtractEmbeddedResource(tempFolder, resourceName, outputPath);
            }

            // Now, run the HTA using mshta
            string tempFilePath = Path.Combine(tempFolder, "gui.hta");
            Process.Start(new ProcessStartInfo
            {
                FileName = "mshta.exe",
                Arguments = $"\"{tempFilePath}\"",
                UseShellExecute = true
            });

            System.Threading.Thread.Sleep(5000);

            // Delete temp files after the HTA app starts
            DeleteTemporaryFiles(tempFolder);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void ExtractEmbeddedResource(string tempFolder, string resourceName, string outputPath)
    {
        // Get the embedded resource stream
        using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
        {
            if (resourceStream == null)
            {
                throw new FileNotFoundException($"Embedded resource '{resourceName}' not found.");
            }

            foreach (string p in outputPath.Split(Path.DirectorySeparatorChar))
            {
                if (!p.Contains(".") && !Directory.Exists(p))
                {
                    Directory.CreateDirectory(Path.Combine(tempFolder, p));
                }
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
        // Delete all files in the temp folder
        foreach (var file in Directory.GetFiles(folder))
        {
            try
            {
                File.Delete(file);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete file {file}: {ex.Message}");
            }
        }

        // Optionally, delete the folder if it's empty
        if (Directory.Exists(folder) && Directory.GetFiles(folder).Length == 0)
        {
            Directory.Delete(folder);
        }
    }
}
