using Gitria.Api.GitModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Gitria.Core.Models;

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
            try
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

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return JsonConvert.DeserializeObject<List<GitRepository>>(streamReader.ReadToEnd());
                }
            }
            catch (Exception)
            {
                return new List<GitRepository>();
            }
        }

        public static List<GitCommit> GetAllCommitsForRepository(Repository repository)
        {
            try
            {
                var authKey = GetAuthKey();

                var getRepositoriesRequest = (HttpWebRequest)WebRequest.Create($"https://api.github.com/repos/{repository.Owner.login}/{repository.Name}/commits");
                getRepositoriesRequest.ContentType = "application/json";
                getRepositoriesRequest.Accept = "*/*";
                getRepositoriesRequest.Method = "GET";
                getRepositoriesRequest.UserAgent = "Flave229";
                getRepositoriesRequest.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(authKey)));

                var httpResponse = (HttpWebResponse)getRepositoriesRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return JsonConvert.DeserializeObject<List<GitCommit>>(streamReader.ReadToEnd());
                }
            }
            catch (Exception)
            {
                return new List<GitCommit>();
            }
        }

        public static GitRepositoryStatistics GetAdditionsAndDeletionsForRepository(Repository repository)
        {
            try
            {
                var authKey = GetAuthKey();

                var getRepositoriesRequest = (HttpWebRequest)WebRequest.Create($"https://api.github.com/repos/{repository.Owner.login}/{repository.Name}/stats/contributors");
                getRepositoriesRequest.ContentType = "application/json";
                getRepositoriesRequest.Accept = "*/*";
                getRepositoriesRequest.Method = "GET";
                getRepositoriesRequest.UserAgent = "Flave229";
                getRepositoriesRequest.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(authKey)));

                var httpResponse = (HttpWebResponse)getRepositoriesRequest.GetResponse();

                List<GitRepositoryStatistics> repositoryStatistics;

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    repositoryStatistics = JsonConvert.DeserializeObject<List<GitRepositoryStatistics>>(streamReader.ReadToEnd());
                }

                return repositoryStatistics.ElementAt(0);
            }
            catch (Exception)
            {
                return new GitRepositoryStatistics();
            }
        }
    }
}
