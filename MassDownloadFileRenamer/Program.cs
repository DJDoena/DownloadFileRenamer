using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using DoenaSoft.AbstractionLayer.IOServices;
using SIO = System.IO;

namespace DoenaSoft.MassDownloadFileRenamer;

internal static class Program
{
    private const string ShortName = "Loudermilk";

    private const string TitleFile = @"D:\" + ShortName + ".csv";

    private const string SourceFolder = @"N:\Fresh Downloads\" + ShortName;

    private const bool UseTvdb = true;

    private const string Extension = ".mkv";

    private const string Resolution = "1080";

    private const bool GermanAudio = true;

    //S01E01
    private static readonly Regex _fileNameRegex = new("S(?'Season'[0-1][0-9])E(?'Episode'[0-9][0-9])(E(?'Episode2'[0-9][0-9]))?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    //1x01
    //private static readonly Regex _fileNameRegex = new("(?'Season'[0-1]?[0-9])x(?'Episode'[0-3][0-9])(x(?'Episode2'[0-9][0-9]))?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    //-101
    //private static readonly Regex _fileNameRegex = new("-(?'Season'[1-9])(?'Episode'[0-3][0-9])", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    //- 101 -
    //private static readonly Regex _fileNameRegex = new("- (?'Season'[1-9])(?'Episode'[0-5][0-9]) -", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    //101
    //private static readonly Regex _fileNameRegex = new("(?'Season'[1-9])(?'Episode'[0-3][0-9])", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private static void Main()
    {
        Console.WriteLine($"v{Assembly.GetExecutingAssembly().GetName().Version}");

        Debugger.Launch();

        if (!TitleReader.GetEpisodeTitles(TitleFile, UseTvdb, out var episodeTitles))
        {
            Console.WriteLine("Aborted.");
            Console.ReadLine();

            return;
        }

        var renameQueue = new RenameQueue();

        var actualRenamer = new DownloadRenamer.FileRenamer(renameQueue);

        var renamer = new FileRenamer(actualRenamer, _fileNameRegex, episodeTitles, ShortName, Resolution, GermanAudio);
        //var renamer = new SequentialFileRenamer(actualRenamer, episodeTitles, ShortName, Resolution, GermanAudio);

        var files = renameQueue.IOServices.Folder.GetFiles(SourceFolder, $"*{Extension}", SIO.SearchOption.TopDirectoryOnly)
            .ToList();

        try
        {
            renameQueue.StartRename();

            renamer.Rename(files);

            var count = renameQueue.FinishRename();

            Console.WriteLine($"{count} renamed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Finished.");
        Console.ReadLine();
    }
}