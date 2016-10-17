using System;

namespace Gitria.Api.GitModels
{
    public class GitRepository
    {
        public string name { get; set; }
        public string url { get; set; }
        public string commits_url { get; set; }
        public DateTime updated_at { get; set; }
        public GitAuthor owner { get; set; }
        public int open_issues_count { get; set; }
    }
}
