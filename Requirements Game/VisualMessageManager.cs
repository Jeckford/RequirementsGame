using System.Windows.Forms;
using System.Drawing;
using Requirements_Game.Properties;

class VisualMessageManager {

    private static Form MessageForm;

    static VisualMessageManager() {

        MessageForm = new Form();
        MessageForm.FormBorderStyle = FormBorderStyle.None;
        MessageForm.BackColor = Color.AliceBlue;
        MessageForm.TransparencyKey = Color.AliceBlue;
        MessageForm.Owner = GlobalVariables.MainForm;
        MessageForm.AutoScroll = false;
        MessageForm.Show();

        UpdateMessageFormPosition();

        GlobalVariables.MainForm.Move += (s, e) => UpdateMessageFormPosition();
        GlobalVariables.MainForm.Resize += (s, e) => UpdateMessageFormPosition();

    }

    static void UpdateMessageFormPosition() {

        MessageForm.Width = GlobalVariables.MainForm.Width / 3 * 2;

        var requiredHeight = 0;

        foreach (Control control in MessageForm.Controls) {

            requiredHeight += control.Height;
            requiredHeight += control.Margin.Top;
            requiredHeight += control.Margin.Bottom;

        }

        MessageForm.Height = requiredHeight;

        MessageForm.Location = new Point(
            GlobalVariables.MainForm.Left + (GlobalVariables.MainForm.Width - MessageForm.Width) / 2,
            GlobalVariables.MainForm.Bottom - MessageForm.Height - 10
        );

    }

    public static void ShowMessage(string Message) {

        var messageTableLayoutPanel = new CustomTableLayoutPanel();
        messageTableLayoutPanel.CornerRadius = 10;
        messageTableLayoutPanel.Margin = new Padding(0);
        messageTableLayoutPanel.Dock = DockStyle.Bottom;
        messageTableLayoutPanel.BackColor = GlobalVariables.ColorMedium;
        messageTableLayoutPanel.AutoSize = true;

        messageTableLayoutPanel.RowCount = 1;
        messageTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        messageTableLayoutPanel.ColumnCount = 3;
        messageTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        messageTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40));
        messageTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10));

        MessageForm.Controls.Add(messageTableLayoutPanel);

        var spacer = new Panel();
        spacer.BackColor = Color.AliceBlue;
        spacer.Size = new Size(5, 5);
        spacer.Margin = new Padding(0);
        spacer.Dock = DockStyle.Bottom;
        MessageForm.Controls.Add(spacer);

        // 
        var messageLabel = new Label();
        messageLabel.Text = Message;
        messageLabel.Font = new Font(GlobalVariables.AppFontName, 11, FontStyle.Regular);
        messageLabel.TextAlign = ContentAlignment.MiddleLeft;
        messageLabel.Dock = DockStyle.Fill;
        messageLabel.Margin = new Padding(10);
        messageLabel.AutoSize = true;
        messageTableLayoutPanel.Controls.Add(messageLabel, 0, 0);

        // 
        var closeIcon = (Bitmap)Resources.ResourceManager.GetObject("close");
        closeIcon = new Bitmap(closeIcon, 20, 20);

        var closeButton = new CustomPictureBox();
        closeButton.Dock = DockStyle.Fill;
        closeButton.SizeMode = PictureBoxSizeMode.CenterImage;
        closeButton.Image = closeIcon;
        closeButton.IdleImage = BitmapManager.ChangeColor(closeIcon, Color.FromArgb(0, 0, 0));
        closeButton.EnterImage = BitmapManager.ChangeColor(closeIcon, Color.FromArgb(100, 100, 100));
        closeButton.DownImage = BitmapManager.ChangeColor(closeIcon, Color.FromArgb(160, 160, 160));

        messageTableLayoutPanel.Controls.Add(closeButton, 1, 0);

        closeButton.Click += (sender, e) => {

            messageTableLayoutPanel.Dispose();
            UpdateMessageFormPosition();

        };

        UpdateMessageFormPosition();

    }

}
