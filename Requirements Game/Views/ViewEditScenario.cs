public class ViewEditScenario : ViewCreateScenario {

    public void ChangeScenario(ref Scenario scenario) {

        this.Scenario = scenario;
        this.RebuildView();

    }

}