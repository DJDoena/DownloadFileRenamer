using System.Text.RegularExpressions;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CopySeries;
using DoenaSoft.DownloadRenamer;

namespace DoenaSoft.MassDownloadFileRenamer;

internal sealed class FileRenamer
{
    private readonly DownloadRenamer.FileRenamer _actualRenamer;

    private readonly Regex _fileNameRegex;

    private readonly Dictionary<string, Dictionary<string, string>> _episodeTitles;

    private readonly string _resolution;

    private readonly bool _germanAudio;

    private readonly Name _showName;

    public FileRenamer(DownloadRenamer.FileRenamer actualRenamer
        , Regex fileNameRegex
        , Dictionary<string, Dictionary<string, string>> episodeTitles
        , string shortName
        , string resolution
        , bool germanAudio)
    {
        _actualRenamer = actualRenamer;
        _fileNameRegex = fileNameRegex;

        _episodeTitles = episodeTitles;

        _resolution = resolution;

        _germanAudio = germanAudio;

        _showName = Helper.ReadNames().First(n => n.ShortName == shortName);
    }

    public void Rename(IEnumerable<IFileInfo> files)
    {
        foreach (var file in files)
        {
            this.Rename(file);
        }
    }

    public void Rename(IFileInfo file)
    {
        var match = _fileNameRegex.Match(file.Name);

        if (match.Success)
        {
            var seasonNumber = TitleReader.GetSeasonNumber(match);

            var episodeNumber = TitleReader.GetEpisodeNumber(match);

            if (match.Groups["Episode2"].Success)
            {
                Console.WriteLine($"Warning: double episode: {file.Name}");
            }

            if (!_episodeTitles[seasonNumber].TryGetValue(episodeNumber, out var episodeName))
            {
                var altEpisode = episodeNumber.TrimStart('0');

                episodeName = _episodeTitles[seasonNumber][altEpisode];
            }

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

            _actualRenamer.AddRename(model);
        }
        else
        {
            Console.WriteLine("No match: " + file.Name);
        }
    }
}