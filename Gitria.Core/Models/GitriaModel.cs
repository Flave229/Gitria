using System.Collections.Generic;

namespace Gitria.Core.Models
{
    public class GitriaModel
    {
        public List<Repository> Repositories { get; set; }
        public int ActiveRepositories { get; set; }
        public string LastUpdate { get; set; }
        public List<Commit> CommitsThisWeek { get; set; }
        public int CommitsThisWeekCount { get; set; }
        public int CommitsThisMonthCount { get; set; }
    }
}
