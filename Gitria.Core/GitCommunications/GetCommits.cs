using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace Gitria.Core.GitCommunications
{
    public class GetCommits
    {
        public int GetAllRepositories()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.github.com/users/Flave229/repos");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "*/*";
            httpWebRequest.Method = "GET";
            httpWebRequest.UserAgent = "Gitria";
            httpWebRequest.Headers.Add("type", "all");

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            var json = new object();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                json = JsonConvert.DeserializeObject(streamReader.ReadToEnd());
            }

            return 0;
        }
    }
}
