using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

public class ChatLog : Panel {

    public enum MessageActor { User, System }

    private Panel ChatPanel; // Used for chat message bubble stacking and auto left/right alignment
    private ChatMessageBubble MessageBubble;

    public ChatLog() {

        this.Margin = new Padding(0);
        this.AutoScroll = true;

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

                panel.Size = new Size(this.Width - 40, panel.Controls[0].Size.Height);

            }

        }

    }

    // ChatMessageBubble Class
    private class ChatMessageBubble : Label {

        public enum Corners { TopLeft, TopRight, BottomRight, BottomLeft }

        private int CornerRadius = 15; // Change the corner roundness on the bubble

        // Using static variables for quick bitmap access
        private static Bitmap TopLeftPiece;
        private static Bitmap TopRightPiece;
        private static Bitmap BottomLeftPiece;
        private static Bitmap BottomRightPiece;
        private static Color ParentBackColor = Color.Black;

        public ChatMessageBubble() {
            this.AutoSize = true;
            this.Padding = new Padding(5);
            this.MaximumSize = new Size(400, 0);
            this.Font = new Font("Calibri", 12, FontStyle.Regular);
        }

        private Bitmap GetRoundedCorner(Corners corner) {

            if (TopLeftPiece == null || (this.Parent != null && this.Parent.BackColor != ParentBackColor)) {

                if (this.Parent != null) ParentBackColor = this.Parent.BackColor;

                // Using the 4 quarters of a circle to create the rounded corners

                int diameter = CornerRadius * 2;
                Bitmap circleBitmap = new Bitmap(diameter, diameter, PixelFormat.Format32bppArgb);

                using (Graphics g = Graphics.FromImage(circleBitmap)) {

                    g.Clear(Color.Transparent);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.FillEllipse(Brushes.Black, new Rectangle(-1, -1, diameter + 1, diameter + 1));

                }

                for (int x = 0; x < circleBitmap.Width; x++) {

                    for (int y = 0; y < circleBitmap.Height; y++) {

                        Color pixel = circleBitmap.GetPixel(x, y);
                        int newAlpha = 255 - pixel.A;
                        circleBitmap.SetPixel(x, y, Color.FromArgb(newAlpha, ParentBackColor));

                    }

                }

                TopLeftPiece = circleBitmap.Clone(new Rectangle(0, 0, CornerRadius, CornerRadius), circleBitmap.PixelFormat);
                TopRightPiece = circleBitmap.Clone(new Rectangle(CornerRadius, 0, CornerRadius, CornerRadius), circleBitmap.PixelFormat);
                BottomLeftPiece = circleBitmap.Clone(new Rectangle(0, CornerRadius, CornerRadius, CornerRadius), circleBitmap.PixelFormat);
                BottomRightPiece = circleBitmap.Clone(new Rectangle(CornerRadius, CornerRadius, CornerRadius, CornerRadius), circleBitmap.PixelFormat);

            }

            switch (corner) {

                case Corners.TopLeft:

                    return TopLeftPiece;

                case Corners.TopRight:

                    return TopRightPiece;

                case Corners.BottomLeft:

                    return BottomLeftPiece;

                case Corners.BottomRight:

                    return BottomRightPiece;

                default:

                    throw new Exception("Unknown corner");

            }

        }

        // Draw rounded corners
        protected override void OnPaint(PaintEventArgs e) {

            e.Graphics.DrawImage(GetRoundedCorner(Corners.TopLeft), new Rectangle(0, 0, CornerRadius, CornerRadius));
            e.Graphics.DrawImage(GetRoundedCorner(Corners.TopRight), new Rectangle(this.Width - CornerRadius, 0, CornerRadius, CornerRadius));
            e.Graphics.DrawImage(GetRoundedCorner(Corners.BottomLeft), new Rectangle(0, this.Height - CornerRadius, CornerRadius, CornerRadius));
            e.Graphics.DrawImage(GetRoundedCorner(Corners.BottomRight), new Rectangle(this.Width - CornerRadius, this.Height - CornerRadius, CornerRadius, CornerRadius));

            base.OnPaint(e);

        }
    }
}