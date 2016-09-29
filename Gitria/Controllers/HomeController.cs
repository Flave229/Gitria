using Gitria.Core;
using System;
using System.Web.Mvc;

namespace Gitria.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new GitriaModelBuilder().BuildGitriaModel();

            return View(model);
        }

        [HttpPost]
        [Route("Home/CheckForUpdates/{lastUpdated}")]
        public string CheckForUpdates(string lastUpdated)
        {
            return "You hit me!";
        }
    }
}