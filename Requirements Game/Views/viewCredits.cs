using System;
using System.Drawing;
using System.Windows.Forms;
using Requirements_Game;

public class ViewCredits : View
{
    public ViewCredits()
    {
        ViewTableLayoutPanel.Dock = DockStyle.Fill;
        ViewTableLayoutPanel.BackColor = GlobalVariables.ColorPrimary;

        // Center content horizontally
        ViewTableLayoutPanel.ColumnCount = 3;
        ViewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
        ViewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        ViewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

        ViewTableLayoutPanel.RowCount = 2;
        ViewTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 90f));
        ViewTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));

        var contentPanel = new TableLayoutPanel
        {
            ColumnCount = 1,
            RowCount = 5,
            AutoSize = true,
            BackColor = GlobalVariables.ColorPrimary,
            Anchor = AnchorStyles.None
        };

        contentPanel.Controls.Add(CreateHeaderLabel("Developed by:"), 0, 0);
        contentPanel.Controls.Add(CreateNameWithLink("Courtney Hemmett", "https://github.com/Pleewto"), 0, 1);
        contentPanel.Controls.Add(CreateNameWithLink("Jarron Eckford", "https://github.com/Jeckford"), 0, 2);
        contentPanel.Controls.Add(CreateNameWithLink("Cory Crombie", "https://github.com/KorraOne"), 0, 3);
        contentPanel.Controls.Add(CreateNameWithLink("Mai Le", "https://github.com/ttle11"), 0, 4);

        ViewTableLayoutPanel.Controls.Add(contentPanel, 1, 0);

        var footerPanel = new TableLayoutPanel
        {
            ColumnCount = 1,
            RowCount = 2,
            AutoSize = true,
            BackColor = GlobalVariables.ColorPrimary,
            Anchor = AnchorStyles.None
        };

        footerPanel.Controls.Add(CreateFooterLabel("29/10/2025 – ECU"), 0, 0);
        footerPanel.Controls.Add(CreateFooterLabel("Thank you to Martin and Luke"), 0, 1);

        ViewTableLayoutPanel.Controls.Add(footerPanel, 1, 1);
    }

    private Label CreateHeaderLabel(string text)
    {
        return new Label
        {
            Text = text,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font(GlobalVariables.AppFontName, 20, FontStyle.Bold),
            ForeColor = GlobalVariables.ColorDark,
            AutoSize = true,
            Anchor = AnchorStyles.None
        };
    }

    private LinkLabel CreateNameWithLink(string name, string url)
    {
        var linkLabel = new LinkLabel
        {
            Text = name,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font(GlobalVariables.AppFontName, 14, FontStyle.Regular),
            LinkColor = GlobalVariables.ColorDark,
            ActiveLinkColor = GlobalVariables.ColorDark,
            LinkBehavior = LinkBehavior.NeverUnderline,
            BackColor = GlobalVariables.ColorPrimary,
            ForeColor = GlobalVariables.ColorDark,
            Cursor = Cursors.Hand,
            AutoSize = true,
            Anchor = AnchorStyles.None
        };

        linkLabel.Links.Add(0, name.Length, url);

        // Tooltip
        var tooltip = new ToolTip();
        tooltip.SetToolTip(linkLabel, "View GitHub profile");

        linkLabel.LinkClicked += (s, e) =>
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = e.Link.LinkData.ToString(),
                UseShellExecute = true
            });
        };

        return linkLabel;
    }

    private Label CreateFooterLabel(string text)
    {
        return new Label
        {
            Text = text,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font(GlobalVariables.AppFontName, 10, FontStyle.Italic),
            ForeColor = Color.Gray,
            AutoSize = true,
            Anchor = AnchorStyles.None
        };
    }
}