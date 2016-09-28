using System.Web.Mvc;

namespace Gitria.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var core = new Core.GitCommunications.GetCommits();

            var test = core.GetAllRepositories();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}