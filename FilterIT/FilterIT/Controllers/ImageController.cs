using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filtering;
using System.IO;

namespace FilterIT.Controllers
{
    public class ImageController : Controller
    {
        // GET: /Image/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public object GetThumbnails(HttpPostedFileBase fileUpload)
        {
            var stream = fileUpload.InputStream;
            Bitmap img = new Bitmap(stream);

            if(User.Identity.IsAuthenticated)
                SaveImage(fileUpload.FileName, img);

            int height = (int)(img.Height / ((float)img.Width / 500f));
            var filteredImages = img.Resize(500, height).ApplyFilters();

            Session["Image"] = img;
            Session["ImageName"] = fileUpload.FileName;
            Session["FilteredImages"] = filteredImages;

            return filteredImages;
        }

        private void SaveImage(string fileName, Bitmap img)
        {
            img.Save(string.Format(@"C:\Users\Dimitar\Pictures\FilterIT\{0}\{2}_{1}.png",
                User.Identity.Name,
                Path.GetFileNameWithoutExtension(fileName),
                DateTime.Now.Ticks));
        }

        [HttpPost]
        public object CheckSession()
        {
            if (Session["Image"] != null && Session["filteredImages"] != null)
            {
                var img = Session["Image"] as Bitmap;
                int height = (int)(img.Height / ((float)img.Width / 500f));
                return img.Resize(500, height).ToImageString() + Session["filteredImages"];
            }
            return null;
        }
    }
}
