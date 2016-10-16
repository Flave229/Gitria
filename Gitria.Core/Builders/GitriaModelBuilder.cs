using System;
using System.Collections.Generic;
using System.Linq;
using Gitria.Core.GitCommunications;
using Gitria.Core.Mappers;
using Gitria.Core.Models;
using Gitria.Core.Services;

namespace Gitria.Core.Builders
{
    public class GitriaModelBuilder
    {
        public GitriaModel BuildGitriaModel()
        {
            var allRepositories = RepositoryMapper.MapFrom(GitRepositoryConnection.GetAllRepositories());
            var activeRepositories = GetActiveRepositories(allRepositories);
            var activeCommits = GetCommitsForRepositories(activeRepositories);
            var commitsThisWeek = FilterCommitsByTime(activeCommits, DateTime.Today.AddDays(-7), DateTime.Today.AddDays(1));
            var lastUpdated = commitsThisWeek.Max(commit => commit.Date);

            return new GitriaModel
            {
                Repositories = allRepositories,
                ActiveRepositories = activeRepositories.Count,
                LastUpdate = lastUpdated.ToString("yyyy-MM-dd-HH-mm-ss"),
                CommitsThisWeek = commitsThisWeek,
                CommitsThisWeekCount = commitsThisWeek.Count,
                CommitsThisMonthCount = FilterCommitsByTime(activeCommits, DateTime.Today.AddMonths(-1), DateTime.Today.AddDays(1)).Count
            };
        }

        public List<Repository> GetRepositoryStatistics(List<Repository> repositories)
        {
            for(var i = 0; i < repositories.Count; i++)
            {
                repositories[i] = RepositoryMapper.MapInto(repositories[i], GitRepositoryConnection.GetAdditionsAndDeletionsForRepository(repositories[i]));
            }

            return repositories;
        }

        public List<Repository> GetActiveRepositories()
        {
            var allRepositories = RepositoryMapper.MapFrom(GitRepositoryConnection.GetAllRepositories());

            return GetActiveRepositories(allRepositories);
        }

        private List<Repository> GetActiveRepositories(List<Repository> repositories)
        {
            foreach (var repository in repositories)
            {
                repository.Initials = InitialExtractor.Extract(repository.Name);
            }

            return repositories.Where(repository => repository.UpdatedAt > DateTime.Now.AddMonths(-3)).ToList();
        }
        
        public List<Commit> GetCommitsForRepositories(List<Repository> repositories)
        {
            var commits = new List<Commit>();

            foreach(var repository in repositories)
            {
                var commitsForRepository = CommitMapper.MapFrom(GitRepositoryConnection.GetAllCommitsForRepository(repository)).OrderByDescending(commit => commit.Date).ToList();

                if (commitsForRepository.Count != 0 && repository.UpdatedAt < commitsForRepository.ElementAt(0).Date)
                {
                    repository.UpdatedAt = commitsForRepository.ElementAt(0).Date;
                }

                foreach (var commit in commitsForRepository)
                {
                    var timeAgo = (DateTime.Now - commit.Date);

                    if ((int)timeAgo.TotalSeconds <= 1)
                    {
                        commit.TimeAgo = "1 second ago";
                    }
                    else if ((int)timeAgo.TotalSeconds < 60)
                    {
                        commit.TimeAgo = (int)timeAgo.TotalSeconds + " seconds ago";
                    }
                    else if ((int)timeAgo.TotalMinutes == 1)
                    {
                        commit.TimeAgo = "1 minute ago";
                    }
                    else if ((int)timeAgo.TotalMinutes < 60)
                    {
                        commit.TimeAgo = (int)timeAgo.TotalMinutes + " minutes ago";
                    }
                    else if ((int)timeAgo.TotalHours == 1)
                    {
                        commit.TimeAgo = "1 hour ago";
                    }
                    else if ((int)timeAgo.TotalHours < 24)
                    {
                        commit.TimeAgo = (int)timeAgo.TotalHours + " hours ago";
                    }
                    else if ((int)timeAgo.TotalDays == 1)
                    {
                        commit.TimeAgo = "1 day ago";
                    }
                    else if ((int)timeAgo.TotalDays < 365)
                    {
                        commit.TimeAgo = (int)timeAgo.TotalDays + " days ago";
                    }
                    else if ((int)timeAgo.TotalDays >= 365)
                    {
                        int years;
                        var yearsCalc = Math.DivRem((int)timeAgo.TotalDays, 365, out years);
                        commit.TimeAgo = years + " years ago";
                    }
                    
                    commit.RepositoryName = repository.Name;
                }

                commits.AddRange(commitsForRepository);
            }

            return commits.OrderByDescending(commit => commit.Date).ToList();
        }

        private List<Commit> FilterCommitsByTime(List<Commit> commits, DateTime from, DateTime to)
        {
            return commits.Where(commit => commit.Date > from && commit.Date < to).ToList();
        }
    }
}
