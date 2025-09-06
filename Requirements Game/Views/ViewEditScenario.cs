public class ViewEditScenario : ViewCreateScenario {

    public void ChangeScenario(ref Scenario scenario) {

        this.editingScenario = scenario;
        this.isEditMode = true;
        this.RebuildView();
    }
}