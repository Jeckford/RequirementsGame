using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System;
using System.Windows.Forms;

class CustomPanel : Panel {

    private RoundedCorners RoundedCorners;

    public CustomPanel() {

        this.ParentChanged += Me_ParentChanged;

        this.RoundedCorners = new RoundedCorners();

    }

    // -- Rounded Corners

    private Control PreviousParent;

    private void Me_ParentChanged(object sender, EventArgs e) {

        if (PreviousParent != null) {

            PreviousParent.BackColorChanged -= Me_ParentBackColorChanged;

        }

        if (this.Parent != null) {

            this.Parent.BackColorChanged += Me_ParentBackColorChanged;

            RoundedCorners.MaskColor = this.Parent.BackColor;

            PreviousParent = this.Parent;

        }

    }

    private void Me_ParentBackColorChanged(object sender, EventArgs e) {

        if (this.Parent != null) {

            RoundedCorners.MaskColor = this.Parent.BackColor;

        }

    }

    public int CornerRadius {

        get { return RoundedCorners.CornerRadius; }
        set { RoundedCorners.CornerRadius = value; }

    }

    protected override void OnPaint(PaintEventArgs e) {

        int CornerRadius = RoundedCorners.CornerRadius;

        if (RoundedCorners.CornerRadius != 0) {

            e.Graphics.DrawImage(RoundedCorners.TopLeftMask(), new Rectangle(0, 0, CornerRadius, CornerRadius));
            e.Graphics.DrawImage(RoundedCorners.TopRightMask(), new Rectangle(this.Width - CornerRadius, 0, CornerRadius, CornerRadius));
            e.Graphics.DrawImage(RoundedCorners.BottomLeftMask(), new Rectangle(0, this.Height - CornerRadius, CornerRadius, CornerRadius));
            e.Graphics.DrawImage(RoundedCorners.BottomRightMask(), new Rectangle(this.Width - CornerRadius, this.Height - CornerRadius, CornerRadius, CornerRadius));

        }

        base.OnPaint(e);

    }


}

