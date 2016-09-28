using Gitria.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Gitria.Core.GitCommunications
{
    public class GetRepositories
    {
        public List<GitRepository> GetAllRepositories()
        {
            var authFileContents = File.ReadAllLines((AppDomain.CurrentDomain.BaseDirectory + @"Auth\AuthKey.txt"));
            var authKey = string.Join("", authFileContents);

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
    }
}
