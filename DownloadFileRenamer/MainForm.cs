using System.Reflection;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CopySeries;

namespace DoenaSoft.DownloadRenamer;

public partial class MainForm : Form
{
    private readonly EpisodeModel _model;

    private readonly Helper _helper;

    private readonly List<string> _dateShows;

    private readonly List<CrypticName> _crypticNames;

    private readonly HashSet<Tuple<string, string>> _episodeNames;

    private List<Name> _names;

    private int _currentSeriesIndex;

    public MainForm()
    {
        _currentSeriesIndex = -1;

        _model = new();

        _model.AirDateChanged += this.OnModelAirDateChanged;
        _model.EpisodeNameChanged += this.OnModelEpisodeNameChanged;
        _model.EpisodeNumberChanged += this.OnModelEpisodeNumberChanged;
        _model.ExtensionChanged += this.OnModelExtensionChanged;
        _model.FullEpisodeNameChanged += this.OnModelFullEpisodeNameChanged;
        _model.GermanAudioChanged += this.OnModelGermanAudioChanged;
        _model.ResolutionChanged += this.OnModelResolutionChanged;
        _model.ShowNameChanged += this.OnModelShowNameChanged;
        _model.SourceFileNameChanged += this.OnModelSourceFileNameChanged;
        _model.TargetFileNameChanged += this.OnModelTargetFileNameChanged;
        _model.TvdbIdChanged += this.OnModelTvdbIdChanged;

        _helper = new Helper(Path.Combine(@"N:\", "Fresh Downloads", "!Tools"));

        _dateShows = _helper.ReadDateShows();

        _crypticNames = _helper.ReadCrypticNames();

        this.InitializeComponent();

        this.Icon = Resource.djdsoft;

        SeriesNameComboBox.DisplayMember = nameof(CopySeries.Name.LongName);
        SeriesNameComboBox.ValueMember = nameof(CopySeries.Name.ShortName);

        _names = _helper.ReadNames();

        SeriesNameComboBox.DataSource = _names;

        _episodeNames = new();

        this.Clean();

        this.Text += $" {Assembly.GetExecutingAssembly().GetName().Version}";
    }

    private void Clean()
    {
        SourceFileTextBox.Text = string.Empty;

        EpisodeNamesLinkTextBox.Text = string.Empty;

        SeriesNameComboBox.SelectedIndex = -1;

        EpisodeNameTextBox.Text = string.Empty;

        EpisodeNumberTextBox.Text = string.Empty;

        ResolutionTextBox.Text = string.Empty;

        ExtensionTextBox.Text = string.Empty;

        AirDateTextBox.Text = string.Empty;

        CrypticNameTextBox.Text = string.Empty;

        TargetFileNameTextBox.Text = string.Empty;

        FullEpisodeNameTextBox.Text = string.Empty;

        TvdbIdTextBox.Text = string.Empty;
    }

    private void OnSelectFileButtonClicked(object sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog()
        {
            CheckFileExists = true,
            Filter = "Video files|*.mkv;*.mp4;*.avi|All files|*.*",
            InitialDirectory = Path.Combine("N:", "Fresh Downloads", "Finished"),
            Multiselect = false,
            RestoreDirectory = true,
            Title = "Please select video file",
        };

        if (ofd.ShowDialog() == DialogResult.OK)
        {
            this.TryParse(ofd.FileName);
        }
    }

    private void TryParse(string fullFileName)
    {
        if (string.IsNullOrEmpty(fullFileName))
        {
            return;
        }

        var fileInfo = new FileInfo(fullFileName);

        SourceFileTextBox.Text = fileInfo.FullName;

        var extension = fileInfo.Extension;

        var fileName = fileInfo.Name.Substring(0, fileInfo.Name.Length - extension.Length);

        var previousSeriesIndex = _currentSeriesIndex;

        SeriesNameComboBox.SelectedIndex = this.GetSeries(fileName);

        if (previousSeriesIndex != SeriesNameComboBox.SelectedIndex || SeriesNameComboBox.SelectedIndex == -1)
        {
            DeCheckbox.Checked = false;

            if (SeriesNameComboBox.SelectedIndex != -1 && this.GetSelectedName().OriginalLanguage == "ger")
            {
                DeCheckbox.Checked = true;
            }
            else if (fileName.IndexOf("german", StringComparison.InvariantCultureIgnoreCase) != -1
                && MessageBox.Show("Set German?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DeCheckbox.Checked = true;
            }
        }

        EpisodeNumberTextBox.Text = Helper.GetEpisodeNumber(fileName);

        ResolutionTextBox.Text = Helper.GetResolution(fileName);

        ExtensionTextBox.Text = extension;

        AirDateTextBox.Text = this.GetDefaultAirDate();

        CrypticNameTextBox.Text = string.Empty;
    }

    private int GetSeries(string fileName)
    {
        var potentialMatches = _crypticNames.Where(cn => fileName.IndexOf(cn.DownloadName, StringComparison.InvariantCultureIgnoreCase) >= 0);

        var length = 0;

        var nameIndex = -1;

        foreach (var crypticName in potentialMatches)
        {
            if (crypticName.DownloadName.Length > length)
            {
                nameIndex = _names.FindIndex(n => n.ShortName == crypticName.ShortName);

                length = crypticName.DownloadName.Length;
            }
        }

        return nameIndex;
    }

    private string GetDefaultAirDate()
    {
        if (SeriesNameComboBox.SelectedIndex >= 0)
        {
            var shortName = this.GetSelectedShortName();

            if (_dateShows.Contains(shortName))
            {
                var result = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");

                return result;
            }
        }

        return string.Empty;
    }

    private Name GetSelectedName() => (Name)SeriesNameComboBox.SelectedItem;

    private string GetSelectedShortName() => this.GetSelectedName().ShortName;

    private void OnFormClosing(object sender, FormClosingEventArgs e)
    {
        _helper.WriteCrypticNames(_crypticNames);

        _helper.WriteNames(_names);
    }

    private void OnProcessButtonClicked(object sender, EventArgs e)
    {
        try
        {
            FileNameBuilder.Build(_model, false);
        }
        catch (BuilderException ex)
        {
            MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void OnRenameButtonClick(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TargetFileNameTextBox.Text))
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(CrypticNameTextBox.Text)
            && SeriesNameComboBox.SelectedIndex >= 0
            && !_crypticNames.Any(n => string.Equals(n.DownloadName, CrypticNameTextBox.Text, StringComparison.InvariantCultureIgnoreCase)))
        {
            var shortName = this.GetSelectedShortName();

            _crypticNames.Add(new CrypticName()
            {
                DownloadName = CrypticNameTextBox.Text,
                ShortName = shortName,
            });
        }

        if (!string.IsNullOrWhiteSpace(EpisodeNamesLinkTextBox.Text)
            && SeriesNameComboBox.SelectedIndex >= 0)
        {
            var name = this.GetSelectedName();

            name.EpisodeNamesLink = EpisodeNamesLinkTextBox.Text;
        }

        var episodeName = new Tuple<string, string>(_model.EpisodeName, _model.TvdbId);

        if (!_episodeNames.Add(episodeName))
        {
            if (MessageBox.Show($"Episode name '{_model.EpisodeName}' seems to have been used already. Continue?", "Duplicate", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
        }

        try
        {
            var renameQueue = new RenameQueue(logger: new ConsoleLogger());

            renameQueue.Initialize();

            (new FileRenamer(renameQueue)).AddRename(_model);

            renameQueue.Commit();

            this.Clean();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void OnPasteTitleButtonClicked(object sender, EventArgs e)
    {
        try
        {
            EpisodeNameTextBox.Text = Clipboard.GetText();

            try
            {
                FileNameBuilder.Build(_model, true);
            }
            catch (BuilderException ex)
            {
                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch
        {
        }
    }

    private void OnNewSeriesButtonClick(object sender, EventArgs e)
    {
        _helper.WriteNames(_names);

        using (var form = new NewSeriesForm(_helper))
        {
            if (form.ShowDialog() == DialogResult.OK)
            {
                _names = _helper.ReadNames();

                SeriesNameComboBox.DataSource = _names;

                SeriesNameComboBox.SelectedIndex = -1;

                this.TryParse(SourceFileTextBox.Text);

                if (SeriesNameComboBox.SelectedIndex == -1)
                {
                    SeriesNameComboBox.SelectedIndex = _names.FindIndex(n => n.ShortName == form.NewShortName);
                }
            }
        }
    }

    private void OnEpisodeNamesLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        if (EpisodeNamesLinkLabel.Text.StartsWith("http"))
        {
            System.Diagnostics.Process.Start(EpisodeNamesLinkLabel.Text);
        }
    }

    private void OnSeriesNameComboBoxSelectedIndexChanged(object sender, EventArgs e)
    {
        if (SeriesNameComboBox.SelectedIndex >= 0)
        {
            var selected = this.GetSelectedName();

            EpisodeNamesLinkLabel.Text = selected.EpisodeNamesLink ?? selected.Link ?? string.Empty;

            AirDateTextBox.Text = this.GetDefaultAirDate();

            _model.ShowName = selected;

            _currentSeriesIndex = SeriesNameComboBox.SelectedIndex;
        }
        else
        {
            EpisodeNamesLinkLabel.Text = string.Empty;

            _model.ShowName = null;
        }

        EpisodeNamesLinkLabel.Enabled = !string.IsNullOrEmpty(EpisodeNamesLinkLabel.Text);
    }

    private void OnPasteTvdbIdButtonClick(object sender, EventArgs e)
    {
        try
        {
            TvdbIdTextBox.Text = Clipboard.GetText();
        }
        catch
        {
        }
    }

    private void OnParseHtmlButtonClick(object sender, EventArgs e)
    {
        try
        {
            var clipboard = Clipboard.GetData(DataFormats.Html) as string;

            HtmlParser.Parse(_model, clipboard);
        }
        catch (BuilderException ex)
        {
            MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch
        {
        }
    }

    private void OnSourceFileTextBoxTextChanged(object sender, EventArgs e)
    {
        _model.SourceFileName = SourceFileTextBox.Text;
    }

    private void OnModelSourceFileNameChanged(object sender, EventArgs e)
    {
        if (SourceFileTextBox.Text != _model.SourceFileName)
        {
            SourceFileTextBox.Text = _model.SourceFileName;
        }
    }

    private void OnEpisodeNameTextBoxTextChanged(object sender, EventArgs e)
    {
        _model.EpisodeName = EpisodeNameTextBox.Text;

        if (!EpisodeNameTextBox.Text.EndsWith(" "))
        {
            this.TryBuildFileName();
        }
    }

    private void OnModelShowNameChanged(object sender, EventArgs e)
    {
        if ((Name)SeriesNameComboBox.SelectedItem != _model.ShowName)
        {
            SeriesNameComboBox.SelectedItem = _model.ShowName;
        }
    }

    private void OnModelEpisodeNameChanged(object sender, EventArgs e)
    {
        if (EpisodeNameTextBox.Text != _model.EpisodeName)
        {
            EpisodeNameTextBox.Text = _model.EpisodeName;
        }
    }

    private void OnEpisodeNumberTextBoxTextChanged(object sender, EventArgs e)
    {
        _model.EpisodeNumber = EpisodeNumberTextBox.Text;

        this.TryBuildFileName();
    }

    private void OnModelEpisodeNumberChanged(object sender, EventArgs e)
    {
        if (EpisodeNumberTextBox.Text != _model.EpisodeNumber)
        {
            EpisodeNumberTextBox.Text = _model.EpisodeNumber;
        }
    }

    private void OnTvdbIdTextBoxTextChanged(object sender, EventArgs e)
    {
        _model.TvdbId = TvdbIdTextBox.Text;
    }

    private void OnModelTvdbIdChanged(object sender, EventArgs e)
    {
        if (TvdbIdTextBox.Text != _model.TvdbId)
        {
            TvdbIdTextBox.Text = _model.TvdbId;
        }
    }

    private void OnAirDateTextBoxTextChanged(object sender, EventArgs e)
    {
        _model.AirDate = AirDateTextBox.Text;

        this.TryBuildFileName();
    }

    private void OnModelAirDateChanged(object sender, EventArgs e)
    {
        if (AirDateTextBox.Text != _model.AirDate)
        {
            AirDateTextBox.Text = _model.AirDate;
        }
    }

    private void OnResolutionTextBoxTextChanged(object sender, EventArgs e)
    {
        _model.Resolution = ResolutionTextBox.Text;

        this.TryBuildFileName();
    }

    private void OnModelResolutionChanged(object sender, EventArgs e)
    {
        if (ResolutionTextBox.Text != _model.Resolution)
        {
            ResolutionTextBox.Text = _model.Resolution;
        }
    }

    private void OnExtensionTextBoxTextChanged(object sender, EventArgs e)
    {
        _model.Extension = ExtensionTextBox.Text;
    }

    private void OnModelExtensionChanged(object sender, EventArgs e)
    {
        if (ExtensionTextBox.Text != _model.Extension)
        {
            ExtensionTextBox.Text = _model.Extension;
        }
    }

    private void OnTargetFileNameTextBoxTextChanged(object sender, EventArgs e)
    {
        _model.TargetFileName = TargetFileNameTextBox.Text;
    }

    private void OnModelTargetFileNameChanged(object sender, EventArgs e)
    {
        if (TargetFileNameTextBox.Text != _model.TargetFileName)
        {
            TargetFileNameTextBox.Text = _model.TargetFileName;
        }
    }

    private void OnDeCheckboxCheckedChanged(object sender, EventArgs e)
    {
        _model.GermanAudio = DeCheckbox.Checked;

        this.TryBuildFileName();
    }

    private void TryBuildFileName()
    {
        try
        {
            FileNameBuilder.Build(_model, true);
        }
        catch
        {
        }
    }

    private void OnModelGermanAudioChanged(object sender, EventArgs e)
    {
        if (DeCheckbox.Checked != _model.GermanAudio)
        {
            DeCheckbox.Checked = _model.GermanAudio;
        }
    }

    private void OnFullEpisodeNameTextBoxTextChanged(object sender, EventArgs e)
    {
        _model.FullEpisodeName = FullEpisodeNameTextBox.Text;
    }

    private void OnModelFullEpisodeNameChanged(object sender, EventArgs e)
    {
        if (FullEpisodeNameTextBox.Text != _model.FullEpisodeName)
        {
            FullEpisodeNameTextBox.Text = _model.FullEpisodeName;
        }
    }
}