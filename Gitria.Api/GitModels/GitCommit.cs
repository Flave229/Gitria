﻿using System;

namespace Gitria.Api.GitModels
{
    public class GitCommit
    {
        public string repo_name { get; set; }
        public string time_ago { get; set; }
        public CommitInfo commit { get; set; }
        public GitAuthor author { get; set; }
    }

    public class CommitInfo
    {
        public string url { get; set; }
        public CommitInfoAuthor author { get; set; }
        public string message { get; set; }
    }

    public class CommitInfoAuthor
    {
        public string name { get; set; }
        public DateTime date { get; set; }
    }

}
