using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

class CustomLabelledRichTextBox : CustomTableLayoutPanel {

    private CustomLabel NameLabel;
    private RichTextBox TextBox;

    public CustomLabelledRichTextBox() {

        this.Margin = new Padding(0);
        this.Padding = new Padding(0);
        this.AutoSize = true;

        this.ColumnCount = 1;
        this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        this.RowCount = 2;
        this.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        this.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        NameLabel = new CustomLabel();
        NameLabel.Text = "Text";
        NameLabel.Font = new Font(GlobalVariables.AppFontName, 12, FontStyle.Bold);
        NameLabel.Dock = DockStyle.Top;
        NameLabel.AutoSize = true;
        NameLabel.Padding = new Padding(0,8,0,3);
        NameLabel.Margin = new Padding(0);
        this.Controls.Add(NameLabel, 0, 0);

        Panel borderPanel = new Panel();
        borderPanel.Dock = DockStyle.Fill;
        borderPanel.BackColor = GlobalVariables.ColorMedium;
        borderPanel.Padding = new Padding(2);
        borderPanel.Margin = new Padding(0);
        borderPanel.AutoSize = true;
        borderPanel.AutoScroll = false;
        this.Controls.Add(borderPanel, 0, 1);

        Panel innerBorderPanel = new Panel();
        innerBorderPanel.Dock = DockStyle.Fill;
        innerBorderPanel.BackColor = GlobalVariables.ColorPrimary;
        innerBorderPanel.Padding = new Padding(3);
        innerBorderPanel.Margin = new Padding(0);
        innerBorderPanel.AutoSize = true;
        innerBorderPanel.AutoScroll = false;
        borderPanel.Controls.Add(innerBorderPanel);

        TextBox = new RichTextBox();
        TextBox.BorderStyle = BorderStyle.None;
        TextBox.Dock = DockStyle.Top;
        TextBox.Margin = new Padding(3);
        TextBox.Text = "Text";
        TextBox.Multiline = false;
        TextBox.Font = new Font(GlobalVariables.AppFontName, 12, FontStyle.Regular);
        TextBox.Height = TextRenderer.MeasureText("Ag", TextBox.Font).Height;
        TextBox.Margin = new Padding(0, 0, 0, 2);
        innerBorderPanel.Controls.Add(TextBox);

    }

    public bool Multiline {

        get { return TextBox.Multiline; }
        set { TextBox.Multiline = value; }

    }

    public int TextBoxRowCount {

        set { TextBox.Height = TextRenderer.MeasureText("Ag", TextBox.Font).Height * value; }

    }

    public string LabelText {
    
        get { return NameLabel.Text; }
        set { NameLabel.Text = value; }
    
    }

    public string TextboxText {

        get { return TextBox.Text; }
        set { TextBox.Text = value; }

    }


}
