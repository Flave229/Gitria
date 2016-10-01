using Gitria.Core;
using Gitria.Core.GitCommunications;
using Newtonsoft.Json;
using System;
using System.Linq;
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

        [HttpGet]
        public string CheckForUpdates(string lastUpdated)
        {
            var dateTimeValues = lastUpdated.Split('-');
            var updated = new DateTime(int.Parse(dateTimeValues[0]), int.Parse(dateTimeValues[1]), int.Parse(dateTimeValues[2]), int.Parse(dateTimeValues[3]), int.Parse(dateTimeValues[4]), int.Parse(dateTimeValues[5]));

            var builder = new GitriaModelBuilder();

            var activeRepositories = builder.GetActiveRepositories();
            var commits = builder.GetCommitsForRepositories(activeRepositories);

            var newCommits = commits.Where(commit => commit.commit.author.date > updated).ToList();
            
            return JsonConvert.SerializeObject(newCommits);
        }
    }
}