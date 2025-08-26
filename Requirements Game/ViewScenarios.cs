using Requirements_Game;
using System;
using System.Drawing;
using System.Windows.Forms;

public class ViewScenarios : View {

    public ViewScenarios() {

        ViewTableLayoutPanel.ColumnCount = 7;
        ViewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        ViewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300f));
        ViewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30f));
        ViewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300f));
        ViewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30f));
        ViewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300f));
        ViewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        ViewTableLayoutPanel.Dock = DockStyle.Top;
        
        RebuildView();

    }

    private void RebuildView() {

        ViewTableLayoutPanel.Controls.Clear();
        ViewTableLayoutPanel.RowCount = 0;      
     
        int columnIndex = 1;

        foreach (Scenario scenario in Scenarios.GetScenarios()) {

            if (columnIndex == 1) {

                ViewTableLayoutPanel.RowCount += 2;
                ViewTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
                ViewTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 115f));

            }

            ScenarioCard scenarioCard = new ScenarioCard(scenario);

            ViewTableLayoutPanel.Controls.Add(scenarioCard, columnIndex, ViewTableLayoutPanel.RowCount - 1);

            columnIndex = (columnIndex == 5) ? 1 : columnIndex + 2;

        }

        AutoResizeViewTableLayoutPanel();

    }

    private class ScenarioCard : CustomTableLayoutPanel {

        Scenario scenario;

        public ScenarioCard(Scenario scenario) {

            this.scenario = scenario;
            this.BackColor = GlobalVariables.LightGrey;
            this.CornerRadius = 15;
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(0);
            this.Margin = new Padding(0);

            this.ColumnCount = 3;
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10f));
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10f));

            this.RowCount = 3;
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 36f));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 2f));
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            Label scenarioNameLabel = new Label();
            scenarioNameLabel.Dock = DockStyle.Fill;
            scenarioNameLabel.Text = scenario.Name;
            scenarioNameLabel.BackColor = Color.Transparent;
            scenarioNameLabel.Font = new Font(GlobalVariables.AppFontName, 16, FontStyle.Bold);
            scenarioNameLabel.TextAlign = ContentAlignment.MiddleLeft;
            scenarioNameLabel.AutoSize = true;
            scenarioNameLabel.Padding = new Padding(0);
            scenarioNameLabel.Margin = new Padding(0);
            this.Controls.Add(scenarioNameLabel, 1, 0);

            Panel nameUnderscore = new Panel();
            nameUnderscore.BackColor = GlobalVariables.MediumGrey;
            nameUnderscore.Size = new Size(300, 2);
            nameUnderscore.Dock = DockStyle.Fill;
            nameUnderscore.Padding = new Padding(0);
            nameUnderscore.Margin = new Padding(5, 0, 0, 0);
            this.Controls.Add(nameUnderscore, 1, 1);

            Label scenarioDescriptionLabel = new Label();
            scenarioDescriptionLabel.Dock = DockStyle.Fill;
            scenarioDescriptionLabel.BackColor = Color.Transparent;
            scenarioDescriptionLabel.Text = scenario.Description;
            scenarioDescriptionLabel.Font = new Font(GlobalVariables.AppFontName, 10, FontStyle.Italic);
            scenarioDescriptionLabel.TextAlign = ContentAlignment.TopLeft;
            scenarioDescriptionLabel.AutoSize = true;
            scenarioDescriptionLabel.Padding = new Padding(0);
            scenarioDescriptionLabel.Margin = new Padding(0, 8,0,0);
            this.Controls.Add(scenarioDescriptionLabel, 1, 2);

            this.MouseClick += Me_MouseClick;

            scenarioNameLabel.MouseClick += Me_MouseClick;

            nameUnderscore.MouseClick += Me_MouseClick;

            scenarioDescriptionLabel.MouseClick += Me_MouseClick;


        }

        // -- Events --

        private void Me_MouseClick(object sender, MouseEventArgs e) {

            Form1 form1 = (Form1)this.FindForm();

            ScenarioDetailsForm scenarioDetailsForm = new ScenarioDetailsForm(scenario);
            scenarioDetailsForm.StartPosition = FormStartPosition.Manual;

            scenarioDetailsForm.Location = new Point(
                form1.Location.X + (form1.Width - scenarioDetailsForm.Width) / 2,
                form1.Location.Y + (form1.Height - scenarioDetailsForm.Height) / 2
            );

            scenarioDetailsForm.Show(form1);

        }

    }

}