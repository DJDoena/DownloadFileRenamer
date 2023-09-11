using System.IO;

namespace DoenaSoft.DownloadRenamer
{
    public static class FileRenamer
    {
        public static void AddRename(EpisodeModel model)
        {
            var sourceFileInfo = new FileInfo(model.SourceFileName);

            var targetFileName = Path.Combine(sourceFileInfo.DirectoryName, model.TargetFileName);

            RenameQueue.TryAdd(sourceFileInfo, targetFileName);

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

            EpisodeInfoCreator.Create(targetFileName, seriesName.ShortName, title, model.AirDate, model.EpisodeNumber, model.TvdbId);

            var partnerFilesSourceName = Path.GetFileNameWithoutExtension(sourceFileInfo.Name);

            var partnerSourceFiles = sourceFileInfo.Directory.GetFiles($"{partnerFilesSourceName}*.*", SearchOption.TopDirectoryOnly);

            var partnerTargetFileNamePrefix = Path.GetFileNameWithoutExtension(targetFileName);

            foreach (var partnerSourceFile in partnerSourceFiles)
            {
                if (Path.GetFullPath(partnerSourceFile.FullName) == Path.GetFullPath(sourceFileInfo.FullName))
                {
                    continue;
                }

                var partnerTargetFileName = GetPartnerTargetFileName(partnerSourceFile, partnerFilesSourceName, partnerTargetFileNamePrefix);

                RenameQueue.TryAdd(partnerSourceFile, partnerTargetFileName);
            }
        }

        private static string GetPartnerTargetFileName(FileInfo partnerSourceFile, string partnerFilesSourceName, string partnerTargetFileNamePrefix)
        {
            var partnerTargetFileName = Path.GetFileNameWithoutExtension(partnerSourceFile.Name);

            var partnerFileExtension = Path.GetExtension(partnerSourceFile.Name);

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

            partnerTargetFileName = Path.Combine(partnerSourceFile.DirectoryName, $"{partnerTargetFileName}{partnerSourceFile.Extension}");

            return partnerTargetFileName;
        }
    }
}