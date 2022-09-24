namespace DoenaSoft.DownloadRenamer
{
    using System.IO;

    public static class FileRenamer
    {
        public static void Rename(EpisodeModel model)
        {
            var sourceFileInfo = new FileInfo(model.SourceFileName);

            var targetFileName = Path.Combine(sourceFileInfo.DirectoryName, model.TargetFileName);

            sourceFileInfo.MoveTo(targetFileName);

            string title;
            if (!string.IsNullOrEmpty(model.FullEpisodeName))
            {
                title = model.FullEpisodeName;

                var titleFileName = targetFileName + ".title";

                using (var sw = File.CreateText(titleFileName))
                {
                    if (!string.IsNullOrEmpty(model.AirDate))
                    {
                        sw.Write(model.AirDate);
                        sw.Write(" ");
                    }

                    sw.WriteLine(model.FullEpisodeName);
                }
            }
            else
            {
                title = model.EpisodeName;
            }

            var seriesName = model.ShowName;

            EpisodeInfoCreator.Create(targetFileName, seriesName.ShortName, title, model.AirDate, model.EpisodeNumber, seriesName.Year, model.TvdbId);

            var partnerFilesSourceName = Path.GetFileNameWithoutExtension(sourceFileInfo.Name);

            var partnerSourceFiles = sourceFileInfo.Directory.GetFiles($"{partnerFilesSourceName}*.*", SearchOption.TopDirectoryOnly);

            var partnerTargetFileNamePrefix = Path.GetFileNameWithoutExtension(targetFileName);

            foreach (var partnerSourceFile in partnerSourceFiles)
            {
                var partnerTargetFileName = Path.GetFileNameWithoutExtension(partnerSourceFile.Name);

                partnerTargetFileName = partnerTargetFileName.Replace(partnerFilesSourceName, partnerTargetFileNamePrefix);

                partnerTargetFileName = Path.Combine(partnerSourceFile.DirectoryName, $"{partnerTargetFileName}{partnerSourceFile.Extension}");

                partnerSourceFile.MoveTo(partnerTargetFileName);
            }
        }
    }
}