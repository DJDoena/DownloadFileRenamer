using System;
using System.Collections.Generic;
using System.IO;

namespace DoenaSoft.DownloadRenamer
{
    public static class FileRenamer
    {
        private static readonly object _lock;

        private static readonly Dictionary<string, string> _renames;

        static FileRenamer()
        {
            _lock = new object();

            _renames = new Dictionary<string, string>();
        }

        public static void StartRename(EpisodeModel model)
        {
            var sourceFileInfo = new FileInfo(model.SourceFileName);

            var targetFileName = Path.Combine(sourceFileInfo.DirectoryName, model.TargetFileName);

            TryAdd(sourceFileInfo, targetFileName);

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
                if (Path.GetFullPath(partnerSourceFile.FullName) == Path.GetFullPath(targetFileName))
                {
                    continue;
                }

                var partnerTargetFileName = GetPartnerTargetFileName(partnerSourceFile, partnerFilesSourceName, partnerTargetFileNamePrefix);

                TryAdd(partnerSourceFile, partnerTargetFileName);
            }
        }

        public static void FinishRename()
        {
            try
            {
                foreach (var kvp in _renames)
                {
                    var sourceFile = new FileInfo(kvp.Value);

                    var targetFile = new FileInfo(kvp.Key);

                    Console.WriteLine($@"{sourceFile.DirectoryName}\{sourceFile.Name} -> {targetFile.Name}");

                    sourceFile.MoveTo(targetFile.FullName);

                    File.SetAttributes(targetFile.FullName, FileAttributes.Archive);
                }
            }
            finally
            {
                _renames.Clear();
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

        private static void TryAdd(FileInfo sourceFile, string targetFileName)
        {
            var sourceFileName = Path.GetFullPath(sourceFile.FullName);

            targetFileName = Path.GetFullPath(targetFileName);

            if (sourceFileName == targetFileName)
            {
                return;
            }

            if (File.Exists(targetFileName))
            {
                throw new Exception($"Target file '{targetFileName}' already exists on disk!");
            }

            lock (_lock)
            {
                try
                {
                    _renames.Add(targetFileName, sourceFileName);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Target file '{targetFileName}' is already target of source file '{sourceFileName}", ex);
                }
            }
        }
    }
}