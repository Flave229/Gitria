using System.Collections.Generic;
using System.Linq;
using Gitria.Api.GitModels;
using Gitria.Core.Models;

namespace Gitria.Core.Mappers
{
    public static class CommitMapper
    {
        public static Commit MapFrom(GitCommit gitCommit)
        {
            if (gitCommit.HasError)
            {
                var commit = new Commit();
                commit.AddError(gitCommit.Error);

                return commit;
            }

            return new Commit
            {
                Id = gitCommit.sha,
                Author = new Author
                {
                    Name = gitCommit.committer?.login,
                    AvatarLink = gitCommit.committer?.avatar_url,
                    Url = gitCommit.committer?.url
                },
                Date = gitCommit.commit.author.date,
                Message = gitCommit.commit.message,
                Url = gitCommit.html_url
            };
        }

        public static List<Commit> MapFrom(List<GitCommit> gitCommits)
        {
            return gitCommits.Select(MapFrom).ToList();
        }
    }
}