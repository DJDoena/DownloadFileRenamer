using System;
using System.Collections.Generic;
using System.IO;

namespace DoenaSoft.DownloadRenamer
{
    public static class RenameQueue
    {
        private static readonly object _lock;

        private static Dictionary<string, string> _renames;

        static RenameQueue()
        {
            _lock = new object();
        }

        public static void StartRename()
        {
            lock (_lock)
            {
                _renames = new Dictionary<string, string>();
            }
        }

        public static void TryAdd(FileInfo sourceFile, string targetFileName)
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

        public static void FinishRename()
        {
            lock (_lock)
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
        }
    }
}