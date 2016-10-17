using System;

namespace Gitria.Core.Models
{
    public class Commit
    {
        public string Id { get; set; }
        public string RepositoryName { get; set; }
        public DateTime Date { get; set; }
        public string TimeAgo { get; set; }
        public string Url { get; set; }
        public Author Author { get; set; }
        public string Message { get; set; }
    }
}