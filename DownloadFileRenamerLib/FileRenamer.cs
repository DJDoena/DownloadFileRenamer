using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.DownloadRenamer;

public sealed class FileRenamer
{
    private readonly IRenameQueue _renameQueue;

    private readonly EpisodeInfoCreator _nfoCreator;

    private IIOServices IOServices
        => _renameQueue.IOServices;

    public FileRenamer(IRenameQueue renameQueue)
    {
        _renameQueue = renameQueue;

        _nfoCreator = new(renameQueue.IOServices);
    }

    public void AddRename(EpisodeModel model)
    {
        var sourceFileInfo = this.IOServices.GetFileInfo(model.SourceFileName);

        var targetFileName = this.IOServices.Path.Combine(sourceFileInfo.FolderName, model.TargetFileName);

        _renameQueue.Add(sourceFileInfo, targetFileName);

        string title;
        if (!string.IsNullOrEmpty(model.FullEpisodeName))
        {
            title = model.FullEpisodeName;

            var titleFileName = targetFileName + ".title";

            using var sw = this.IOServices.File.CreateText(titleFileName);

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

        _nfoCreator.Create(targetFileName, seriesName.ShortName, title, model.AirDate, model.EpisodeNumber, model.TvdbId);

        var partnerFilesSourceName = this.IOServices.Path.GetFileNameWithoutExtension(sourceFileInfo.Name);

        var partnerSourceFiles = sourceFileInfo.Folder.GetFiles($"{partnerFilesSourceName}*.*", System.IO.SearchOption.TopDirectoryOnly);

        var partnerTargetFileNamePrefix = this.IOServices.Path.GetFileNameWithoutExtension(targetFileName);

        foreach (var partnerSourceFile in partnerSourceFiles)
        {
            if (this.IOServices.Path.GetFullPath(partnerSourceFile.FullName) == this.IOServices.Path.GetFullPath(sourceFileInfo.FullName))
            {
                continue;
            }

            var partnerTargetFileName = this.GetPartnerTargetFileName(partnerSourceFile, partnerFilesSourceName, partnerTargetFileNamePrefix);

            _renameQueue.Add(partnerSourceFile, partnerTargetFileName);
        }
    }

    private string GetPartnerTargetFileName(IFileInfo partnerSourceFile, string partnerFilesSourceName, string partnerTargetFileNamePrefix)
    {
        var partnerTargetFileName = this.IOServices.Path.GetFileNameWithoutExtension(partnerSourceFile.Name);

        var partnerFileExtension = this.IOServices.Path.GetExtension(partnerSourceFile.Name);

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

        partnerTargetFileName = this.IOServices.Path.Combine(partnerSourceFile.FolderName, $"{partnerTargetFileName}{partnerSourceFile.Extension}");

        return partnerTargetFileName;
    }
}