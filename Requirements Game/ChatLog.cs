using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

public class ChatLog : Panel {

    public enum MessageActor { User, System }

    private Panel ChatPanel; // Used for chat message bubble stacking and auto left/right alignment
    private ChatMessageBubble MessageBubble;

    // Wipes the chat UI back to an empty state
    public void Clear()
    {
        this.Controls.Clear();
        ChatPanel = null;
        MessageBubble = null;
        this.AutoScrollPosition = new Point(0, 0);
    }

    public ChatLog() {

        this.Margin = new Padding(0);
        this.AutoScroll = true;
        this.HorizontalScroll.Maximum = 0;
        this.HorizontalScroll.Visible = false;
        this.AutoScrollMinSize = new Size(0, this.AutoScrollMinSize.Height);

        this.SizeChanged += ChatMessageBubble_SizeChanged;

    }

    public void SendMessage(string message, MessageActor actor) {

        string actorName = actor == MessageActor.System ? "System" : "User";

        // Create new bubble if actor changes
        if (MessageBubble == null || MessageBubble.Name != actorName) {

            int panelLocationY = ChatPanel == null ? 20 : ChatPanel.Location.Y + ChatPanel.Height + 20;

            MessageBubble = new ChatMessageBubble {
                Name = actorName,
                Dock = actor == MessageActor.System ? DockStyle.Left : DockStyle.Right,
                BackColor = actor == MessageActor.System ? Color.FromArgb(224, 224, 224) : Color.Black,
                ForeColor = actor == MessageActor.System ? Color.Black : Color.White
            };

            ChatPanel = new Panel {
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Size = new Size(this.Width, MessageBubble.Height + 10),
                Location = new Point(20, panelLocationY)
            };

            this.Controls.Add(ChatPanel);
            ChatPanel.Controls.Add(MessageBubble);

            MessageBubble.SizeChanged += ChatMessageBubble_SizeChanged;
        }

        MessageBubble.Text = message;

    }

    // Adjust panel sizes if the form changes
    private void ChatMessageBubble_SizeChanged(object sender, EventArgs e) {

        foreach (Control control in this.Controls) {

            if (control is Panel panel && panel.Controls.Count > 0) {

                panel.Size = new Size(this.Width - 50, panel.Controls[0].Size.Height);

            }

        }

        if (this.VerticalScroll.Visible) {

            this.AutoScrollPosition = new Point(0, this.VerticalScroll.Maximum);

        }

    }

    // ChatMessageBubble Class
    private class ChatMessageBubble : CustomLabel {

        public ChatMessageBubble() {
            this.AutoSize = true;
            this.Padding = new Padding(5);
            this.MaximumSize = new Size(400, 0);
            this.Font = new Font("Calibri", 12, FontStyle.Regular);
            this.CornerRadius = 15;
        }

    }
}