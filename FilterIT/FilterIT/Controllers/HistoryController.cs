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
            GetHistory();
            return View();
        }

        private void GetHistory()
        {
            var history = new List<string>();
            var filePaths = Directory.GetFiles(string.Format(@"C:\Users\Dimitar\Pictures\FilterIT\{0}\", User.Identity.Name), "*.png");
            filePaths.ToList().ForEach(filePath => history.Add(new Bitmap(filePath).ToImageString()));
            ViewBag.History = history;
        }

       

    }
}
