using Gitria.Api.GitModels;
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
            var activeRepositories = GetActiveRepositories(allRepositories);
            var activeCommits = GetCommitsForRepositories(activeRepositories);

            return new GitriaModel
            {
                ActiveRepositories = activeRepositories.Count(),
                CommitsThisWeek = FilterCommitsByTime(activeCommits, DateTime.Today.AddDays(-7), DateTime.Today).Count,
                CommitsThisMonth = FilterCommitsByTime(activeCommits, DateTime.Today.AddMonths(-1), DateTime.Today).Count
            };
        }

        public List<GitRepository> GetActiveRepositories(List<GitRepository> repositories)
        {
            return repositories.Where(repository => repository.updated_at > DateTime.Now.AddMonths(-3)).ToList();
        }


        public List<GitCommit> GetCommitsForRepositories(List<GitRepository> repositories)
        {
            var commits = new List<GitCommit>();

            foreach(var repository in repositories)
            {
                var commitsForRepository = GitRepositoryConnection.GetAllCommitsForRepository(repository);

                foreach (var commit in commitsForRepository)
                {
                    commit.repo_name = repository.name;
                }

                commits.AddRange(commitsForRepository);
            }

            return commits;
        }

        private List<GitCommit> FilterCommitsByTime(List<GitCommit> commits, DateTime from, DateTime to)
        {
            return commits.Where(commit => commit.commit.author.date > from && commit.commit.author.date < to).ToList();
        }
    }
}
