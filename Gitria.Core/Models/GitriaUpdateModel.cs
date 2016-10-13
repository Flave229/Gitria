using System.Collections.Generic;

namespace Gitria.Core.Models
{
    public class GitriaUpdateModel
    {
        public List<Commit> NewCommits { get; set; }
        public int Count { get; set; }
    }
}
