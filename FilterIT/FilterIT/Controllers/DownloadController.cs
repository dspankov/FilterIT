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
            ImageConverter converter = new ImageConverter();
            var originalImage = Session["Image"] as Bitmap;
            Bitmap filteredImage = null;
            switch (filterName)
            {
                case "Invert":
                    filteredImage = originalImage.Invert();
                    break;
                case "Grayscale":
                    filteredImage = originalImage.Grayscale();
                    break;
                case "Sepia":
                    filteredImage = originalImage.Sepia();
                    break;
                case "FalseContrast":
                    filteredImage = originalImage.FalseContrast();
                    break;
                case "TrueContrast":
                    filteredImage = originalImage.TrueContrast();
                    break;
                case "PositiveBrightness":
                    filteredImage = originalImage.PositiveBrightness();
                    break;
                case "NegativeBrightness":
                    filteredImage = originalImage.NegativeBrightness();
                    break;
                case "ComboGamma":
                    filteredImage = originalImage.ComboGamma();
                    break;
                case "RedGamma":
                    filteredImage = originalImage.RedGamma();
                    break;
                case "GreenGamma":
                    filteredImage = originalImage.GreenGamma();
                    break;
                case "BlueGamma":
                    filteredImage = originalImage.BlueGamma();
                    break;
            }
            
            var imageData = (byte[])converter.ConvertTo(filteredImage, typeof(byte[]));

            return File(imageData, "application/octet-stream",
                string.Format("{0}_{1}.png", Session["ImageName"], filterName));
        }

    }
}
