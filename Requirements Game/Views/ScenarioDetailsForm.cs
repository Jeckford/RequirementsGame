using Requirements_Game;
using System.Windows.Forms;
using System.Drawing;

class ScenarioDetailsForm : Form {

    Scenario scenario;

    public static void Show(Scenario scenario, Form mainForm) {

        using (var form = new ScenarioDetailsForm(scenario)) {

            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog(mainForm);

        }

    }

    public ScenarioDetailsForm(Scenario scenario) {

        this.scenario = scenario;
        this.FormBorderStyle = FormBorderStyle.None;
        this.Size = new Size(600,600);
        this.BackColor = Color.AliceBlue;
        this.TransparencyKey = Color.AliceBlue;

        // TableLayoutPanel

        CustomTableLayoutPanel tableLayoutPanel = new CustomTableLayoutPanel();
        tableLayoutPanel.CornerRadius = 10;
        tableLayoutPanel.Dock = DockStyle.Fill;
        tableLayoutPanel.BackColor = GlobalVariables.ColorMedium;
        tableLayoutPanel.Padding = new Padding(0);

        tableLayoutPanel.RowCount = 5;
        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));

        tableLayoutPanel.ColumnCount = 5;
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130f));
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30f));
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
        this.Controls.Add(tableLayoutPanel);

        //

        Label label = new Label();
        label.Text = scenario.Name;
        label.Font = new Font(GlobalVariables.AppFontName, 16, FontStyle.Bold);
        label.Dock = DockStyle.Fill;
        label.AutoSize = true;
        label.TextAlign = ContentAlignment.MiddleLeft;
        tableLayoutPanel.Controls.Add(label,1,1);

        // Close

        CustomPictureBox pictureBox = new CustomPictureBox();
        pictureBox.IdleImage = (Image)Requirements_Game.Properties.Resources.ResourceManager.GetObject("close");
        tableLayoutPanel.Controls.Add(pictureBox, 3, 1);

        pictureBox.MouseClick += CloseButton_MouseClick;

        //

        CustomTextButton testButton = new CustomTextButton();
        testButton.Text = "Begin Interviewing";
        testButton.CornerRadius = 5;
        testButton.TextAlign = ContentAlignment.MiddleCenter;
        testButton.Padding = new Padding(0,0,0,3);
        testButton.Font = new Font(GlobalVariables.AppFontName, 12, FontStyle.Bold);
        testButton.IdleBackColor = Color.Black;
        testButton.EnterBackColor = Color.Black;
        testButton.DownBackColor = Color.Black;
        testButton.ForeColor = Color.White;
        testButton.Dock = DockStyle.Fill;
        tableLayoutPanel.Controls.Add(testButton, 2,3);
        tableLayoutPanel.SetColumnSpan(testButton, 2);

        testButton.MouseClick += TestButton_MouseClick;
     
    }

    private void CloseButton_MouseClick(object sender, MouseEventArgs e) {

        this.Close();

    }

    private void TestButton_MouseClick(object sender, MouseEventArgs e) {

        Form1 form1 = (Form1)this.Owner;
        form1.ChangeView("Chat");

        this.Close();

    }

}