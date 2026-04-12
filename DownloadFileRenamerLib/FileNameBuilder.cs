using System.Text;

namespace DoenaSoft.DownloadRenamer;

public static class FileNameBuilder
{
    public static void Build(EpisodeModel model
        , bool silent)
    {
        model.TargetFileName = string.Empty;

        if (model.ShowName == null)
        {
            if (!silent)
            {
                throw new BuilderException("No show selected");
            }
            else
            {
                return;
            }
        }
        else if (string.IsNullOrWhiteSpace(model.EpisodeName))
        {
            if (!silent)
            {
                throw new BuilderException("No episode name entered");
            }
            else
            {
                return;
            }
        }
        else if (string.IsNullOrWhiteSpace(model.EpisodeNumber))
        {
            if (!silent)
            {
                throw new BuilderException("No episode number entered");
            }
            else
            {
                return;
            }
        }
        else if (string.IsNullOrWhiteSpace(model.Resolution))
        {
            if (!silent)
            {
                throw new BuilderException("No resolution entered");
            }
            else
            {
                return;
            }
        }
        else if (string.IsNullOrWhiteSpace(model.Extension))
        {
            if (!silent)
            {
                throw new BuilderException("No extension entered");
            }
            else
            {
                return;
            }
        }

        model.EpisodeName = model.EpisodeName.Trim('"', ' ', '\t');

        var fileNameBuilder = new StringBuilder();

        var shortName = GetSelectedShortName(model);

        fileNameBuilder.Append(shortName);
        fileNameBuilder.Append(" ");
        fileNameBuilder.Append(model.EpisodeNumber);
        fileNameBuilder.Append(" [ ");

        if (!string.IsNullOrEmpty(model.AirDate))
        {
            fileNameBuilder.Append(model.AirDate);
            fileNameBuilder.Append(" ");
        }

        if (!EpisodeNameIsClean(model))
        {
            var episodeName = GetCleanEpisodeName(model);

            fileNameBuilder.Append(episodeName);

            model.FullEpisodeName = model.EpisodeName;
        }
        else
        {
            fileNameBuilder.Append(model.EpisodeName);

            model.FullEpisodeName = string.Empty;
        }

        fileNameBuilder.Append(" ].");

        if (model.GermanAudio)
        {
            fileNameBuilder.Append("de.");
        }

        fileNameBuilder.Append(model.Resolution);
        fileNameBuilder.Append(model.Extension);

        model.TargetFileName = fileNameBuilder.ToString();
    }

    private static string GetSelectedShortName(EpisodeModel model) 
        => model.ShowName.ShortName;

    private static bool EpisodeNameIsClean(EpisodeModel model) 
        => !model.EpisodeName.Any(c => Path.GetInvalidFileNameChars().Contains(c));

    private static string GetCleanEpisodeName(EpisodeModel model)
    {
        var cleanedEpisodeName = model.EpisodeName;

        cleanedEpisodeName = cleanedEpisodeName.Replace(" / ", " - ");
        cleanedEpisodeName = cleanedEpisodeName.Replace("/", " - ");
        cleanedEpisodeName = cleanedEpisodeName.Replace(" : ", " - ");
        cleanedEpisodeName = cleanedEpisodeName.Replace(": ", " - ");

        foreach (var c in Path.GetInvalidFileNameChars())
        {
            cleanedEpisodeName = cleanedEpisodeName.Replace(c, '-');
        }

        cleanedEpisodeName = cleanedEpisodeName.Trim('-');

        return cleanedEpisodeName;
    }
}