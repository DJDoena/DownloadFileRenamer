using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.DownloadRenamer;

public static class FileRenamer
{
    public static void AddRename(EpisodeModel model
        , IIOServices ioServices
        , IRenameQueue renameQueue)
    {
        var sourceFileInfo = ioServices.GetFileInfo(model.SourceFileName);

        var targetFileName = ioServices.Path.Combine(sourceFileInfo.FolderName, model.TargetFileName);

        renameQueue.Add(sourceFileInfo, targetFileName);

        string title;
        if (!string.IsNullOrEmpty(model.FullEpisodeName))
        {
            title = model.FullEpisodeName;

            var titleFileName = targetFileName + ".title";

            using var sw = ioServices.File.CreateText(titleFileName);

            if (!string.IsNullOrEmpty(model.AirDate))
            {
                sw.Write(model.AirDate);
                sw.Write(" ");
            }

            sw.WriteLine(model.FullEpisodeName);
        }
        else
        {
            title = model.EpisodeName;
        }

        var seriesName = model.ShowName;

        EpisodeInfoCreator.Create(targetFileName, seriesName.ShortName, title, model.AirDate, model.EpisodeNumber, model.TvdbId);

        var partnerFilesSourceName = ioServices.Path.GetFileNameWithoutExtension(sourceFileInfo.Name);

        var partnerSourceFiles = sourceFileInfo.Folder.GetFiles($"{partnerFilesSourceName}*.*", System.IO.SearchOption.TopDirectoryOnly);

        var partnerTargetFileNamePrefix = ioServices.Path.GetFileNameWithoutExtension(targetFileName);

        foreach (var partnerSourceFile in partnerSourceFiles)
        {
            if (ioServices.Path.GetFullPath(partnerSourceFile.FullName) == ioServices.Path.GetFullPath(sourceFileInfo.FullName))
            {
                continue;
            }

            var partnerTargetFileName = GetPartnerTargetFileName(ioServices, partnerSourceFile, partnerFilesSourceName, partnerTargetFileNamePrefix);

            renameQueue.Add(partnerSourceFile, partnerTargetFileName);
        }
    }

    private static string GetPartnerTargetFileName(IIOServices ioServices, IFileInfo partnerSourceFile, string partnerFilesSourceName, string partnerTargetFileNamePrefix)
    {
        var partnerTargetFileName = ioServices.Path.GetFileNameWithoutExtension(partnerSourceFile.Name);

        var partnerFileExtension = ioServices.Path.GetExtension(partnerSourceFile.Name);

        switch (partnerFileExtension)
        {
            case ".title":
            case ".nfo":
                {
                    return partnerSourceFile.FullName;
                }
        }

        if (partnerTargetFileName.Length == partnerFilesSourceName.Length)
        {
            partnerTargetFileName = partnerTargetFileName.Replace(partnerFilesSourceName, partnerTargetFileNamePrefix);
        }
        else
        {
            partnerTargetFileName = partnerTargetFileName.Replace(partnerFilesSourceName, $"{partnerTargetFileNamePrefix}.");
        }

        if (partnerTargetFileName.StartsWith($"{partnerTargetFileNamePrefix}.-"))
        {
            partnerTargetFileName = partnerTargetFileName.Replace($"{partnerTargetFileNamePrefix}.-", $"{partnerTargetFileNamePrefix}.");
        }

        partnerTargetFileName = ioServices.Path.Combine(partnerSourceFile.FolderName, $"{partnerTargetFileName}{partnerSourceFile.Extension}");

        return partnerTargetFileName;
    }
}