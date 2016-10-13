using System;

namespace Gitria.Api.GitModels
{
    public class GitCommit
    {
        public string html_url { get; set; }
        public GitCommitInfo commit { get; set; }
        public GitAuthor committer { get; set; }
    }

    public class GitCommitInfo
    {
        public GitCommitInfoAuthor author { get; set; }
        public string message { get; set; }
    }

    public class GitCommitInfoAuthor
    {
        public string name { get; set; }
        public DateTime date { get; set; }
    }

}
