using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

// Manages the Data folder: deserializing and serializing scenarios to JSON.
public static class FileManager
{
    private static readonly string filePath = FileSystem.ScenariosFilePath;

    public static List<Scenario> LoadScenarios(string filePath)
    {
        if (!File.Exists(filePath))
            return new List<Scenario>();

        try
        {
            string json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<List<Scenario>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Scenario>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading scenarios: {ex.Message}");
            return new List<Scenario>();
        }
    }

    public static bool SaveScenarios(List<Scenario> scenarios, string filePath)
    {
        try
        {
            string json = JsonSerializer.Serialize(scenarios, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(filePath, json);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving scenarios: {ex.Message}");
            return false;
        }
    }
}