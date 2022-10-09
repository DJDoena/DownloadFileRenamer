namespace DoenaSoft.MassDownloadFileRenamer
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    internal static class TitleReader
    {
        private static readonly Regex _tvdbRegex = new Regex("S(?'Season'[0-1][0-9])E(?'Episode'[0-3][0-9])", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static bool GetEpisodeTitles(string episodeNameSourceFile, bool useTvdb, out Dictionary<string, Dictionary<string, string>> episodeTitles)
        {
            var hasErrors = false;

            episodeTitles = new Dictionary<string, Dictionary<string, string>>();

            using (var sr = new StreamReader(episodeNameSourceFile, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();

                    var split = line.Split(';');

                    hasErrors |= ProcessLine(split, useTvdb, episodeTitles);
                }
            }

            return !hasErrors;
        }

        private static bool ProcessLine(string[] lineParts, bool useTvdb, Dictionary<string, Dictionary<string, string>> episodeTitles)
        {
            string season;
            string episode;
            string title;
            if (useTvdb)
            {
                #region TVDB

                if (lineParts.Length < 2)
                {
                    return false;
                }

                var match = _tvdbRegex.Match(lineParts[0]);

                if (!match.Success)
                {
                    return false;
                }

                season = GetSeasonNumber(match);

                episode = GetEpisodeNumber(match);

                title = lineParts[1].Trim(' ', '\t', '"');

                #endregion
            }
            else
            {
                #region Wikipedia

                if (lineParts.Length < 3)
                {
                    return false;
                }

                season = lineParts[0];

                episode = lineParts[1];

                title = lineParts[2].Trim(' ', '\t', '"');

                #endregion
            }

            if (!int.TryParse(season, out _) || !int.TryParse(episode, out _))
            {
                return false;
            }

            if (!episodeTitles.TryGetValue(season, out var episodes))
            {
                episodes = new Dictionary<string, string>();

                episodeTitles.Add(season, episodes);
            }

            episodes.Add(episode, title);

            return false;
        }

        internal static string GetSeasonNumber(Match match)
        {
            var seasonText = match.Groups["Season"].Value.Trim();

            string seasonNumber;
            if (seasonText == "00" || seasonText == "0")
            {
                seasonNumber = "0";
            }
            else
            {
                seasonNumber = seasonText.TrimStart('0');
            }

            return seasonNumber;
        }

        internal static string GetEpisodeNumber(Match match) => match.Groups["Episode"].Value.Trim().TrimStart('0');
    }
}
