using System;
using System.Collections.Generic;
using System.Drawing;

public static class Scenarios {

    public static event EventHandler ScenariosChanged;

    static private List<Scenario> ListOfScenarios;

    static Scenarios() {

        ListOfScenarios = new List<Scenario>();

        // Manually creating the scenarios for ui implementation
        // These should be loaded from a file instead, implement later

        Scenario scenario1 = new Scenario();
        scenario1.Name = "Book Discussion Platform";
        scenario1.Description = "An online space for book clubs to chat, track reading progress, host discussions, and share notes in real time.";
        ListOfScenarios.Add(scenario1);

        Scenario scenario2 = new Scenario();
        scenario2.Name = "Online Food Delivery Platform";
        scenario2.Description = "A delivery app that connects users with local restaurants, offering real-time tracking, contactless delivery, and driver features.";
        ListOfScenarios.Add(scenario2);

        Scenario scenario3 = new Scenario();
        scenario3.Name = "Course Registration System";
        scenario3.Description = "A university system where students register for courses, view schedules, handle prerequisites, and manage academic plans.";
        ListOfScenarios.Add(scenario3);

        Scenario scenario4 = new Scenario();
        scenario4.Name = "Smart Parking System";
        scenario4.Description = "An IoT-enabled app that helps users locate, reserve, and pay for parking spots with live availability and sensor-based tracking.";
        ListOfScenarios.Add(scenario4);

        Scenario scenario5 = new Scenario();
        scenario5.Name = "Mental Health App";
        scenario5.Description = "A mobile platform providing mood tracking, journaling, mindfulness exercises, and access to certified mental health professionals.";
        ListOfScenarios.Add(scenario5);

    }

    static void Add(Scenario item) {

        ListOfScenarios.Add(item);

    }

    static void Remove(Scenario item) {

        ListOfScenarios.Remove(item);

        ScenariosChanged?.Invoke(null, EventArgs.Empty);

    }

     static public Scenario[] GetScenarios() { 
    
        return ListOfScenarios.ToArray();

    }

}

public class Scenario {

    public string Name { get; set; }
    public string Description { get; set; }
    public Stakeholder SeniorSoftwareEngineer { get; }
    public List<Stakeholder> ListStakeholders { get; }

    public Scenario() {

        Name = "";
        Description = "";
        SeniorSoftwareEngineer = new Stakeholder();
        ListStakeholders = new List<Stakeholder>();

    }

    public void AddStakeholder(Stakeholder Stakeholder) {

        ListStakeholders.Add(Stakeholder);

    }

    public Stakeholder[] GetStakeholder() {

        return ListStakeholders.ToArray();

    }

}

public class Stakeholder {

    public string Name { get; set; }
    public string Role { get; set; }
    public string Personality { get; set; }
    public Image ProfilePicture { get; set; }

}