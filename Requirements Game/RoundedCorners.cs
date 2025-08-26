using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

class RoundedCorners {

    private int _CornerRadius;
    private Color _MaskColor;

    private Bitmap _TopLeftMask;
    private Bitmap _TopRightMask;
    private Bitmap _BottomLeftMask;
    private Bitmap _BottomRightMask;

    public Bitmap TopLeftMask() { return _TopLeftMask; }
    public Bitmap TopRightMask() { return _TopRightMask; }
    public Bitmap BottomLeftMask() { return _BottomLeftMask; }
    public Bitmap BottomRightMask() { return _BottomRightMask; }

    public RoundedCorners() {

        _CornerRadius = 0;
        _MaskColor = Color.White;

    }

    public Color MaskColor {

        get { return _MaskColor; }

        set {

            _MaskColor = value;

            UpdateCornerMarks();

        }

    }

    public int CornerRadius {

        get { return _CornerRadius; }

        set  {

            if (value < 0) { throw new Exception("CornerRadius cannot be less than 0"); }

            _CornerRadius = value;

            UpdateCornerMarks();

        }

    }

    private void UpdateCornerMarks() {

        if (_CornerRadius == 0) {

            _TopLeftMask = null;
            _TopRightMask = null;
            _BottomLeftMask = null;
            _BottomRightMask = null;

            return; 

        }

        // Using the 4 quarters of a circle to create the rounded corners

        int diameter = CornerRadius * 2;
        Bitmap circleBitmap = new Bitmap(diameter, diameter, PixelFormat.Format32bppArgb);

        using (Graphics g = Graphics.FromImage(circleBitmap)) {

            g.Clear(Color.Transparent);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillEllipse(Brushes.Black, new Rectangle(-1, -1, diameter + 1, diameter + 1));

        }

        // Invert the bitmap

        for (int x = 0; x < circleBitmap.Width; x++) {

            for (int y = 0; y < circleBitmap.Height; y++) {

                Color pixel = circleBitmap.GetPixel(x, y);
                int newAlpha = 255 - pixel.A;
                circleBitmap.SetPixel(x, y, Color.FromArgb(newAlpha, _MaskColor));

            }

        }

        // Update the four bitmap masks

        _TopLeftMask = circleBitmap.Clone(new Rectangle(0, 0, CornerRadius, CornerRadius), circleBitmap.PixelFormat);
        _TopRightMask = circleBitmap.Clone(new Rectangle(CornerRadius, 0, CornerRadius, CornerRadius), circleBitmap.PixelFormat);
        _BottomLeftMask = circleBitmap.Clone(new Rectangle(0, CornerRadius, CornerRadius, CornerRadius), circleBitmap.PixelFormat);
        _BottomRightMask = circleBitmap.Clone(new Rectangle(CornerRadius, CornerRadius, CornerRadius, CornerRadius), circleBitmap.PixelFormat);

    }

}
