using Filtering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            result += SepiaFilter.Apply(bitmapData, 20).ToImageString();
            result += ContrastFilter.Apply(bitmapData, 50.0, false).ToImageString();
            result += ContrastFilter.Apply(bitmapData, 50.0, true).ToImageString();
            result += BrightnessFilter.Apply(bitmapData, 50).ToImageString();
            result += BrightnessFilter.Apply(bitmapData, -50).ToImageString();
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

        public static Bitmap Invert(this Bitmap bitmap)
        {
            BitmapData bitmapData = LockBitmap(bitmap);
            try
            {
                return InvertFilter.Apply(bitmapData);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        public static Bitmap Grayscale(this Bitmap bitmap)
        {
            BitmapData bitmapData = LockBitmap(bitmap);
            try
            {
                return GrayscaleFilter.Apply(bitmapData);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        public static Bitmap Sepia(this Bitmap bitmap)
        {
            BitmapData bitmapData = LockBitmap(bitmap);
            try
            {
                return SepiaFilter.Apply(bitmapData, 20);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        public static Bitmap FalseContrast(this Bitmap bitmap)
        {
            BitmapData bitmapData = LockBitmap(bitmap);
            try
            {
                return ContrastFilter.Apply(bitmapData, 50.0, false);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        public static Bitmap TrueContrast(this Bitmap bitmap)
        {
            BitmapData bitmapData = LockBitmap(bitmap);
            try
            {
                return ContrastFilter.Apply(bitmapData, 50.0, true);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        public static Bitmap PositiveBrightness(this Bitmap bitmap)
        {
            BitmapData bitmapData = LockBitmap(bitmap);
            try
            {
                return BrightnessFilter.Apply(bitmapData, 50);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        public static Bitmap NegativeBrightness(this Bitmap bitmap)
        {
            BitmapData bitmapData = LockBitmap(bitmap);
            try
            {
                return BrightnessFilter.Apply(bitmapData, -50);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        public static Bitmap ComboGamma(this Bitmap bitmap)
        {
            BitmapData bitmapData = LockBitmap(bitmap);
            try
            {
                return GammaFilter.Apply(bitmapData, 0.1, 0.5, 0.9);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        public static Bitmap RedGamma(this Bitmap bitmap)
        {
            BitmapData bitmapData = LockBitmap(bitmap);
            try
            {
                return GammaFilter.Apply(bitmapData, 1.0, 0.0, 0.0);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        public static Bitmap GreenGamma(this Bitmap bitmap)
        {
            BitmapData bitmapData = LockBitmap(bitmap);
            try
            {
                return GammaFilter.Apply(bitmapData, 0.0, 1.0, 0.0);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        public static Bitmap BlueGamma(this Bitmap bitmap)
        {
            BitmapData bitmapData = LockBitmap(bitmap);
            try
            {
                return GammaFilter.Apply(bitmapData, 0.0, 0.0, 1.0);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
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
