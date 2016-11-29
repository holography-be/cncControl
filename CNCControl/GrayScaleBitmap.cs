using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace CNCControl
{
    public static class GrayScaleBitmap
    {

        public enum GrayScaleMethod { AUTOMATIC = 0, RED=1, GREEN=2, BLUE=3, DESATURATION=4, DECOMPOSITION_MIN=5, DECOMPOSITION_MAX=6 };

        private static ColorPalette _palette;
        private static bool _bPalette = false;

        private static void initPalette() {
            Bitmap tempImg = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            _palette = tempImg.Palette;
            for (int i = 0; i < 256; i++)
            {
                _palette.Entries[i] = Color.FromArgb(255, i, i, i);
            }
            _bPalette = true;
        }

        private static ColorPalette getPalette()
        {
            if (_bPalette == false) initPalette();
            return _palette;
        }

        public static Bitmap grayScale(Bitmap img, PixelFormat pf, GrayScaleMethod method)
        {
            Bitmap RetImg = new Bitmap(img);
            int bytesPerPixel;
            switch (img.PixelFormat) {
                case PixelFormat.Format24bppRgb: bytesPerPixel = 3; break;
                case PixelFormat.Format32bppArgb: bytesPerPixel = 4; break;
                case PixelFormat.Format32bppRgb: bytesPerPixel = 4; break;
                default: throw new InvalidOperationException("Image format not supported");
            }
            //ColorPalette palette = RetImg.Palette;
            //for (int i = 0; i < 256; i++)
            //{
            //    palette.Entries[i] = Color.FromArgb(255, i, i, i);
            //}
            //RetImg.Palette = palette;
            ////RetImg.Palette = getPalette();
            LockBitmap lockRetImg = new LockBitmap(RetImg);
            LockBitmap lockImg = new LockBitmap(img);
            int w = img.Width;
            int h = img.Height;
            Color pixel;
            Color newColor;
            byte indexColor;
            
            lockImg.LockBits();
            lockRetImg.LockBits();
            byte a, r, g, b;
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    pixel = lockImg.GetPixel(x, y);
                    a = pixel.A; r = pixel.R; g = pixel.G; b = pixel.B;
                    switch (method)
                    {
                        case GrayScaleMethod.AUTOMATIC:
                            if (bytesPerPixel == 3)
                            {
                                indexColor = (byte)(int)(0.299f * r + 0.587f * g + 0.114 * b);
                            }
                            else
                            {
                                indexColor = (byte)(int)((a / 255.0f) * (0.299f * r + 0.587f * g + 0.114 * b));
                            }
                            newColor = Color.FromArgb(0, (byte)(0.299f * r), (byte)(0.587f * g), (byte)(0.114 * b));
                            break;
                        case GrayScaleMethod.RED:
                            newColor = Color.FromArgb(a,r,r,r);
                            break;
                        case GrayScaleMethod.GREEN:
                            newColor = Color.FromArgb(a, g, g, g);
                            break;
                        case GrayScaleMethod.BLUE:
                            newColor = Color.FromArgb(a, b, b, b);
                            break;
                        case GrayScaleMethod.DESATURATION:
                            indexColor = (byte)((Math.Max(r, Math.Max(g, b)) + Math.Min(r, Math.Min(g, b))) / 2);
                            newColor = Color.FromArgb(255,indexColor,indexColor,indexColor);
                            break;
                        case GrayScaleMethod.DECOMPOSITION_MIN:
                            indexColor = (Math.Min(r, Math.Min(g, b)));
                            newColor = Color.FromArgb(255,indexColor,indexColor,indexColor);
                            break;
                        case GrayScaleMethod.DECOMPOSITION_MAX:
                            indexColor = (Math.Max(r, Math.Max(g, b)));
                            newColor = Color.FromArgb(255,indexColor,indexColor,indexColor);
                            break;
                        default:
                            newColor = Color.FromArgb(255,127,127,127);
                            break;
                    }
                    lockRetImg.SetPixel(x, y,newColor);
                }
            }
            lockImg.UnlockBits();
            lockRetImg.UnlockBits();
            RetImg.Save("c:\\temp\\test.bmp", ImageFormat.Bmp);
            return RetImg;
        }


    }
}
