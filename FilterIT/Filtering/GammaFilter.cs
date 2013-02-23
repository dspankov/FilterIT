using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filtering
{
    internal static class GammaFilter
    {
        internal static Bitmap Apply(BitmapData bitmapData1, double r, double g, double b)
        {
            Bitmap returnMap = new Bitmap(bitmapData1.Width, bitmapData1.Height, PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0,
                                     returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);

            byte[] redGamma = new byte[256];
            byte[] greenGamma = new byte[256];
            byte[] blueGamma = new byte[256];

            // Calculates the new gamma of each color from 0 to 255.
            for (int i = 0; i < 256; ++i)
            {
                redGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / r)) + 0.5));

                greenGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / g)) + 0.5));

                blueGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / b)) + 0.5));
            }

            //int a = 0;
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        // Standard Gamma algorithm
                        imagePointer2[0] = blueGamma[imagePointer1[0]];
                        imagePointer2[1] = greenGamma[imagePointer1[1]];
                        imagePointer2[2] = redGamma[imagePointer1[2]];
                        imagePointer2[3] = imagePointer1[3];
                        //4 bytes per pixel
                        imagePointer1 += 4;
                        imagePointer2 += 4;
                    }
                    //4 bytes per pixel
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                    imagePointer2 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }
            }
            returnMap.UnlockBits(bitmapData2);
            return returnMap;
        }
    }
}
