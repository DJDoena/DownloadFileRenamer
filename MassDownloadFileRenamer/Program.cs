using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using DoenaSoft.AbstractionLayer.IOServices;
using SIO = System.IO;

namespace DoenaSoft.MassDownloadFileRenamer
{
    internal static class Program
    {
        private const string ShortName = "Hercules";

        private const string TitleFile = @"D:\" + ShortName + ".csv";

        private const string SourceFolder = @"N:\Fresh Downloads\" + ShortName;

        private const bool UseTvdb = true;

        private const string Extension = ".mkv";

        private const string Resolution = "720";

        private const bool GermanAudio = true;

        //S01E01
        private static readonly Regex _fileNameRegex = new Regex("S(?'Season'[0-1][0-9])E(?'Episode'[0-9][0-9])(E(?'Episode2'[0-9][0-9]))?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        //1x01
        //private static readonly Regex _fileNameRegex = new Regex("(?'Season'[0-9])x(?'Episode'[0-3][0-9])(x(?'Episode2'[0-9][0-9]))?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        //-101
        //private static readonly Regex _fileNameRegex = new Regex("-(?'Season'[1-9])(?'Episode'[0-3][0-9])", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        //- 101 -
        //private static readonly Regex _fileNameRegex = new Regex("- (?'Season'[1-9])(?'Episode'[0-5][0-9]) -", RegexOptions.Compiled | RegexOptions.IgnoreCase);

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

            var ioServices = new IOServices();

            var renameQueue = new RenameQueue(ioServices);

            var renamer = new FileRenamer(ioServices, renameQueue, _fileNameRegex, episodeTitles, ShortName, Resolution, GermanAudio);
            //var renamer = new SequentialFileRenamer(ioServices, renameQueue, episodeTitles, ShortName, Resolution, GermanAudio);

            var files = ioServices.Folder.GetFiles(SourceFolder, $"*{Extension}", SIO.SearchOption.TopDirectoryOnly)
                .ToList();

            try
            {
                renameQueue.StartRename();

                renamer.Rename(files);

                renameQueue.FinishRename();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Finished.");
            Console.ReadLine();
        }
    }
}