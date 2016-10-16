﻿using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;
using Gitria.Core.Builders;
using Gitria.Core.Models;
using Newtonsoft.Json.Serialization;
using JsonSerializer = Microsoft.ApplicationInsights.Extensibility.Implementation.JsonSerializer;

namespace Gitria.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new GitriaModelBuilder().BuildGitriaModel();

            return View(model);
        }

        public ActionResult GetApplicationStatisticsPartial(string repository)
        {
            return View("ApplicationStatistics", JsonConvert.DeserializeObject<Repository>(repository));
        }

        [HttpGet]
        public string CheckForUpdates(string lastUpdated)
        {
            var dateTimeValues = lastUpdated.Split('-');
            var updated = new DateTime(int.Parse(dateTimeValues[0]), int.Parse(dateTimeValues[1]), int.Parse(dateTimeValues[2]), int.Parse(dateTimeValues[3]), int.Parse(dateTimeValues[4]), int.Parse(dateTimeValues[5]));

            var builder = new GitriaModelBuilder();

            var activeRepositories = builder.GetActiveRepositories();
            var commits = builder.GetCommitsForRepositories(activeRepositories);

            var newCommits = commits.Where(commit => commit.Date > updated).ToList();
            if (newCommits.Count <= 0) return "{}";

            var gitriaUpdate = new GitriaUpdateModel
            {
                Count = newCommits.Count,
                NewCommits = newCommits
            };

            return JsonConvert.SerializeObject(gitriaUpdate);
        }
    }
}