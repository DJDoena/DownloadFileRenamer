using System.Collections.Generic;
using System.Linq;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CopySeries;
using DoenaSoft.DownloadRenamer;

namespace DoenaSoft.MassDownloadFileRenamer
{
    internal sealed class SequentialFileRenamer
    {
        private readonly IOServices _ioServices;

        private readonly RenameQueue _renameQueue;

        private readonly Dictionary<string, Dictionary<string, string>> _episodeTitles;

        private readonly string _resolution;

        private readonly bool _germanAudio;

        private readonly Name _showName;

        public SequentialFileRenamer(IOServices ioServices
            , RenameQueue renameQueue
            , Dictionary<string, Dictionary<string, string>> episodeTitles
            , string shortName
            , string resolution
            , bool germanAudio)
        {
            _ioServices = ioServices;
            _renameQueue = renameQueue;
            _episodeTitles = episodeTitles;

            _resolution = resolution;

            _germanAudio = germanAudio;

            _showName = Helper.ReadNames().First(n => n.ShortName == shortName);
        }

        public void Rename(IEnumerable<IFileInfo> files)
        {
            var orderedFiles = files
                .OrderBy(f => f.Name)
                .ToList();

            var fileIndex = 0;

            var orderedSeasons = _episodeTitles
                .OrderBy(kvp => kvp.Key.PadLeft(2, '0'));

            foreach (var season in orderedSeasons)
            {
                var orderedEpisodes = season.Value
                    .OrderBy(kvp => kvp.Key.PadLeft(3, '0'));

                foreach (var episode in orderedEpisodes)
                {
                    if (fileIndex >= orderedFiles.Count)
                    {
                        break;
                    }

                    var file = orderedFiles[fileIndex];

                    var seasonNumber = season.Key;

                    var episodeNumber = episode.Key;

                    var episodeName = episode.Value;

                    var seasonAndEpisode = episodeNumber.Length == 1
                       ? $"{seasonNumber}x0{episodeNumber}"
                       : $"{seasonNumber}x{episodeNumber}";

                    var model = new EpisodeModel()
                    {
                        SourceFileName = file.FullName,
                        ShowName = _showName,
                        EpisodeName = episodeName,
                        EpisodeNumber = seasonAndEpisode,
                        Resolution = _resolution,
                        GermanAudio = _germanAudio,
                        Extension = file.Extension,
                    };

                    FileNameBuilder.Build(model, false);

                    DownloadRenamer.FileRenamer.AddRename(model, _ioServices, _renameQueue);

                    fileIndex++;
                }

                if (fileIndex >= orderedFiles.Count)
                {
                    break;
                }
            }
        }
    }
}