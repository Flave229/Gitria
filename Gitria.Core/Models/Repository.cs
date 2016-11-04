using System;
using System.Collections.Generic;
using Gitria.Api.GitModels;

namespace Gitria.Core.Models
{
    public class Repository : BaseModel
    {
        public string Name { get; set; }
        public string Initials { get; set; }
        public string Url { get; set; }
        public string CommitsUrl { get; set; }
        public DateTime UpdatedAt { get; set; }
        public GitAuthor Owner { get; set; }
        public List<RepositoryStatistics> WeeklyRepositoryStatistics { get; set; }
        public List<RepositoryCommitStatistics> SixMonthCommitData { get; set; }
        public int IssueCount { get; set; }

        public Repository()
        {
            // The list needs to be populated with 2 elements by default, as assumptions are made that data will be populated for the first two weeks
            WeeklyRepositoryStatistics = new List<RepositoryStatistics>
            {
                new RepositoryStatistics(),
                new RepositoryStatistics()
            };

            SixMonthCommitData = new List<RepositoryCommitStatistics>();
        }
    }
}