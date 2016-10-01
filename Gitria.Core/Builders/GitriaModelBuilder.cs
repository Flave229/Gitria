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
            var activeRepositories = GetActiveRepositories();
            var activeCommits = GetCommitsForRepositories(activeRepositories);
            var commitsThisWeek = FilterCommitsByTime(activeCommits, DateTime.Today.AddDays(-7), DateTime.Today.AddDays(1));
            var lastUpdated = commitsThisWeek.Max(commit => commit.commit.author.date);

            return new GitriaModel
            {
                ActiveRepositories = activeRepositories.Count(),
                LastUpdate = lastUpdated.ToString("yyyy-MM-dd-HH-mm-ss"),
                CommitsThisWeek = commitsThisWeek,
                CommitsThisWeekCount = commitsThisWeek.Count(),
                CommitsThisMonthCount = FilterCommitsByTime(activeCommits, DateTime.Today.AddMonths(-1), DateTime.Today.AddDays(1)).Count()
            };
        }

        public List<GitRepository> GetActiveRepositories()
        {
            var allRepositories = GitRepositoryConnection.GetAllRepositories();
            return GetActiveRepositories(allRepositories);
        }

        private List<GitRepository> GetActiveRepositories(List<GitRepository> repositories)
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
                    var timeAgo = (DateTime.Now - commit.commit.author.date);

                    if ((int)timeAgo.TotalSeconds <= 1)
                    {
                        commit.time_ago = "1 second ago";
                    }
                    else if ((int)timeAgo.TotalSeconds < 60)
                    {
                        commit.time_ago = (int)timeAgo.TotalSeconds + " seconds ago";
                    }
                    else if ((int)timeAgo.TotalMinutes == 1)
                    {
                        commit.time_ago = "1 minute ago";
                    }
                    else if ((int)timeAgo.TotalMinutes < 60)
                    {
                        commit.time_ago = (int)timeAgo.TotalMinutes + " minutes ago";
                    }
                    else if ((int)timeAgo.TotalHours == 1)
                    {
                        commit.time_ago = "1 hour ago";
                    }
                    else if ((int)timeAgo.TotalHours < 24)
                    {
                        commit.time_ago = (int)timeAgo.TotalHours + " hours ago";
                    }
                    else if ((int)timeAgo.TotalDays == 1)
                    {
                        commit.time_ago = "1 day ago";
                    }
                    else if ((int)timeAgo.TotalDays < 365)
                    {
                        commit.time_ago = (int)timeAgo.TotalDays + " days ago";
                    }
                    else if ((int)timeAgo.TotalDays >= 365)
                    {
                        int years;
                        var yearsCalc = Math.DivRem((int)timeAgo.TotalDays, 365, out years);
                        commit.time_ago = years + " years ago";
                    }
                    
                    commit.repo_name = repository.name;
                }

                commits.AddRange(commitsForRepository);
            }

            return commits.OrderByDescending(commit => commit.commit.author.date).ToList();
        }

        private List<GitCommit> FilterCommitsByTime(List<GitCommit> commits, DateTime from, DateTime to)
        {
            return commits.Where(commit => commit.commit.author.date > from && commit.commit.author.date < to).ToList();
        }
    }
}
