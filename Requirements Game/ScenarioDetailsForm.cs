using Requirements_Game;
using System.Windows.Forms;
using System.Drawing;

class ScenarioDetailsForm : Form {

    Scenario scenario;

    public ScenarioDetailsForm(Scenario scenario) {

        this.scenario = scenario;
        this.FormBorderStyle = FormBorderStyle.None;
        this.Size = new Size(600,600);

        CustomTableLayoutPanel cardTableLayoutPanel = new CustomTableLayoutPanel();
        cardTableLayoutPanel.Dock = DockStyle.Fill;
        cardTableLayoutPanel.BackColor = Color.FromArgb(200,200,200);
        cardTableLayoutPanel.RowCount = 2;
        cardTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
        cardTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
        cardTableLayoutPanel.ColumnCount = 2;
        cardTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        cardTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        this.Controls.Add(cardTableLayoutPanel);

        Label label = new Label();
        label.Text = scenario.Name;
        label.Font = new Font(GlobalVariables.AppFontName, 14, FontStyle.Bold);
        label.Dock = DockStyle.Top;
        label.Padding = new Padding(5);
        label.AutoSize = true;
        cardTableLayoutPanel.Controls.Add(label,0,0);

        CustomTextButton testButton = new CustomTextButton();
        testButton.Text = "Begin";
        testButton.Location = new Point(this.Width - testButton.Width - 10, this.Height - testButton.Height - 10);
        testButton.IdleBackColor = Color.Black;
        testButton.EnterBackColor = Color.Black;
        testButton.DownBackColor = Color.Black;
        testButton.ForeColor = Color.White;
        cardTableLayoutPanel.Controls.Add(testButton,1,1);

        testButton.MouseClick += TestButton_MouseClick;

    }

    private void TestButton_MouseClick(object sender, MouseEventArgs e) {

        Form1 form1 = (Form1)this.Owner;
        form1.ChangeView("Chat");

        this.Close();

    }

}