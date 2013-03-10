using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Filtering;
using System.Drawing;
namespace FilterIT.Controllers
{
    public class DownloadController : Controller
    {
        // GET: /Download/

        public ActionResult Index()
        {
            return View();
        }

        public FileResult Invert()
        {
            return GetFilteredImage("Invert");
        }

        public FileResult Grayscale()
        {
            return GetFilteredImage("Grayscale");
        }

        public FileResult Sepia()
        {
            return GetFilteredImage("Sepia");
        }

        public FileResult FalseContrast()
        {
            return GetFilteredImage("FalseContrast");
        }

        public FileResult TrueContrast()
        {
            return GetFilteredImage("TrueContrast");
        }

        public FileResult PositiveBrightness()
        {
            return GetFilteredImage("PositiveBrightness");
        }

        public FileResult NegativeBrightness()
        {
            return GetFilteredImage("NegativeBrightness");
        }

        public FileResult ComboGamma()
        {
            return GetFilteredImage("ComboGamma");
        }

        public FileResult RedGamma()
        {
            return GetFilteredImage("RedGamma");
        }

        public FileResult GreenGamma()
        {
            return GetFilteredImage("GreenGamma");
        }

        public FileResult BlueGamma()
        {
            return GetFilteredImage("BlueGamma");
        }


        private FileResult GetFilteredImage(string filterName)
        {
            var converter = new ImageConverter();
            var originalImage = Session["Image"] as Bitmap;
            var filteredImage = ApplyFilter(originalImage, filterName);
            
            var imageData = (byte[])converter.ConvertTo(filteredImage, typeof(byte[]));

            return File(imageData, "application/octet-stream",
                string.Format("{0}_{1}.png", Session["ImageName"], filterName));
        }

        private static Bitmap ApplyFilter(Bitmap originalImage, string filterName)
        {
            switch (filterName)
            {
                case "Invert":
                    return originalImage.Invert();
                case "Grayscale":
                    return originalImage.Grayscale();
                case "Sepia":
                    return originalImage.Sepia();
                case "FalseContrast":
                    return originalImage.FalseContrast();
                case "TrueContrast":
                    return originalImage.TrueContrast();
                case "PositiveBrightness":
                    return originalImage.PositiveBrightness();
                case "NegativeBrightness":
                    return originalImage.NegativeBrightness();
                case "ComboGamma":
                    return originalImage.ComboGamma();
                case "RedGamma":
                    return originalImage.RedGamma();
                case "GreenGamma":
                    return originalImage.GreenGamma();
                case "BlueGamma":
                    return originalImage.BlueGamma();
            }
            return originalImage;
        }
    }
}
