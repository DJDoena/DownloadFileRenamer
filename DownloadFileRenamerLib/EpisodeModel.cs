using DoenaSoft.CopySeries;

namespace DoenaSoft.DownloadRenamer;

public sealed class EpisodeModel
{
    private string _sourceFileName;

    private Name _showName;

    private string _episodeName;

    private string _episodeNumber;

    private string _tvdbId;

    private string _airDate;

    private string _resolution;

    private string _extension;

    private string _targetFileName;

    private bool _germanAudio;

    private string _fullEpisodeName;

    public string SourceFileName
    {
        get => _sourceFileName;
        set
        {
            if (_sourceFileName != value)
            {
                _sourceFileName = value;

                SourceFileNameChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public Name ShowName
    {
        get => _showName;
        set
        {
            if (_showName != value)
            {
                _showName = value;

                ShowNameChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public string EpisodeName
    {
        get => _episodeName;
        set
        {
            if (_episodeName != value)
            {
                _episodeName = value;

                EpisodeNameChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public string EpisodeNumber
    {
        get => _episodeNumber;
        set
        {
            if (_episodeNumber != value)
            {
                _episodeNumber = value;

                EpisodeNumberChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public string TvdbId
    {
        get => _tvdbId;
        set
        {
            if (_tvdbId != value)
            {
                _tvdbId = value;

                TvdbIdChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public string AirDate
    {
        get => _airDate;
        set
        {
            if (_airDate != value)
            {
                _airDate = value;

                AirDateChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public string Resolution
    {
        get => _resolution;
        set
        {
            if (_resolution != value)
            {
                _resolution = value;

                ResolutionChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public string Extension
    {
        get => _extension;
        set
        {
            if (_extension != value)
            {
                _extension = value;

                ExtensionChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public string TargetFileName
    {
        get => _targetFileName;
        set
        {
            if (_targetFileName != value)
            {
                _targetFileName = value;

                TargetFileNameChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public bool GermanAudio
    {
        get => _germanAudio;
        set
        {
            if (_germanAudio != value)
            {
                _germanAudio = value;

                GermanAudioChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public string FullEpisodeName
    {
        get => _fullEpisodeName;
        set
        {
            if (_fullEpisodeName != value)
            {
                _fullEpisodeName = value;

                FullEpisodeNameChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public event EventHandler SourceFileNameChanged;

    public event EventHandler ShowNameChanged;

    public event EventHandler EpisodeNameChanged;

    public event EventHandler EpisodeNumberChanged;

    public event EventHandler TvdbIdChanged;

    public event EventHandler AirDateChanged;

    public event EventHandler ResolutionChanged;

    public event EventHandler ExtensionChanged;

    public event EventHandler TargetFileNameChanged;

    public event EventHandler GermanAudioChanged;

    public event EventHandler FullEpisodeNameChanged;
}
