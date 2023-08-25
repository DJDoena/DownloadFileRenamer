using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DoenaSoft.CopySeries;
using DoenaSoft.ToolBox.Generics;

namespace DoenaSoft.DownloadRenamer
{
    public static class Helper
    {
        private static readonly string _root;

        private static readonly string _crypticNameFile;

        private static readonly Regex _episodeNumberRegex;

        static Helper()
        {
            _root = Path.Combine(@"N:\", "Fresh Downloads", "!Tools");

            _crypticNameFile = Path.Combine(_root, "CrypticNames.xml");

            _episodeNumberRegex = new Regex("(S(?'Season'[0-9]+)E(?'Episode'[0-9]+)(E(?'Episode2'[0-2][0-9]))?)|((?'Season'[0-9]+)x(?'Episode'[0-9]+)(x(?'Episode2'[0-2][0-9]))?)|(\\-(?'Season'[1-9])(?'Episode'[0-9]{2})$)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        public static List<Name> ReadNames()
        {
            var fileName = Path.Combine(_root, "Names.xml");

            var names = Serializer<Names>.Deserialize(fileName);

            var nameList = (names.NameList ?? Enumerable.Empty<Name>()).ToList();

            return nameList;
        }


        public static string GetResolution(string fileName)
        {
            if (fileName.Contains("1080"))
            {
                return "1080";
            }
            else if (fileName.Contains("720") || fileName.Contains("-7p-"))
            {
                return "720";
            }
            else
            {
                return "480";
            }
        }

        public static string GetEpisodeNumber(string fileName)
        {
            var match = _episodeNumberRegex.Match(fileName);

            if (match.Success)
            {
                var season = match.Groups["Season"].Value.TrimStart('0');

                var episode = match.Groups["Episode"].Value.PadLeft(2, '0');

                var result = $"{season}x{episode}";

                if (match.Groups["Episode2"].Success)
                {
                    var episode2 = match.Groups["Episode2"].Value.PadLeft(2, '0');

                    result += $"x{episode2}";
                }

                return result;
            }
            else
            {
                return string.Empty;
            }
        }

        public static List<string> ReadDateShows()
        {
            var fileName = Path.Combine(_root, "DateShows.xml");

            var names = Serializer<DateShows>.Deserialize(fileName);

            var nameList = (names.ShortNameList ?? Enumerable.Empty<string>()).ToList();

            return nameList;
        }

        public static void WriteNames(List<Name> names)
        {
            var nameList = new Names()
            {
                NameList = names.OrderBy(n => n, Comparer<Name>.Create((left, right) => left.SortName.CompareTo(right.SortName))).ToArray(),
            };

            var fileName = Path.Combine(_root, "Names.xml");

            if (File.Exists(fileName))
            {
                var backupFileName = Path.Combine(_root, "Names.xml.bak");

                if (File.Exists(backupFileName))
                {
                    File.Delete(backupFileName);
                }

                File.Move(fileName, backupFileName);
            }

            Serializer<Names>.Serialize(fileName, nameList);
        }

        public static List<CrypticName> ReadCrypticNames()
        {
            if (File.Exists(_crypticNameFile))
            {
                var names = Serializer<CrypticNames>.Deserialize(_crypticNameFile);

                var nameList = (names.CrypticNameList ?? Enumerable.Empty<CrypticName>()).ToList();

                return nameList;
            }
            else
            {
                return new List<CrypticName>();
            }
        }

        public static void WriteCrypticNames(List<CrypticName> crypticNames)
        {
            var nameList = new CrypticNames()
            {
                CrypticNameList = crypticNames
                    .OrderBy(cn => cn.ShortName)
                    .ThenBy(cn => cn.DownloadName)
                    .ToArray(),
            };

            Serializer<CrypticNames>.Serialize(_crypticNameFile, nameList);
        }
    }
}