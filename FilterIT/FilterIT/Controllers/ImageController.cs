using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilterIT;
using Filtering;

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
            int height = (int)(img.Height / ((float)img.Width / 500f));
            var filteredImages = img.Resize(500, height).ApplyFilters();
            Session["Image"] = img;
            Session["ImageName"] = fileUpload.FileName;
            Session["filteredImages"] = filteredImages;
            return filteredImages;
        }

        [HttpPost]
        public object CheckSession()
        {
            if (Session["Image"] != null && Session["filteredImages"] != null)
            {
                var img = Session["Image"] as Bitmap;
                int height = (int)(img.Height / ((float)img.Width / 500f));
                return img.Resize(500, height).ToImageString() + " " + Session["filteredImages"];
            }
            return null;
        }
    }
}
