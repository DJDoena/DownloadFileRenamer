using System.Text;
using System.Xml;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.ToolBox.Generics;
using SIO = System.IO;

namespace DoenaSoft.DownloadRenamer;

internal sealed class EpisodeInfoCreator
{
    private readonly IIOServices _ioServices;

    public EpisodeInfoCreator(IIOServices ioServices)
    {
        _ioServices = ioServices;
    }

    public void Create(string videoFileName, string seriesShortName, string title, string airdate, string seasonAndEpisode, string tvdbId)
    {
        var videoFI = _ioServices.GetFile(videoFileName);

        var nfoFileName = _ioServices.Path.GetFileNameWithoutExtension(videoFI.Name) + ".nfo";

        var targetFileName = _ioServices.Path.Combine(videoFI.FolderName, nfoFileName);

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

        using var sw = _ioServices.GetFileStream(targetFileName, SIO.FileMode.Create, SIO.FileAccess.Write, SIO.FileShare.ReadWrite);

        XmlSerializer<EpisodeDetails>.Serialize(sw, episodeDetails, new UTF8Encoding(false));
    }
}