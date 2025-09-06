using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

/// <summary>
/// Static manager class for handling a collection of scenarios.
/// Provides methods for loading, saving, adding, removing, and debugging scenarios.
/// </summary>
public static class Scenarios
{
    /// <summary>
    /// Event triggered whenever the scenario list changes.
    /// </summary>
    public static event EventHandler ScenariosChanged;

    private static List<Scenario> listOfScenarios;

    static Scenarios()
    {
        listOfScenarios = new List<Scenario>();
    }
    /// <summary>
    /// Loads scenarios from a JSON file and updates the internal list.
    /// Also saves the loaded data to the default file path.
    /// </summary>
    /// <param name="filePath">Path to the JSON file.</param>
    public static void LoadFromFile(string filePath)
    {
        listOfScenarios = FileManager.LoadScenarios(filePath);
        ScenariosChanged?.Invoke(null, EventArgs.Empty);
        SaveToFile(FileSystem.ScenariosFilePath, listOfScenarios);
    }
    /// <summary>
    /// Saves a list of scenarios to a specified file path.
    /// </summary>
    /// <param name="filePath">Destination file path.</param>
    /// <param name="scenarios">List of scenarios to save.</param>
    public static void SaveToFile(string filePath, List<Scenario> scenarios)
    {
        FileManager.SaveScenarios(scenarios, filePath);
    }
    /// <summary>
    /// Adds a scenario to the internal list and saves the updated list.
    /// </summary>
    /// <param name="item">Scenario to add.</param>
    public static void Add(Scenario item)
    {
        listOfScenarios.Add(item);
        ScenariosChanged?.Invoke(null, EventArgs.Empty);
        SaveToFile(FileSystem.ScenariosFilePath, listOfScenarios);
    }
    /// <summary>
    /// Removes a scenario from the internal list and saves the updated list.
    /// </summary>
    /// <param name="item">Scenario to remove.</param>
    public static void Remove(Scenario item)
    {
        listOfScenarios.Remove(item);
        ScenariosChanged?.Invoke(null, EventArgs.Empty);
        SaveToFile(FileSystem.ScenariosFilePath, listOfScenarios);
    }
    /// <summary>
    /// Returns a copy of the current scenario list as an array.
    /// </summary>
    /// <returns>Array of scenarios.</returns>
    public static Scenario[] GetScenarios()
    {
        return listOfScenarios.ToArray();
    }
    /// <summary>
    /// Gets the internal scenario list.
    /// </summary>
    public static List<Scenario> ScenarioList => listOfScenarios;

    /// <summary>
    /// Prints detailed information about each scenario to the debug console.
    /// </summary>
    /// <param name="scenarios">List of scenarios to print.</param>
    public static void DebugPrintScenarios(List<Scenario> scenarios)
    {
        foreach (Scenario scenario in scenarios)
        {
            Debug.WriteLine("======================================");
            Debug.WriteLine($"Scenario Name: {scenario.Name}");
            Debug.WriteLine($"Description: {scenario.Description}");
            Debug.WriteLine($"Prompt: {scenario.Prompt}");

            Debug.WriteLine("--- Senior Software Engineer ---");
            Debug.WriteLine($"Name: {scenario.SeniorSoftwareEngineer.Name}");
            Debug.WriteLine($"Role: {scenario.SeniorSoftwareEngineer.Role}");
            Debug.WriteLine($"Personality: {scenario.SeniorSoftwareEngineer.Personality}");

            Debug.WriteLine("--- Stakeholders ---");
            if (scenario.ListStakeholders.Count == 0)
            {
                Debug.WriteLine("None");
            }
            else
            {
                foreach (Stakeholder stakeholder in scenario.ListStakeholders)
                {
                    Debug.WriteLine($"- Name: {stakeholder.Name}");
                    Debug.WriteLine($"  Role: {stakeholder.Role}");
                    Debug.WriteLine($"  Personality: {stakeholder.Personality}");
                }
            }

            Debug.WriteLine("--- Functional Requirements ---");
            if (scenario.FunctionalRequirements.Count == 0)
            {
                Debug.WriteLine("None");
            }
            else
            {
                foreach (string req in scenario.FunctionalRequirements)
                {
                    Debug.WriteLine($"- {req}");
                }
            }

            Debug.WriteLine("--- Non-Functional Requirements ---");
            if (scenario.NonFunctionalRequirements.Count == 0)
            {
                Debug.WriteLine("None");
            }
            else
            {
                foreach (string req in scenario.NonFunctionalRequirements)
                {
                    Debug.WriteLine($"- {req}");
                }
            }

            Debug.WriteLine("======================================\n");
        }
    }

    /// <summary>
    /// Prints detailed information about each scenario to the debug console.
    /// </summary>
    public static void DebugPrintScenarios()
    {
        DebugPrintScenarios(listOfScenarios);
    }
}

