using System;
using System.IO;
using Requirements_Game.Properties;

class FileSystem
{
    public static string InstallDirectory => AppDomain.CurrentDomain.BaseDirectory;
    public static string AppFolderPath { get; }
    public static string ModelsFolderPath { get; }
    public static string ScenariosFilePath { get; }

    static FileSystem()
    {
        AppFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Requirement Game");
        ModelsFolderPath = Path.Combine(AppFolderPath, "Models");
        ScenariosFilePath = Path.Combine(AppFolderPath, "Scenarios.json");

        Directory.CreateDirectory(AppFolderPath);
        Directory.CreateDirectory(ModelsFolderPath);

        string bundledScenarioPath = Path.Combine(InstallDirectory, "Resources", "DefaultScenarios.json");
        string bundledModelsPath = Path.Combine(InstallDirectory, "Models");

        // Check if first installation
        if (!File.Exists(ScenariosFilePath))
        {
            // Copy scenario file
            if (File.Exists(bundledScenarioPath))
            {
                File.Copy(bundledScenarioPath, ScenariosFilePath);
            }

            // Copy each model file from installer to app location
            if (Directory.Exists(bundledModelsPath))
            {
                foreach (string modelFile in Directory.GetFiles(bundledModelsPath))
                {
                    string fileName = Path.GetFileName(modelFile);
                    string destPath = Path.Combine(ModelsFolderPath, fileName);

                    if (!File.Exists(destPath))
                    {
                        File.Copy(modelFile, destPath);
                    }
                }
            }
        }
    }

}