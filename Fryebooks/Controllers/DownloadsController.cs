using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fryebooks.Controllers
{
    [Authorize(Roles = "Download")]
    public class DownloadsController : Controller
    {
        // GET: Downloads
        public ActionResult Index()
        {
            return View();
        }
    }
}