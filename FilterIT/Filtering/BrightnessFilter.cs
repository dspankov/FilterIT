using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filtering
{
    internal static class BrightnessFilter
    {
        internal static Bitmap Apply(BitmapData bitmapData1, int brightness)
        {
            Bitmap returnMap = new Bitmap(bitmapData1.Width, bitmapData1.Height, PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0,
                                     returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        // Standard Brightness algorithm
                        imagePointer2[0] = (byte)(imagePointer1[0] + brightness);
                        if (imagePointer2[0] > 255)
                        {
                            imagePointer2[0] = 255;
                        }
                        else if (imagePointer2[0] < 0)
                        {
                            imagePointer2[0] = 0;
                        }

                        imagePointer2[1] = (byte)(imagePointer1[1] + brightness);
                        if (imagePointer2[1] > 255)
                        {
                            imagePointer2[1] = 255;
                        }
                        else if (imagePointer2[1] < 0)
                        {
                            imagePointer2[1] = 0;
                        }

                        imagePointer2[2] = (byte)(imagePointer1[2] + brightness);
                        if (imagePointer2[2] > 255)
                        {
                            imagePointer2[2] = 255;
                        }
                        else if (imagePointer2[2] < 0)
                        {
                            imagePointer2[2] = 0;
                        }


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
