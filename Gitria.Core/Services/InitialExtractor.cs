using System.Linq;
using System.Text.RegularExpressions;

namespace Gitria.Core.Services
{
    public static class InitialExtractor
    {
        public static string Extract(string name)
        {
            var splitWords = Regex.Split(name, @"(?<!^)(?=[A-Z0-9])");

            switch (splitWords.Length)
            {
                case 1:
                    return splitWords[0].Substring(0, 1).ToUpper() + splitWords[0].Substring(1, 1).ToUpper();
                case 2:
                    return splitWords[0].Substring(0, 1) + splitWords[1].Substring(0, 1);
                case 3:
                    return splitWords[0].Substring(0, 1) + splitWords[1].Substring(0, 1) + splitWords[2].Substring(0, 1);
                default:
                    var number = 0;

                    if (int.TryParse(splitWords[0], out number) || int.TryParse(splitWords[0], out number))
                    {
                        return splitWords[0].Substring(0, 1) + splitWords[1].Substring(0, 1) + splitWords[2].Substring(0, 1);
                    }

                    if (splitWords.Any(word => int.TryParse(word, out number)))
                    {
                        return splitWords[0].Substring(0, 1) + splitWords[1].Substring(0, 1) + number;
                    }

                    return splitWords[0].Substring(0, 1) + splitWords[1].Substring(0, 1) + splitWords[2].Substring(0, 1);
            }
        }
    }
}