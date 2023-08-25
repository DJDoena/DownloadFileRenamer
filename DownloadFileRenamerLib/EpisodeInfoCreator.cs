using System.IO;
using System.Text;
using System.Xml;
using DoenaSoft.ToolBox.Generics;

namespace DoenaSoft.DownloadRenamer
{
    internal static class EpisodeInfoCreator
    {
        public static void Create(string videoFileName, string seriesShortName, string title, string airdate, string seasonAndEpisode, string tvdbId)
        {
            var videoFI = new FileInfo(videoFileName);

            var nfoFileName = Path.GetFileNameWithoutExtension(videoFI.Name) + ".nfo";

            var targetFileName = Path.Combine(videoFI.DirectoryName, nfoFileName);

            var homeId = $"{seriesShortName}_{seasonAndEpisode}";

            UniqueId[] uniqueIds;
            if (string.IsNullOrEmpty(tvdbId))
            {
                uniqueIds = new[]
                {
                    new UniqueId()
                    {
                        type = "home",
                        @default = true,
                        Value = homeId,
                    },
                };
            }
            else
            {
                uniqueIds = new[]
                {
                    new UniqueId()
                    {
                        type = "tvdb",
                        @default = true,
                        Value = tvdbId,
                    },
                    new UniqueId()
                    {
                        type = "home",
                        @default = false,
                        Value = homeId,
                    },
                };
            }

            var episodeDetails = new EpisodeDetails()
            {
                title = title,
                uniqueid = uniqueIds,
            };

            if (!string.IsNullOrEmpty(airdate))
            {
                episodeDetails.aired = XmlConvert.ToDateTime(airdate, XmlDateTimeSerializationMode.Unspecified);
                episodeDetails.airedSpecified = true;
            }

            var split = seasonAndEpisode.Split('x');

            episodeDetails.season = split[0];

            episodeDetails.episode = string.Join(" & ", split, 1, split.Length - 1);

            Serializer<EpisodeDetails>.Serialize(targetFileName, episodeDetails, new UTF8Encoding(false));
        }
    }
}
