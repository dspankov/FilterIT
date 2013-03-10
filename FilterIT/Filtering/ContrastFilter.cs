using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filtering
{
    internal static class ContrastFilter
    {
        internal static Bitmap Apply(BitmapData bitmapData1, double contrast, bool fake)
        {
            Bitmap returnMap = new Bitmap(bitmapData1.Width, bitmapData1.Height, PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0,
                                     returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
            if (!fake)
            {
                contrast = (100.0 + contrast) / 100.0;
                contrast *= contrast;
            }

            double C;

            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        // A custom Contrast algorithm
                        C = imagePointer1[2];
                        C /= 255.0;
                        C -= 0.5;
                        C *= contrast;
                        C += 0.5;
                        C *= 255;
                        if (C > 255)
                        {
                            C = 255;
                        }
                        else if (C < 0)
                        {
                            C = 0;
                        }

                        imagePointer2[2] = (byte)C;

                        C = imagePointer1[1];
                        C /= 255.0;
                        C -= 0.5;
                        C *= contrast;
                        C += 0.5;
                        C *= 255;
                        if (C > 255)
                        {
                            C = 255;
                        }
                        else if (C < 0)
                        {
                            C = 0;
                        }

                        imagePointer2[1] = (byte)C;

                        C = imagePointer1[0];
                        C /= 255.0;
                        C -= 0.5;
                        C *= contrast;
                        C += 0.5;
                        C *= 255;
                        if (C > 255)
                        {
                            C = 255;
                        }
                        else if (C < 0)
                        {
                            C = 0;
                        }

                        imagePointer2[0] = (byte)C;


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
