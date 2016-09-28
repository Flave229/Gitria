using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gitria.Api.Models
{
    public class GitRepository
    {
        public string name { get; set; }
        public string url { get; set; }
        public string commits_url { get; set; }
        public DateTime updated_at { get; set; }
    }
}
