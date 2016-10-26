using System;
using System.Collections.Generic;
using System.Linq;
using Gitria.Api.GitModels;
using Gitria.Core.Models;

namespace Gitria.Core.Mappers
{
    public static class RepositoryMapper
    {
        public static Repository MapFrom(GitRepository gitRepository)
        {
            if (gitRepository.HasError)
            {
                var repository = new Repository();
                repository.AddError(gitRepository.Error);

                return repository;
            }

            return new Repository
            {
                Name = gitRepository.name,
                CommitsUrl = gitRepository.commits_url,
                Owner = gitRepository.owner,
                UpdatedAt = gitRepository.updated_at,
                Url = gitRepository.url,
                IssueCount = gitRepository.open_issues_count
            };
        }

        public static List<Repository> MapFrom(List<GitRepository> gitRepositories)
        {
            var repositoryList = new List<Repository>();

            foreach (var gitRepository in gitRepositories)
            {
                repositoryList.Add(MapFrom(gitRepository));
            }

            return repositoryList;
        }

        public static Repository MapInto(Repository repository, GitRepositoryStatistics repositoryStatistics)
        {
            try
            {
                if (repositoryStatistics.HasError)
                    return repository;

                foreach (var week in repositoryStatistics.weeks)
                {
                    var date = DateTimeOffset.FromUnixTimeSeconds(week.w).UtcDateTime;

                    if (date >= DateTime.Today.AddDays(-21))
                    {
                        repository.WeeklyRepositoryStatistics.Add(new RepositoryStatistics
                        {
                            Week = DateTimeOffset.FromUnixTimeSeconds(week.w).UtcDateTime,
                            Additions = week.a,
                            Deletions = week.d
                        });
                    }
                }

                repository.WeeklyRepositoryStatistics = repository.WeeklyRepositoryStatistics.OrderByDescending(statistics => statistics.Week).ToList();

                return repository;
            }
            catch (Exception)
            {
                return repository;
            }
        }

        public static Repository MapInto(Repository repository, List<GitRepositoryStatistics> repositoryStatistics)
        {
            try
            {
                foreach (var contributer in repositoryStatistics)
                {
                    foreach (var week in contributer.weeks)
                    {
                        var date = DateTimeOffset.FromUnixTimeSeconds(week.w).UtcDateTime;

                        if (date <= DateTime.Today.AddDays(-21))
                            continue;

                        if (repository.WeeklyRepositoryStatistics.Any(x => x.Week == DateTimeOffset.FromUnixTimeSeconds(week.w).UtcDateTime) == false)
                        {
                            repository.WeeklyRepositoryStatistics.Add(new RepositoryStatistics
                            {
                                Week = DateTimeOffset.FromUnixTimeSeconds(week.w).UtcDateTime,
                                Additions = week.a,
                                Deletions = week.d
                            });

                            continue;
                        }

                        var existingWeekStatistics = repository.WeeklyRepositoryStatistics.Where(x => x.Week.Equals(DateTimeOffset.FromUnixTimeSeconds(week.w).UtcDateTime)).ToList();

                        existingWeekStatistics.FirstOrDefault().Additions += week.a;
                        existingWeekStatistics.FirstOrDefault().Deletions += week.d;
                    }
                }

                repository.WeeklyRepositoryStatistics = repository.WeeklyRepositoryStatistics.OrderByDescending(statistics => statistics.Week).ToList();

                return repository;
            }
            catch (Exception)
            {
                return repository;
            }
        }
    }
}
