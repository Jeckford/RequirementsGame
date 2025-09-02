using System;
using System.Collections.Generic;
using System.Drawing;


public static class Scenarios
{
    public static event EventHandler ScenariosChanged;

    private static List<Scenario> listOfScenarios;

    static Scenarios()
    {
        listOfScenarios = new List<Scenario>();

        // Temporary hardcoded scenario for UI testing
        //var scenario1 = new Scenario
        //{
        //    Name = "Book Discussion Platform",
        //    Description = "An online space for book clubs to chat, track reading progress, host discussions, and share notes in real time."
        //};

        //listOfScenarios.Add(scenario1);
    }
    public static void LoadFromFile()
    {
        listOfScenarios = FileManager.LoadScenarios();
        ScenariosChanged?.Invoke(null, EventArgs.Empty);
    }
    public static void SaveToFile()
    {
        FileManager.SaveScenarios(listOfScenarios);
    }

    public static void Add(Scenario item)
    {
        listOfScenarios.Add(item);
        ScenariosChanged?.Invoke(null, EventArgs.Empty);
    }

    public static void Remove(Scenario item)
    {
        listOfScenarios.Remove(item);
        ScenariosChanged?.Invoke(null, EventArgs.Empty);
    }

    public static Scenario[] GetScenarios()
    {
        return listOfScenarios.ToArray();
    }

    public static List<Scenario> ScenarioList => listOfScenarios;
}

// Houses a single Scenario, is managed by Scenarios.
public class Scenario
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Prompt { get; set; }

    public Stakeholder SeniorSoftwareEngineer { get; }
    public List<Stakeholder> ListStakeholders { get; }
    public List<string> FunctionalRequirements { get; }
    public List<string> NonFunctionalRequirements { get; }

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

    public void AddStakeholder(Stakeholder stakeholder)
    {
        ListStakeholders.Add(stakeholder);
    }

    public Stakeholder[] GetStakeholders()
    {
        return ListStakeholders.ToArray();
    }
}


// Stakeholder class used by Scenario for senior engineer and other stakeholders.
public class Stakeholder
{
    public string Name { get; set; }
    public string Role { get; set; }
    public string Personality { get; set; }
}