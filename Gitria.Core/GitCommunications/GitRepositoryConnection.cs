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

                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    var errorStatistics = new GitRepository();
                    errorStatistics.AddError("Github API call failed when fetching repositories.");

                    return new List<GitRepository> { errorStatistics };
                }

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return JsonConvert.DeserializeObject<List<GitRepository>>(streamReader.ReadToEnd());
                }
            }
            catch (Exception)
            {
                var errorStatistics = new GitRepository();
                errorStatistics.AddError("An exception was thrown while fetching repositories.");

                return new List<GitRepository> { errorStatistics };
            }
        }

        public static List<GitCommit> GetAllCommitsForRepository(Repository repository)
        {
            try
            {
                var authKey = GetAuthKey();

                var getRepositoriesRequest = (HttpWebRequest)WebRequest.Create($"https://api.github.com/repos/{repository.Owner.login}/{repository.Name}/commits?per_page=1000");
                getRepositoriesRequest.ContentType = "application/json";
                getRepositoriesRequest.Accept = "*/*";
                getRepositoriesRequest.Method = "GET";
                getRepositoriesRequest.UserAgent = "Flave229";
                getRepositoriesRequest.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(authKey)));

                var httpResponse = (HttpWebResponse)getRepositoriesRequest.GetResponse();
                
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    var errorCommit = new GitCommit();
                    errorCommit.AddError($"Github API call failed when fetching commits for repository {repository.Name}.");

                    return new List<GitCommit> { errorCommit };
                }

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return JsonConvert.DeserializeObject<List<GitCommit>>(streamReader.ReadToEnd());
                }
            }
            catch (Exception)
            {
                var errorCommit = new GitCommit();
                errorCommit.AddError($"An exception was thrown while fetching commits for repository {repository.Name}.");

                return new List<GitCommit> { errorCommit };
            }
        }

        public static List<GitRepositoryStatistics> GetAdditionsAndDeletionsForRepository(Repository repository)
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

                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    var errorStatistics = new GitRepositoryStatistics();
                    errorStatistics.AddError($"Github API call failed when fetching repository statistics for {repository.Name}.");

                    return new List<GitRepositoryStatistics> { errorStatistics };
                }

                List<GitRepositoryStatistics> repositoryStatistics;

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    repositoryStatistics = JsonConvert.DeserializeObject<List<GitRepositoryStatistics>>(streamReader.ReadToEnd());
                }

                return repositoryStatistics;
            }
            catch (Exception exception)
            {
                var errorStatistics = new GitRepositoryStatistics();
                errorStatistics.AddError($"An exception was thrown while fetching repository statistics for {repository.Name}. {exception.Message}.");

                return new List<GitRepositoryStatistics> { errorStatistics };
            }
        }
    }
}
