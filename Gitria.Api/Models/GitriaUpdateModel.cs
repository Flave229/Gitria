using System.Collections.Generic;
using Gitria.Api.GitModels;

namespace Gitria.Api.Models
{
    public class GitriaUpdateModel
    {
        public List<GitCommit> NewCommits { get; set; }
        public int Count { get; set; }
    }
}
