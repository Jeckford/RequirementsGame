using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public class ViewChat : Panel {

    private ChatLog ChatLog;

    public ViewChat() {

        this.Dock = DockStyle.Fill;

        string basePath = Application.StartupPath; // The folder where the exe is located, used for sample images

        //

        this.BackColor = Color.White;

        // Main Divider

        var verticalDivider = new CustomTableLayoutPanel {
            Dock = DockStyle.Fill,
            Padding = new Padding(0),
            ColumnCount = 3,
            RowCount = 1
        };

        verticalDivider.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 240f)); // Left
        verticalDivider.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 1f));   // Divider
        verticalDivider.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));  // Right

        verticalDivider.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

        this.Controls.Add(verticalDivider);

        // Left Panel

        var leftPanel = new TableLayoutPanel {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 4,
            Margin = new Padding(0)
        };

        leftPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        leftPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f));   // Chats Label
        leftPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));   // Profiles
        leftPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 1f));    // Divider
        leftPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80f));   // Senior Eng

        verticalDivider.Controls.Add(leftPanel, 0, 0);

        // Left Panel - Chat Label

        var chatsLabel = new Label {
            Text = "Chats",
            Font = new Font("Calibri", 18, FontStyle.Bold | FontStyle.Italic),
            Padding = new Padding(10, 0, 0, 0),
            Margin = new Padding(0),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft
        };

        leftPanel.Controls.Add(chatsLabel, 0, 0);

        // Left Panel - Profile List (this should be its own class)

        var profilePanel = new Panel {
            Dock = DockStyle.Fill,
            Margin = new Padding(0)
        };
        leftPanel.Controls.Add(profilePanel, 0, 1);

        var profiles = new List<(string, string, Bitmap)>{
            ("Helena Hills", "Restaurant Owner", new Bitmap(Path.Combine(basePath, "Images", "Helena.png"))),
            ("Carlo Emilio", "Customer", new Bitmap(Path.Combine(basePath, "Images", "Carlo.png"))),
            ("Oscar Davis", "Delivery Driver", new Bitmap(Path.Combine(basePath, "Images", "Oscar.png"))),
            ("Daniel Park", "Restaurant Staff", new Bitmap(Path.Combine(basePath, "Images", "Daniel.png")))
        };
        profiles.Reverse();

        foreach (var profile in profiles) {
            var profileLabel = new ProfileLabel {
                Dock = DockStyle.Top,
                ProfileName = profile.Item1,
                ProfileShortDescription = profile.Item2,
                ProfileImage = profile.Item3
            };
            profilePanel.Controls.Add(profileLabel);
        }

        // Left Panel - Senior Software Engineer

        var seniorEngLabel = new ProfileLabel {
            Dock = DockStyle.Top,
            ProfileName = "Gabriel Schwitz",
            ProfileShortDescription = "Senior Software Engineer",
            ProfileImage = new Bitmap(Path.Combine(basePath, "Images", "Gabriel.png"))
        };
        leftPanel.Controls.Add(seniorEngLabel, 0, 3);

        // Right Panel

        var rightPanel = new CustomTableLayoutPanel {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 4,
            Margin = new Padding(0)
        };

        rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60f));  // Profile
        rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 1f));   // Divider
        rightPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));  // Chat
        rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80f));  // Other

        verticalDivider.Controls.Add(rightPanel, 2, 0);

        var selectedProfileLabel = new ProfileLabel {
            Dock = DockStyle.Fill,
            ProfileName = "Gabriel Schwitz",
            ProfileShortDescription = "",
            ProfileImage = new Bitmap(Path.Combine(basePath, "Images", "Gabriel.png"))
        };
        rightPanel.Controls.Add(selectedProfileLabel, 0, 0);

        // Right Panel - Chat log

        ChatLog = new ChatLog {
            Dock = DockStyle.Fill
        };
        rightPanel.Controls.Add(ChatLog, 0, 2);

        // Right Panel - More to be added



        // Send some messages

        ChatLog.SendMessage("What’s the most important thing you need from the food delivery platform to make your life easier?", ChatLog.MessageActor.User);
        ChatLog.SendMessage("...", ChatLog.MessageActor.System);

    }

}