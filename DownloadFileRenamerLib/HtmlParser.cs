namespace DoenaSoft.DownloadRenamer
{
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class HtmlParser
    {
        private static readonly Regex _urlRegex;

        private static readonly Regex _nameRegex;

        static HtmlParser()
        {
            _urlRegex = new Regex("<a (.*?)href=\"(?'Link'.+?)\"(.*?)>(?'Name'.+?)</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

            _nameRegex = new Regex("(<.+?>)?(?'Name'.+)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        }

        public static void Parse(EpisodeModel model, string html)
        {
            var match = _urlRegex.Match(html);

            if (match.Success)
            {
                var name = match.Groups["Name"].Value;

                var nameMatch = _nameRegex.Match(name);

                if (nameMatch.Success)
                {
                    name = nameMatch.Groups["Name"].Value;
                }

                var decoded = System.Web.HttpUtility.HtmlDecode(name);

                model.EpisodeName = decoded.Replace("\r\n", string.Empty).Replace("\n", string.Empty).Trim();

                var link = match.Groups["Link"].Value;

                model.TvdbId = link.Split('/').Last();

                FileNameBuilder.Build(model, false);
            }
        }
    }
}