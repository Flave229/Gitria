using System;
using Gitria.Api.GitModels;

namespace Gitria.Core.Models
{
    public class Repository
    {
        public string Name { get; set; }
        public string Initials { get; set; }
        public string Url { get; set; }
        public string CommitsUrl { get; set; }
        public DateTime UpdatedAt { get; set; }
        public GitAuthor Owner { get; set; }
    }
}