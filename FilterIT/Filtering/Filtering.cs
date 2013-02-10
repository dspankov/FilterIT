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
            bitmap.UnlockBits(bitmapData);
            return result;
        }

        private static string ToImageString(this Bitmap img)
        {
            string bitmapString = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                img.Save(memoryStream, ImageFormat.Jpeg);
                byte[] bitmapBytes = memoryStream.GetBuffer();
                bitmapString = Convert.ToBase64String(bitmapBytes,
                    Base64FormattingOptions.InsertLineBreaks);
            }
            bitmapString = "data:image/jpeg;base64," + bitmapString + " ";
            return bitmapString;
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
