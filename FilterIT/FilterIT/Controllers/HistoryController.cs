using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filtering;
using System.IO;
using Filtering;

namespace FilterIT.Controllers
{
    public class HistoryController : Controller
    {
        // GET: /History/

        public ActionResult History()
        {
            return View();
        }

        [HttpPost]
        public string GetHistory()
        {
            var history = string.Empty;
            var filePaths = Directory.GetFiles(string.Format(@"C:\Users\Dimitar\Pictures\FilterIT\{0}\", User.Identity.Name), "*.png");
            filePaths.ToList().ForEach(filePath => history += new Bitmap(filePath).ToImageString());
            return history;
        }

        [HttpPost]
        public void ChooseImage(int id)
        {
            var filePaths = Directory.GetFiles(string.Format(@"C:\Users\Dimitar\Pictures\FilterIT\{0}\", User.Identity.Name), "*.png");
            var img = new Bitmap(filePaths[id]);

            int height = (int)(img.Height / ((float)img.Width / 500f));
            var filteredImages = img.Resize(500, height).ApplyFilters();

            Session["Image"] = img;
            Session["ImageName"] = Path.GetFileName(filePaths[id]);
            Session["FilteredImages"] = filteredImages;
        }

    }
}
