using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.DownloadRenamer;

public sealed class FileRenamer
{
    private readonly IIOServices _ioServices;

    private readonly IRenameQueue _renameQueue;

    private readonly EpisodeInfoCreator _nfoCreator;

    public FileRenamer(IIOServices ioServices
        , IRenameQueue renameQueue)
    {
        _ioServices = ioServices;
        _renameQueue = renameQueue;

        _nfoCreator = new EpisodeInfoCreator(ioServices);
    }

    public void AddRename(EpisodeModel model)
    {
        var sourceFileInfo = _ioServices.GetFileInfo(model.SourceFileName);

        var targetFileName = _ioServices.Path.Combine(sourceFileInfo.FolderName, model.TargetFileName);

        _renameQueue.Add(sourceFileInfo, targetFileName);

        string title;
        if (!string.IsNullOrEmpty(model.FullEpisodeName))
        {
            title = model.FullEpisodeName;

            var titleFileName = targetFileName + ".title";

            using var sw = _ioServices.File.CreateText(titleFileName);

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

        var partnerFilesSourceName = _ioServices.Path.GetFileNameWithoutExtension(sourceFileInfo.Name);

        var partnerSourceFiles = sourceFileInfo.Folder.GetFiles($"{partnerFilesSourceName}*.*", System.IO.SearchOption.TopDirectoryOnly);

        var partnerTargetFileNamePrefix = _ioServices.Path.GetFileNameWithoutExtension(targetFileName);

        foreach (var partnerSourceFile in partnerSourceFiles)
        {
            if (_ioServices.Path.GetFullPath(partnerSourceFile.FullName) == _ioServices.Path.GetFullPath(sourceFileInfo.FullName))
            {
                continue;
            }

            var partnerTargetFileName = this.GetPartnerTargetFileName(partnerSourceFile, partnerFilesSourceName, partnerTargetFileNamePrefix);

            _renameQueue.Add(partnerSourceFile, partnerTargetFileName);
        }
    }

    private string GetPartnerTargetFileName(IFileInfo partnerSourceFile, string partnerFilesSourceName, string partnerTargetFileNamePrefix)
    {
        var partnerTargetFileName = _ioServices.Path.GetFileNameWithoutExtension(partnerSourceFile.Name);

        var partnerFileExtension = _ioServices.Path.GetExtension(partnerSourceFile.Name);

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

        partnerTargetFileName = _ioServices.Path.Combine(partnerSourceFile.FolderName, $"{partnerTargetFileName}{partnerSourceFile.Extension}");

        return partnerTargetFileName;
    }
}