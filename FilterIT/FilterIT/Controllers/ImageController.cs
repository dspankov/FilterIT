using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilterIT;

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
            Session["Image"] = img;
            Session["ImageName"] = fileUpload.FileName;
            int height = (int)(img.Height / ((float)img.Width / 500f));
            img = ResizeBitmap(img, 500, height);
            return img.ApplyFilters();
        }

        private Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }
    }
}
