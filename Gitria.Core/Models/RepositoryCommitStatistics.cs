using System;

namespace Gitria.Core.Models
{
    public class RepositoryCommitStatistics : BaseModel
    {
        public string Month { get; set; }
        public int CommitCount { get; set; }
    }
}
