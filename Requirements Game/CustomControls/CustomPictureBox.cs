using System;
using System.Drawing;
using System.Windows.Forms;

class CustomPictureBox : PictureBox {

    public Image _idleImage;
    public Image EnterImage { get; set; }
    public Image DownImage { get; set; }

    public CustomPictureBox() {

        this.SizeMode = PictureBoxSizeMode.CenterImage;
        this.BackColor = Color.Transparent;

        this.MouseDown += CustomPictureBox_MouseDown;
        this.MouseEnter += CustomPictureBox_MouseEnter;
        this.MouseLeave += CustomPictureBox_MouseLeave;
        this.MouseUp += CustomPictureBox_MouseUp;

    }

    public Image IdleImage {

        get => _idleImage;

        set
        {

            _idleImage = value;
            this.Image = value;

        }
    }

    private void CustomPictureBox_MouseDown(object sender, MouseEventArgs e) {

        if (e.Button == MouseButtons.Left) {

            this.Image = DownImage == null ? IdleImage : DownImage;
            this.Refresh();

        }

    }

    private void CustomPictureBox_MouseEnter(object sender, EventArgs e) {

        this.Image = EnterImage == null ? IdleImage : EnterImage;

    }

    private void CustomPictureBox_MouseLeave(object sender, EventArgs e) {

        this.Image = IdleImage;

    }

    private void CustomPictureBox_MouseUp(object sender, MouseEventArgs e) {

        this.Image = EnterImage == null ? IdleImage : EnterImage;

    }

}