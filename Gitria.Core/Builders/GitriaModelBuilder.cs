using Gitria.Api.Models;
using Gitria.Core.GitCommunications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gitria.Core
{
    public class GitriaModelBuilder
    {
        public GitriaModel BuildGitriaModel()
        {
            var allRepositories = GitRepositoryConnection.GetAllRepositories();

            return new GitriaModel
            {
                ActiveRepositories = GetActiveRepositories(allRepositories).Count()
            };
        }

        public List<GitRepository> GetActiveRepositories(List<GitRepository> repositories)
        {
            return repositories.Where(repository => repository.updated_at > DateTime.Now.AddMonths(-3)).ToList();
        }
    }
}
