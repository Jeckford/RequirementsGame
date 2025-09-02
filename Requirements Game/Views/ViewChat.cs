using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public class ViewChat : Panel {

    private ChatLog ChatLog;

    public ViewChat() {

        this.Dock = DockStyle.Fill;
        this.Margin = new Padding(0);

        string basePath = Application.StartupPath; // The folder where the exe is located, used for sample images

        //

        this.BackColor = Color.White;

        // Main Divider

        CustomTableLayoutPanel verticalDivider = new CustomTableLayoutPanel();
        verticalDivider.Dock = DockStyle.Fill;
        verticalDivider.Padding = new Padding(0);

        verticalDivider.ColumnCount = 3;    
        verticalDivider.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 240f)); // Left
        verticalDivider.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 1f));   // Divider
        verticalDivider.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));  // Right

        verticalDivider.RowCount = 1;
        verticalDivider.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

        this.Controls.Add(verticalDivider);

        // Divider

        Panel mainDivider = new Panel();
        mainDivider.Dock = DockStyle.Fill;
        mainDivider.Margin = new Padding(0);
        mainDivider.BackColor = GlobalVariables.ColorLight;

        verticalDivider.Controls.Add(mainDivider, 1, 0);

        // Left Panel

        CustomTableLayoutPanel leftPanel = new CustomTableLayoutPanel();
        leftPanel.Dock = DockStyle.Fill;       
        leftPanel.Margin = new Padding(0);

        leftPanel.ColumnCount = 1;
        leftPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

        leftPanel.RowCount = 4;
        leftPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f));   // Chats Label
        leftPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));   // Profiles
        leftPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 1f));    // Divider
        leftPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80f));   // Senior Eng

        verticalDivider.Controls.Add(leftPanel, 0, 0);

        // Left Panel - Chat Label

        Label chatsLabel = new Label();
        chatsLabel.Text = "Chats";
        chatsLabel.Font = new Font("Calibri", 18, FontStyle.Bold | FontStyle.Italic);
        chatsLabel.Padding = new Padding(10, 0, 0, 0);
        chatsLabel.Margin = new Padding(0);
        chatsLabel.Dock = DockStyle.Fill;
        chatsLabel.TextAlign = ContentAlignment.MiddleLeft;
       
        leftPanel.Controls.Add(chatsLabel, 0, 0);

        // Left Panel - Profile List (this should be its own class)

        Panel profilePanel = new Panel();
        profilePanel.Dock = DockStyle.Fill;
        profilePanel.Margin = new Padding(0);

        leftPanel.Controls.Add(profilePanel, 0, 1);

        var profiles = new List<(string, string, Bitmap)>{
            ("Helena Hills", "Restaurant Owner", new Bitmap(Path.Combine(basePath, "Images", "Helena.png"))),
            ("Carlo Emilio", "Customer", new Bitmap(Path.Combine(basePath, "Images", "Carlo.png"))),
            ("Oscar Davis", "Delivery Driver", new Bitmap(Path.Combine(basePath, "Images", "Oscar.png"))),
            ("Daniel Park", "Restaurant Staff", new Bitmap(Path.Combine(basePath, "Images", "Daniel.png")))
        };
        profiles.Reverse();

        foreach (var profile in profiles) {

            var profileLabel = new ProfileLabel();
            profileLabel.Dock = DockStyle.Top;
            profileLabel.ProfileName = profile.Item1;
            profileLabel.ProfileShortDescription = profile.Item2;
            profileLabel.ProfileImage = profile.Item3;
            
            profilePanel.Controls.Add(profileLabel);

        }

        // Divider

        Panel LeftPanelDivider = new Panel();
        LeftPanelDivider.Dock = DockStyle.Fill;
        LeftPanelDivider.Margin = new Padding(0);
        LeftPanelDivider.BackColor = GlobalVariables.ColorLight;

        leftPanel.Controls.Add(LeftPanelDivider, 0, 2);

        // Left Panel - Senior Software Engineer

        var seniorEngLabel = new ProfileLabel();
        seniorEngLabel.Dock = DockStyle.Top;
        seniorEngLabel.ProfileName = "Gabriel Schwitz";
        seniorEngLabel.ProfileShortDescription = "Senior Software Engineer";
        seniorEngLabel.ProfileImage = new Bitmap(Path.Combine(basePath, "Images", "Gabriel.png"));

        leftPanel.Controls.Add(seniorEngLabel, 0, 3);

        // Right Panel

        var rightPanel = new CustomTableLayoutPanel();
        rightPanel.Dock = DockStyle.Fill;
        rightPanel.ColumnCount = 1;        
        rightPanel.Margin = new Padding(0);

        rightPanel.RowCount = 5;
        rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60f));  // Profile
        rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 1f));   // Divider
        rightPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));  // Chat
        rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 26f));  // Chat
        rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80f));  // Other

        verticalDivider.Controls.Add(rightPanel, 2, 0);

        var selectedProfileLabel = new ProfileLabel();
        selectedProfileLabel.Dock = DockStyle.Fill;
        selectedProfileLabel.ProfileName = "Gabriel Schwitz";
        selectedProfileLabel.ProfileShortDescription = "";
        selectedProfileLabel.ProfileImage = new Bitmap(Path.Combine(basePath, "Images", "Gabriel.png"));

        rightPanel.Controls.Add(selectedProfileLabel, 0, 0);

        // Divider

        Panel rightPanelDivider = new Panel();
        rightPanelDivider.Dock = DockStyle.Fill;
        rightPanelDivider.Margin = new Padding(0);
        rightPanelDivider.BackColor = GlobalVariables.ColorLight;

        rightPanel.Controls.Add(rightPanelDivider, 0, 1);

        // Right Panel - Chat log

        ChatLog = new ChatLog();
        ChatLog.Dock = DockStyle.Fill;

        rightPanel.Controls.Add(ChatLog, 0, 2);

        // Right Panel - More to be added

        TextBox messageTextBox = new TextBox();
        messageTextBox.Dock = DockStyle.Fill;
        messageTextBox.Margin = new Padding(10,0,10,0);
        messageTextBox.Font = new Font(GlobalVariables.AppFontName, 12, FontStyle.Regular);
        rightPanel.Controls.Add(messageTextBox, 0, 3);

        messageTextBox.PreviewKeyDown += MessageTextBox_PreviewKeyDown;

    }

    private void MessageTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {

        if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return) {

            TextBox messageTextBox = (TextBox)sender;

            ChatLog.SendMessage(messageTextBox.Text, ChatLog.MessageActor.User);
            ChatLog.SendMessage("...", ChatLog.MessageActor.System);

            GlobalVariables.ChatReply = "";

            BackgroundWorker tempWorker = new BackgroundWorker();
            tempWorker.DoWork += TempWorker_DoWork;
            tempWorker.RunWorkerAsync(messageTextBox.Text);

            Timer tempTimer = new Timer();
            tempTimer.Interval = 100;
            tempTimer.Enabled = true;
            tempTimer.Tick += TempTimer_Tick;


            messageTextBox.Text = "";

        } 

    }

    private void TempTimer_Tick(object sender, System.EventArgs e) {

        if (GlobalVariables.ChatReply != "") {

            ChatLog.SendMessage(GlobalVariables.ChatReply, ChatLog.MessageActor.System);



        }
     
    }

    private void TempWorker_DoWork(object sender, DoWorkEventArgs e) {

        string message = (string)e.Argument;

        LLMServerClient.SendMessage(message);

    }

}