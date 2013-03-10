using Filtering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filtering;

namespace FilterIT
{
    public static class Filtering
    {
        public static string ApplyFilters(this Bitmap bitmap)
        {
            string result = null;
            BitmapData bitmapData = LockBitmap(bitmap);
            result += InvertFilter.Apply(bitmapData).ToImageString();
            result += GrayscaleFilter.Apply(bitmapData).ToImageString();
            result += GammaFilter.Apply(bitmapData, 0.1, 0.5, 0.9).ToImageString(); // combo gamma filter from 0.0 to 1.0
            result += GammaFilter.Apply(bitmapData, 1.0, 0.0, 0.0).ToImageString(); // red gamma filter
            result += GammaFilter.Apply(bitmapData, 0.0, 1.0, 0.0).ToImageString(); // green gamma filter
            result += GammaFilter.Apply(bitmapData, 0.0, 0.0, 1.0).ToImageString(); // blue gamma filter
            //result += ColorFilter.Apply(bitmapData, 1.0, 0.0, 0.0).ToImageString(); // red color filter
            //result += ColorFilter.Apply(bitmapData, 0.0, 1.0, 0.0).ToImageString(); // green color filter
            //result += ColorFilter.Apply(bitmapData, 0.0, 0.0, 1.0).ToImageString(); // blue color filter
            bitmap.UnlockBits(bitmapData);
            return result;
        }

        private static BitmapData LockBitmap(Bitmap bitmap)
        {
            return bitmap.LockBits(new Rectangle(0, 0,
                                    bitmap.Width, bitmap.Height),
                                    ImageLockMode.ReadOnly,
                                    PixelFormat.Format32bppArgb);
        }
    }
}
