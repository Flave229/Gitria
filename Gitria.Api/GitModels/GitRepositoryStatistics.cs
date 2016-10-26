using System.Collections.Generic;

namespace Gitria.Api.GitModels
{
    public class GitRepositoryStatistics : GitResponse
    {
        public List<GitRepositoryStatisticWeek> weeks { get; set; }
    }

    public class GitRepositoryStatisticWeek
    {
        public long w { get; set; }
        public int a { get; set; }
        public int d { get; set; }
    }
}
