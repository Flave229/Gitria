namespace Gitria.Api.GitModels
{
    public class GitAuthor : GitResponse
    {
        public string login { get; set; }
        public string avatar_url { get; set; }
        public string url { get; set; }
    }
}
