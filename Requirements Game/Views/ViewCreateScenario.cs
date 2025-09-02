using System.Windows.Forms;
using System.Drawing;

public class ViewCreateScenario : View {

    protected Scenario Scenario;

    public ViewCreateScenario() {

        this.Scenario = new Scenario();
        this.Scenario.AddStakeholder(new Stakeholder());

        ViewTableLayoutPanel.Dock = DockStyle.Top;
        ViewTableLayoutPanel.AutoSize = true;

        ViewTableLayoutPanel.ColumnCount = 3;
        ViewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        ViewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 800));
        ViewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        ViewTableLayoutPanel.RowCount = 2;
        ViewTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
        ViewTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        ViewTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

        RebuildView();

    }

    public void RebuildView() {

        ViewTableLayoutPanel.Controls.Clear();

        CustomTableLayoutPanel subTableLayoutPanel = new CustomTableLayoutPanel();
        subTableLayoutPanel.CornerRadius = 10;
        subTableLayoutPanel.Padding = new Padding(0, 0, 0, 0);
        subTableLayoutPanel.Dock = DockStyle.Top;
        subTableLayoutPanel.BackColor = GlobalVariables.ColorLight;
        subTableLayoutPanel.AutoSize = true;

        subTableLayoutPanel.ColumnCount = 3;
        subTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
        subTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        subTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));

        subTableLayoutPanel.RowCount = 1;
        subTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 10f));

        ViewTableLayoutPanel.Controls.Add(subTableLayoutPanel, 1, 1);

        // Children

        RebuildView_LabelledRichTextBox(ref subTableLayoutPanel, "Scenario Name", Scenario.Name);
        RebuildView_LabelledRichTextBox(ref subTableLayoutPanel, "Description", Scenario.Description, 6);

        RebuildView_Label(ref subTableLayoutPanel, "Senior Software Engineer");
        RebuildView_LabelledRichTextBox(ref subTableLayoutPanel, "Name", Scenario.SeniorSoftwareEngineer.Name);
        RebuildView_LabelledRichTextBox(ref subTableLayoutPanel, "Role", Scenario.SeniorSoftwareEngineer.Role);
        RebuildView_LabelledRichTextBox(ref subTableLayoutPanel, "Personality", Scenario.SeniorSoftwareEngineer.Personality, 3);

        RebuildView_Label(ref subTableLayoutPanel, "Stakeholders");

        foreach (Stakeholder stakeholder in Scenario.GetStakeholders()) {

            RebuildView_LabelledRichTextBox(ref subTableLayoutPanel, "Name", stakeholder.Name);
            RebuildView_LabelledRichTextBox(ref subTableLayoutPanel, "Role", stakeholder.Role);
            RebuildView_LabelledRichTextBox(ref subTableLayoutPanel, "Personality", stakeholder.Personality, 3);

        }

        // Add Stake holder

        subTableLayoutPanel.RowCount += 2;
        subTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
        subTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        CustomTextButton addStakeholderButton = new CustomTextButton();
        addStakeholderButton.Text = "+ Add Stakeholder";
        addStakeholderButton.Font = new Font(GlobalVariables.AppFontName, 14, FontStyle.Bold);
        addStakeholderButton.ForeColor = Color.White;
        addStakeholderButton.IdleBackColor = Color.FromArgb(45, 45, 45);
        addStakeholderButton.EnterBackColor = Color.FromArgb(85, 85, 85);
        addStakeholderButton.DownBackColor = Color.FromArgb(125, 125, 125);
        addStakeholderButton.TextAlign = ContentAlignment.MiddleCenter;
        addStakeholderButton.CornerRadius = 5;
        addStakeholderButton.Size = new Size(170, 30);
        addStakeholderButton.Anchor = AnchorStyles.None;

        subTableLayoutPanel.Controls.Add(addStakeholderButton, 1, subTableLayoutPanel.RowCount - 1);

        // Create/Save Button

        subTableLayoutPanel.RowCount += 2;
        subTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
        subTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        CustomTextButton createButton = new CustomTextButton();
        createButton.Text = "Create";
        createButton.Font = new Font(GlobalVariables.AppFontName, 14, FontStyle.Bold);
        createButton.ForeColor = Color.White;
        createButton.IdleBackColor = Color.FromArgb(0,136,5);
        createButton.EnterBackColor = ColorManager.DarkenColor(createButton.IdleBackColor, 0.1);
        createButton.DownBackColor = ColorManager.DarkenColor(createButton.EnterBackColor, 0.1);
        createButton.TextAlign = ContentAlignment.MiddleCenter;
        createButton.CornerRadius = 5;
        createButton.Size = new Size(100, 30);
        createButton.Anchor = AnchorStyles.Right;

        subTableLayoutPanel.Controls.Add(createButton, 1, subTableLayoutPanel.RowCount - 1);

        // Margin

        subTableLayoutPanel.RowCount += 1;
        subTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));

    }

    public void RebuildView_LabelledRichTextBox(ref CustomTableLayoutPanel SubTableLayoutPanel, string LabelText, string TextboxText, int RowCount = 1) {

        SubTableLayoutPanel.RowCount += 1;
        SubTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        CustomLabelledRichTextBox richTextBox = new CustomLabelledRichTextBox();
        richTextBox.LabelText = LabelText;
        richTextBox.TextboxText = TextboxText;
        richTextBox.Dock = DockStyle.Top;
        SubTableLayoutPanel.Controls.Add(richTextBox, 1, SubTableLayoutPanel.RowCount - 1);

        if (RowCount > 1) {

            richTextBox.Multiline = true;
            richTextBox.TextBoxRowCount = RowCount;

        }

    }

    public void RebuildView_Label(ref CustomTableLayoutPanel SubTableLayoutPanel, string LabelText) {

        SubTableLayoutPanel.RowCount += 1;
        SubTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        CustomLabel label = new CustomLabel();
        label.Text = LabelText;
        label.Dock = DockStyle.Top;
        label.Font = new Font(GlobalVariables.AppFontName, 16, FontStyle.Bold);
        label.TextAlign = ContentAlignment.MiddleLeft;
        label.Padding = new Padding(0);
        label.Margin = new Padding(0,8,0,2);
        label.AutoSize = true;
        SubTableLayoutPanel.Controls.Add(label, 1, SubTableLayoutPanel.RowCount - 1);

    }

}