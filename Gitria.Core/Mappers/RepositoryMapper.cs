using System.Collections.Generic;
using Gitria.Api.GitModels;
using Gitria.Core.Models;

namespace Gitria.Core.Mappers
{
    public static class RepositoryMapper
    {
        public static Repository MapFrom(GitRepository gitRepository)
        {
            return new Repository
            {
                Name = gitRepository.name,
                CommitsUrl = gitRepository.commits_url,
                Owner = gitRepository.owner,
                UpdatedAt = gitRepository.updated_at,
                Url = gitRepository.url
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
    }
}
