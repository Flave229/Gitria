using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Gitria.Core.GitCommunications
{
    public class GetCommits
    {
        public int GetAllRepositories()
        {
            var authFileContents = File.ReadAllLines((AppDomain.CurrentDomain.BaseDirectory + @"..\Auth\AuthKey.txt"));
            var authKey = string.Join("", authFileContents);

            var getRepositoriesRequest = (HttpWebRequest)WebRequest.Create("https://api.github.com/user/repos");
            getRepositoriesRequest.ContentType = "application/json";
            getRepositoriesRequest.Accept = "*/*";
            getRepositoriesRequest.Method = "GET";
            getRepositoriesRequest.UserAgent = "Flave229";
            getRepositoriesRequest.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(authKey)));
            getRepositoriesRequest.Headers.Add("type", "all");

            var httpResponse = (HttpWebResponse)getRepositoriesRequest.GetResponse();

            var json = new object();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                json = JsonConvert.DeserializeObject(streamReader.ReadToEnd());
            }

            return 0;
        }
    }
}
