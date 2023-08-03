namespace DoenaSoft.MassDownloadFileRenamer
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text.RegularExpressions;

    internal static class Program
    {
        private const string TitleFile = @"D:\RS.csv";

        private const string ShortName = "RemingtonSteele";

        private const string SourceFolder = @"N:\Fresh Downloads\RS";

        private const bool UseTvdb = true;

        private const string Extension = ".mkv";

        private const string Resolution = "480";

        private const bool GermanAudio = true;

        //S01E01
        //private static readonly Regex _fileNameRegex = new Regex("S(?'Season'[0-1][0-9])E(?'Episode'[0-9][0-9])(E(?'Episode2'[0-9][0-9]))?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        //1x01
        //private static readonly Regex _fileNameRegex = new Regex("(?'Season'[0-9])x(?'Episode'[0-3][0-9])", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        //101
        private static readonly Regex _fileNameRegex = new Regex("-(?'Season'[1-9])(?'Episode'[0-3][0-9])", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        //
        //private static readonly Regex _fileNameRegex = new Regex("S(?'Season'[1-9])E(?'Episode'[0-3][0-9])(E(?'Episode2'[0-3][0-9]))?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static void Main()
        {
            Debugger.Launch();

            if (!TitleReader.GetEpisodeTitles(TitleFile, UseTvdb, out var episodeTitles))
            {
                Console.WriteLine("Aborted.");
                Console.ReadLine();

                return;
            }

            var renamer = new FileRenamer(_fileNameRegex, episodeTitles, ShortName, Resolution, GermanAudio);

            var fileNames = Directory.GetFiles(SourceFolder, $"*{Extension}", SearchOption.TopDirectoryOnly);

            foreach (var fileName in fileNames)
            {
                var file = new FileInfo(fileName);

                try
                {
                    renamer.Rename(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error renaming '{file.Name}': {ex.Message}");
                }
            }

            Console.WriteLine("Finished.");
            Console.ReadLine();
        }
    }
}