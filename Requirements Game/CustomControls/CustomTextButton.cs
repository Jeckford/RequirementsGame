using System;
using System.Drawing;
using System.Windows.Forms;

class CustomTextButton : CustomLabel {

    public Color _idleBackColor;
    public Color EnterBackColor { get; set; }
    public Color DownBackColor { get; set; }

    public CustomTextButton() { 
    
        this.CornerRadius = 12;
        this.BackColor = Color.Gray;
        this.IdleBackColor = Color.FromArgb(200, 200, 200);
        this.EnterBackColor = Color.FromArgb(220, 220, 220);
        this.DownBackColor = Color.FromArgb(240, 240, 240);

        this.TextAlign = ContentAlignment.MiddleCenter;

        this.MouseDown += Me_MouseDown;
        this.MouseEnter += Me_MouseEnter;
        this.MouseUp += Me_MouseUp;
        this.MouseLeave += Me_MouseLeave;


    }

    public Color IdleBackColor {

        get => _idleBackColor;

        set {

            _idleBackColor = value;
            this.BackColor = value;

        }
    }

    // -- Events --

    private void Me_MouseDown(object sender, MouseEventArgs e) {

        if (e.Button == MouseButtons.Left) {

            this.BackColor = DownBackColor == null ? IdleBackColor : DownBackColor;
            this.Refresh();

        }

    }

    private void Me_MouseEnter(object sender, EventArgs e) {

        this.BackColor = EnterBackColor == null ? IdleBackColor : EnterBackColor;

    }

    private void Me_MouseLeave(object sender, EventArgs e) {

        this.BackColor = IdleBackColor;

    }

    private void Me_MouseUp(object sender, MouseEventArgs e) {

        this.BackColor = EnterBackColor == null ? IdleBackColor : EnterBackColor;

    }

}
