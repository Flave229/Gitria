using System;

namespace Gitria.Core.Models
{
    public class RepositoryCommitStatistics : BaseModel
    {
        public DateTime MonthStart { get; set; }
        public int CommitCount { get; set; }
    }
}
