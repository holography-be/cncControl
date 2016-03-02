using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessing
{
   public class Image8Bit : IDisposable
   {
      private BitmapData bmd;
      private Bitmap b;
      /// Bitmap reference
      public Image8Bit (Bitmap bitmap)
      {
         if(bitmap.PixelFormat!=System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
            throw(new System.Exception("Invalid PixelFormat. 8 bit indexed required"));
         b = bitmap; //Store a private reference to the bitmap
         bmd = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
                          ImageLockMode.ReadWrite, b.PixelFormat);
      }
      
      /// <summary>

      /// Releases memory
      /// </summary>

      public void Dispose()
      {
         b.UnlockBits(bmd);
      }
      
      /// <summary>

      /// Gets color of an 8bit-pixel
      /// </summary>

      /// <param name="x">Row</param>
      /// <param name="y">Column</param>
      /// <returns>Color of pixel</returns>
      public unsafe System.Drawing.Color GetPixel(int x, int y)
      {
         byte* p = (byte *)bmd.Scan0.ToPointer();
         //always assumes 8 bit per pixels
         int offset=y*bmd.Stride+x;
         return GetColorFromIndex(p[offset]);
      }      

      /// <summary>

      /// Sets color of an 8bit-pixel
      /// </summary>

      /// <param name="x">Row</param>
      /// <param name="y">Column</param>
      /// <param name="c">Color index</param>
      public unsafe void SetPixel(int x, int y, byte c)
      {
         byte* p = (byte *)bmd.Scan0.ToPointer();
         //always assumes 8 bit per pixels
         int offset=y*bmd.Stride+(x);
         p[offset] = c;
      }
      
      /// <summary>

      /// Sets the palette for the referenced image to Grayscale
      /// </summary>

      public void MakeGrayscale()
      {
         SetGrayscalePalette(this.b);
      }
      
      /// <summary>

      /// Sets the palette of an image to grayscales (0=black, 255=white)
      /// </summary>

      /// <param name="b">Bitmap to set palette on</param>
      public static void SetGrayscalePalette(Bitmap b)
      {
         ColorPalette pal = b.Palette;
         for(int i = 0; i < 256; i++)
            pal.Entries[i] = Color.FromArgb( 255, i, i, i );
         b.Palette = pal;
      }
      
      private System.Drawing.Color GetColorFromIndex(byte c)
      {
         return b.Palette.Entries[c];
      }

       public int Height {
           get {
               return b.Height;
           }
       }

       public int Width {
           get {
               return b.Width;
           }
       }

       public Bitmap bitmap {
           get {
            return b;
           }

       }
   }
}

