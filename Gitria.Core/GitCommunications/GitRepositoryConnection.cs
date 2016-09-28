using Gitria.Api.GitModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Gitria.Core.GitCommunications
{
    public static class GitRepositoryConnection
    {
        private static string GetAuthKey()
        {
            var authFileContents = File.ReadAllLines((AppDomain.CurrentDomain.BaseDirectory + @"Auth\AuthKey.txt"));
            return string.Join("", authFileContents);
        }

        public static List<GitRepository> GetAllRepositories()
        {
            var authKey = GetAuthKey();

            var getRepositoriesRequest = (HttpWebRequest)WebRequest.Create("https://api.github.com/user/repos");
            getRepositoriesRequest.ContentType = "application/json";
            getRepositoriesRequest.Accept = "*/*";
            getRepositoriesRequest.Method = "GET";
            getRepositoriesRequest.UserAgent = "Flave229";
            getRepositoriesRequest.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(authKey)));
            getRepositoriesRequest.Headers.Add("type", "all");

            var httpResponse = (HttpWebResponse)getRepositoriesRequest.GetResponse();

            var repositories = new List<GitRepository>();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                repositories = JsonConvert.DeserializeObject<List<GitRepository>>(streamReader.ReadToEnd());
            }

            return repositories;
        }

        public static List<GitCommit> GetAllCommitsForRepository(GitRepository repository)
        {
            var authKey = GetAuthKey();

            var getRepositoriesRequest = (HttpWebRequest)WebRequest.Create($"https://api.github.com/repos/{repository.owner.login}/{repository.name}/commits");
            getRepositoriesRequest.ContentType = "application/json";
            getRepositoriesRequest.Accept = "*/*";
            getRepositoriesRequest.Method = "GET";
            getRepositoriesRequest.UserAgent = "Flave229";
            getRepositoriesRequest.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(authKey)));

            var httpResponse = (HttpWebResponse)getRepositoriesRequest.GetResponse();

            var commit = new List<GitCommit>();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                commit = JsonConvert.DeserializeObject<List<GitCommit>>(streamReader.ReadToEnd());
            }

            return commit;
        }
    }
}