/// <summary>
/// Represents a single scenario.
/// Contains description, stakeholders, and requirement lists.
/// Managed by the <c>Scenarios</c> class.
/// </summary>
public class Scenario
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Prompt { get; set; }

    public Stakeholder SeniorSoftwareEngineer { get; set; }

    /// <summary>
    /// List of additional stakeholders involved in the scenario.
    /// </summary>
    public List<Stakeholder> ListStakeholders { get; set; }

    /// <summary>
    /// List of functional requirements for the scenario.
    /// </summary>
    public List<string> FunctionalRequirements { get; set; }

    /// <summary>
    /// List of non-functional requirements for the scenario.
    /// </summary>
    public List<string> NonFunctionalRequirements { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Scenario"/> class with empty attributes.
    /// </summary>
    public Scenario()
    {
        Name = "";
        Description = "";
        Prompt = "";
        SeniorSoftwareEngineer = new Stakeholder();
        ListStakeholders = new List<Stakeholder>();
        FunctionalRequirements = new List<string>();
        NonFunctionalRequirements = new List<string>();
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="Scenario"/> class with values.
    /// </summary>
    /// <param name="name">The title of the scenario.</param>
    /// <param name="description">A description of the scenario context.</param>
    /// <param name="prompt">The prompt associated with the scenario.</param>
    /// <param name="seniorSoftwareEngineer">The senior software engineer assigned to the scenario.</param>
    /// <param name="stakeholders">A list of additional stakeholders involved.</param>
    /// <param name="functionalRequirements">A list of functional requirements.</param>
    /// <param name="nonFunctionalRequirements">A list of non-functional requirements.</param>
    public Scenario(
        string name,
        string description,
        string prompt,
        Stakeholder seniorSoftwareEngineer,
        List<Stakeholder> stakeholders,
        List<string> functionalRequirements,
        List<string> nonFunctionalRequirements)
    {
        Name = name;
        Description = description;
        Prompt = prompt;
        SeniorSoftwareEngineer = seniorSoftwareEngineer ?? new Stakeholder();
        ListStakeholders = stakeholders ?? new List<Stakeholder>();
        FunctionalRequirements = functionalRequirements ?? new List<string>();
        NonFunctionalRequirements = nonFunctionalRequirements ?? new List<string>();
    }

    /// <summary>
    /// Adds a stakeholder to the scenario's stakeholder list.
    /// </summary>
    /// <param name="stakeholder">The stakeholder to add.</param>
    public void AddStakeholder(Stakeholder stakeholder)
    {
        ListStakeholders.Add(stakeholder);
    }

    /// <summary>
    /// Returns all stakeholders in the scenario as an array.
    /// </summary>
    /// <returns>An array of <see cref="Stakeholder"/> objects.</returns>
    public Stakeholder[] GetStakeholders()
    {
        return ListStakeholders.ToArray();
    }

    /// <summary>
    /// Adds a functional requirement to the scenario.
    /// </summary>
    /// <param name="requirement">The functional requirement to add.</param>
    public void AddFunctionalRequirement(string requirement)
    {
        FunctionalRequirements.Add(requirement);
    }

    /// <summary>
    /// Returns all functional requirements as an array.
    /// </summary>
    /// <returns>An array of functional requirement strings.</returns>
    public string[] GetFunctionalRequirements()
    {
        return FunctionalRequirements.ToArray();
    }

    /// <summary>
    /// Adds a non-functional requirement to the scenario.
    /// </summary>
    /// <param name="requirement">The non-functional requirement to add.</param>
    public void AddNonFunctionalRequirement(string requirement)
    {
        NonFunctionalRequirements.Add(requirement);
    }

    /// <summary>
    /// Returns all non-functional requirements as an array.
    /// </summary>
    /// <returns>An array of non-functional requirement strings.</returns>
    public string[] GetNonFunctionalRequirements()
    {
        return NonFunctionalRequirements.ToArray();
    }
}


/// <summary>
/// Represents a stakeholder involved in a scenario.
/// Used for both the senior software engineer and additional participants.
/// </summary>
public class Stakeholder
{
    public string Name { get; set; }
    public string Role { get; set; }
    public string Personality { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Stakeholder"/> class with default values.
    /// </summary>
    public Stakeholder()
    {
        Name = "";
        Role = "";
        Personality = "";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Stakeholder"/> class with specified values.
    /// </summary>
    /// <param name="name">The stakeholder's name.</param>
    /// <param name="role">The stakeholder's role.</param>
    /// <param name="personality">The stakeholder's personality description.</param>
    public Stakeholder(string name, string role, string personality)
    {
        Name = name;
        Role = role;
        Personality = personality;
    }
}