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
            return new Commit
            {
                Author = new CommitAuthor
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