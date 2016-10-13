using System.Text.RegularExpressions;

namespace Gitria.Core.Services
{
    public static class InitialExtractor
    {
        public static string Extract(string name)
        {
            var splitWords = Regex.Split(name, @"(?<!^)(?=[A-Z])");

            switch (splitWords.Length)
            {
                case 1:
                    return splitWords[0].Substring(0, 1).ToUpper() + splitWords[0].Substring(1, 1).ToUpper();
                case 2:
                    return splitWords[0].Substring(0, 1) + splitWords[1].Substring(0, 1);
                case 3:
                    return splitWords[0].Substring(0, 1) + splitWords[1].Substring(0, 1) + splitWords[2].Substring(0, 1);
                default:
                    return splitWords[0].Substring(0, 1) + splitWords[1].Substring(0, 1) + splitWords[2].Substring(0, 1);
            }
        }
    }
}