using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

public class CustomTableLayoutPanel : TableLayoutPanel {

    private RoundedCorners RoundedCorners;

    public CustomTableLayoutPanel() {

        this.CellPaint += Me_CellPaint;
        this.Paint += Me_Paint;
        this.RoundedCorners = new RoundedCorners();

    }

    private void Me_CellPaint(object sender, TableLayoutCellPaintEventArgs e) {

        Rectangle cellRectangle = e.CellBounds;

        if (cellRectangle.Width == 1 || cellRectangle.Height == 1) {

            using (SolidBrush brush = new SolidBrush(GlobalVariables.LightGrey)) {

                e.Graphics.FillRectangle(brush, cellRectangle);

            }

        }

    }

    // For testing

    private void Me_Paint(object sender, PaintEventArgs e) {

        if (!GlobalVariables.DesignMode) { return; }

        Graphics g = e.Graphics;
        Pen gridPen = new Pen(Color.Black, 2);
        Brush textBrush = Brushes.Black;
        Font font = this.Font;

        int[] columnWidths = this.GetColumnWidths();
        int[] rowHeights = this.GetRowHeights();

        int x = 0;

        for (int col = 0; col < columnWidths.Length; col++) {

            int y = 0;

            for (int row = 0; row < rowHeights.Length; row++) {

                string label = $"C{col} R{row}";
                SizeF textSize = g.MeasureString(label, font);

                float textX = x + (columnWidths[col] - textSize.Width) / 2;
                float textY = y + (rowHeights[row] - textSize.Height) / 2;

                //g.DrawString(label, font, textBrush, textX, textY);

                y += rowHeights[row];

            }

            x += columnWidths[col];

        }

        // Draw vertical lines

        x = 0;

        for (int i = 0; i < columnWidths.Length - 1; i++) {

            x += columnWidths[i];
            g.DrawLine(gridPen, x, 0, x, this.Height);

        }

        // Draw horizontal lines

        int yLine = 0;

        for (int i = 0; i < rowHeights.Length - 1; i++) {

            yLine += rowHeights[i];
            g.DrawLine(gridPen, 0, yLine, this.Width, yLine);

        }

        gridPen.Dispose();


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
