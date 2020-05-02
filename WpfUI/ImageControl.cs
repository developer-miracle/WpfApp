using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
//using System.Drawing.Imaging;

namespace WpfUI
{
    class ImageControl
    {
        private Bitmap captured;
        public Bitmap Foo()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            int colourDepth = Screen.PrimaryScreen.BitsPerPixel;
            PixelFormat format;
            switch (colourDepth)
            {
                case 8:
                case 16:
                    format = PixelFormat.Format16bppRgb565;
                    break;

                case 24:
                    format = PixelFormat.Format24bppRgb;
                    break;

                case 32:
                    format = PixelFormat.Format32bppArgb;
                    break;

                default:
                    format = PixelFormat.Format32bppArgb;
                    break;
            }
            captured = new Bitmap(rectangle.Width, rectangle.Height, format);
            Graphics gdi = Graphics.FromImage(captured);

            gdi.CopyFromScreen(rectangle.Left, rectangle.Top, 0, 0, rectangle.Size);

            return captured;
        }
    }
}
