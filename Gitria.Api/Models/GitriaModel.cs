using Gitria.Api.GitModels;
using System.Collections.Generic;

namespace Gitria.Api.Models
{
    public class GitriaModel
    {
        public int ActiveRepositories { get; set; }
        public List<GitCommit> CommitsThisWeek { get; set; }
        public int CommitsThisWeekCount { get; set; }
        public int CommitsThisMonthCount { get; set; }
    }
}
