using Requirements_Game;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

public class ViewCreateScenario : View
{
    protected Scenario editingScenario = null;
    protected bool isEditMode = false;

    private Dictionary<string, CustomLabelledRichTextBox> inputFields = new Dictionary<string, CustomLabelledRichTextBox>();
    private int stakeholderCount = 1;

    public ViewCreateScenario()
    {
        ViewTableLayoutPanel.Dock = DockStyle.Top;
        ViewTableLayoutPanel.AutoSize = true;

        ViewTableLayoutPanel.ColumnCount = 3;
        ViewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        ViewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 800));
        ViewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        ViewTableLayoutPanel.RowCount = 0;
        RebuildView();
    }

    public void RebuildView()
    {
        inputFields.Clear();
        ViewTableLayoutPanel.Controls.Clear();
        ViewTableLayoutPanel.RowStyles.Clear();
        ViewTableLayoutPanel.RowCount = 0;

        string name = isEditMode ? editingScenario.Name : "";
        string description = isEditMode ? editingScenario.Description : "";

        string seniorName = isEditMode ? editingScenario.SeniorSoftwareEngineer.Name : "";
        string seniorRole = isEditMode ? editingScenario.SeniorSoftwareEngineer.Role : "";
        string seniorPersonality = isEditMode ? editingScenario.SeniorSoftwareEngineer.Personality : "";

        List<Stakeholder> stakeholders = isEditMode ? editingScenario.GetStakeholders().ToList() : new List<Stakeholder> { new Stakeholder() };
        stakeholderCount = stakeholders.Count;

        string frText = isEditMode ? string.Join("\n", editingScenario.FunctionalRequirements) : "";
        string nfrText = isEditMode ? string.Join("\n", editingScenario.NonFunctionalRequirements) : "";

        // Scenario Info Block
        var scenarioBlock = CreateSectionBlock();
        RebuildView_LabelledRichTextBox(ref scenarioBlock, "Scenario Name", name);
        RebuildView_LabelledRichTextBox(ref scenarioBlock, "Description", description, 6);
        ViewTableLayoutPanel.Controls.Add(scenarioBlock, 1, ViewTableLayoutPanel.RowCount++);
        ViewTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        // Senior Engineer Block
        var seniorBlock = CreateSectionBlock();
        RebuildView_Label(ref seniorBlock, "Senior Software Engineer");
        RebuildView_LabelledRichTextBox(ref seniorBlock, "Senior Name", seniorName);
        RebuildView_LabelledRichTextBox(ref seniorBlock, "Senior Role", seniorRole);
        RebuildView_LabelledRichTextBox(ref seniorBlock, "Senior Personality", seniorPersonality, 3);
        ViewTableLayoutPanel.Controls.Add(seniorBlock, 1, ViewTableLayoutPanel.RowCount++);
        ViewTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        // Stakeholders Block
        var stakeholderBlock = CreateSectionBlock();
        RebuildView_Label(ref stakeholderBlock, "Stakeholders");
        for (int i = 0; i < stakeholders.Count; i++)
        {
            int index = i + 1;
            RebuildView_LabelledRichTextBox(ref stakeholderBlock, $"Name_{index}", stakeholders[i].Name);
            RebuildView_LabelledRichTextBox(ref stakeholderBlock, $"Role_{index}", stakeholders[i].Role);
            RebuildView_LabelledRichTextBox(ref stakeholderBlock, $"Personality_{index}", stakeholders[i].Personality, 3);
        }

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

        addStakeholderButton.MouseClick += (s, e) =>
        {
            stakeholderCount++;
            RebuildView_LabelledRichTextBox(ref stakeholderBlock, $"Name_{stakeholderCount}", "");
            RebuildView_LabelledRichTextBox(ref stakeholderBlock, $"Role_{stakeholderCount}", "");
            RebuildView_LabelledRichTextBox(ref stakeholderBlock, $"Personality_{stakeholderCount}", "", 3);
        };

        stakeholderBlock.RowCount += 1;
        stakeholderBlock.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
        stakeholderBlock.Controls.Add(addStakeholderButton, 1, stakeholderBlock.RowCount - 1);

        ViewTableLayoutPanel.Controls.Add(stakeholderBlock, 1, ViewTableLayoutPanel.RowCount++);
        ViewTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        // Requirements Block
        var requirementsBlock = CreateSectionBlock();
        RebuildView_Label(ref requirementsBlock, "Requirements");
        RebuildView_LabelledRichTextBox(ref requirementsBlock, "Functional Requirements", frText, 6);
        RebuildView_LabelledRichTextBox(ref requirementsBlock, "Non-Functional Requirements", nfrText, 6);
        ViewTableLayoutPanel.Controls.Add(requirementsBlock, 1, ViewTableLayoutPanel.RowCount++);
        ViewTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        // Create Button
        var buttonBlock = CreateSectionBlock();
        CustomTextButton createButton = new CustomTextButton();
        createButton.Text = isEditMode ? "Save" : "Create";
        createButton.Font = new Font(GlobalVariables.AppFontName, 14, FontStyle.Bold);
        createButton.ForeColor = Color.White;
        createButton.IdleBackColor = Color.FromArgb(0, 136, 5);
        createButton.EnterBackColor = ColorManager.DarkenColor(createButton.IdleBackColor, 0.1);
        createButton.DownBackColor = ColorManager.DarkenColor(createButton.EnterBackColor, 0.1);
        createButton.TextAlign = ContentAlignment.MiddleCenter;
        createButton.CornerRadius = 5;
        createButton.Size = new Size(100, 30);
        createButton.Anchor = AnchorStyles.Right;

        createButton.MouseClick += CreateButton_MouseClick;
        buttonBlock.RowCount += 1;
        buttonBlock.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
        buttonBlock.Controls.Add(createButton, 1, buttonBlock.RowCount - 1);

        ViewTableLayoutPanel.Controls.Add(buttonBlock, 1, ViewTableLayoutPanel.RowCount++);
        ViewTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
    }

    public void RebuildView_LabelledRichTextBox(ref CustomTableLayoutPanel SubTableLayoutPanel, string LabelText, string TextboxText, int RowCount = 1)
    {
        SubTableLayoutPanel.RowCount += 1;
        SubTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        CustomLabelledRichTextBox richTextBox = new CustomLabelledRichTextBox();
        richTextBox.LabelText = LabelText;
        richTextBox.Dock = DockStyle.Top;

        if (RowCount > 1)
        {
            richTextBox.Multiline = true;
            richTextBox.TextBoxRowCount = RowCount;
            richTextBox.Height = RowCount * 20;
        }

        richTextBox.TextboxText = TextboxText;
        SubTableLayoutPanel.Controls.Add(richTextBox, 1, SubTableLayoutPanel.RowCount - 1);
        inputFields[LabelText] = richTextBox;
    }

    public void RebuildView_Label(ref CustomTableLayoutPanel SubTableLayoutPanel, string LabelText)
    {
        SubTableLayoutPanel.RowCount += 1;
        SubTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        CustomLabel label = new CustomLabel();
        label.Text = LabelText;
        label.Dock = DockStyle.Top;
        label.Font = new Font(GlobalVariables.AppFontName, 16, FontStyle.Bold);
        label.AutoSize = true;

        SubTableLayoutPanel.Controls.Add(label, 1, SubTableLayoutPanel.RowCount - 1);
    }

    private void CreateButton_MouseClick(object sender, MouseEventArgs e)
    {
        Debug.WriteLine(isEditMode ? "Saving Edited Scenario" : "Creating New Scenario");

        Scenario target = isEditMode ? editingScenario : new Scenario();

        target.Name = inputFields["Scenario Name"].TextboxText;
        target.Description = inputFields["Description"].TextboxText;

        target.SeniorSoftwareEngineer.Name = inputFields["Senior Name"].TextboxText;
        target.SeniorSoftwareEngineer.Role = inputFields["Senior Role"].TextboxText;
        target.SeniorSoftwareEngineer.Personality = inputFields["Senior Personality"].TextboxText;

        target.ListStakeholders.Clear();

        for (int i = 1; i <= stakeholderCount; i++)
        {
            if (inputFields.ContainsKey($"Name_{i}"))
            {
                var stakeholder = new Stakeholder
                {
                    Name = inputFields[$"Name_{i}"].TextboxText,
                    Role = inputFields[$"Role_{i}"].TextboxText,
                    Personality = inputFields[$"Personality_{i}"].TextboxText
                };
                target.AddStakeholder(stakeholder);
            }
        }

        // Parse Functional Requirements
        string[] frLines = inputFields["Functional Requirements"].TextboxText
            .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        target.FunctionalRequirements = frLines.ToList();

        // Parse Non-Functional Requirements
        string[] nfrLines = inputFields["Non-Functional Requirements"].TextboxText
            .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        target.NonFunctionalRequirements = nfrLines.ToList();

        if (!isEditMode)
        {
            Scenarios.Add(target);
        }

        Form1 form1 = (Form1)this.FindForm();
        form1.ChangeView("Manage Scenarios", trackHistory: false);
    }

    private CustomTableLayoutPanel CreateSectionBlock()
    {
        var panel = new CustomTableLayoutPanel();
        panel.CornerRadius = 10;
        panel.Padding = new Padding(10);
        panel.Dock = DockStyle.Top;
        panel.BackColor = GlobalVariables.ColorLight;
        panel.AutoSize = true;

        panel.ColumnCount = 3;
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30f));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30f));

        return panel;
    }
}

