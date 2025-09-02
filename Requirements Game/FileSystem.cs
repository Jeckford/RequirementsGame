using System;
using System.IO;
using Requirements_Game.Properties;

class FileSystem
{

    public static string AppFolderPath { get; }
    public static string ModelsFolderPath { get; }
    public static string ScenariosFilePath { get; }

    static FileSystem()
    {

        AppFolderPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Requirement Game";
        ModelsFolderPath = $"{AppFolderPath}\\Models";
        ScenariosFilePath = $"{AppFolderPath}\\Scenarios.json";

        System.IO.Directory.CreateDirectory(AppFolderPath);
        System.IO.Directory.CreateDirectory(ModelsFolderPath);
        if (!File.Exists(ScenariosFilePath))
        {
            File.WriteAllText(ScenariosFilePath, "[]");
        }
    }

}