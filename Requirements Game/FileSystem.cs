using System;

class FileSystem {

    public static string AppFolderPath { get; }
    public static string ModelsFolderPath { get; }

    static FileSystem() {

        AppFolderPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Requirement Game";
        ModelsFolderPath = $"{AppFolderPath}\\Models";

        System.IO.Directory.CreateDirectory(AppFolderPath);
        System.IO.Directory.CreateDirectory(ModelsFolderPath);

    }

}