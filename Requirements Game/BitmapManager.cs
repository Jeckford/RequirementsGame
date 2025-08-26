using System.Drawing;
using System.Drawing.Imaging;

class BitmapManager {

    public static Bitmap ChangeColor(Bitmap image, Color newColor) {

        if (image is null) return null;

        Bitmap newImage = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);

        for (int x = 0; x < image.Width; x++) {

            for (int y = 0; y < image.Height; y++) {

                int alpha = image.GetPixel(x, y).A;

                newImage.SetPixel(x, y, Color.FromArgb(alpha, newColor));

            }

        }

        return newImage;

    }

}
