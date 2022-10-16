namespace DoenaSoft.DownloadRenamer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Web;
    using System.Windows.Forms;
    using CopySeries;

    public partial class NewSeriesForm : Form
    {
        internal string NewShortName { get; private set; }

        public NewSeriesForm()
        {
            this.InitializeComponent();

            this.Icon = Resource.djdsoft;
        }

        private void OnCloseButtonClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void OnSaveButtonClick(object sender, EventArgs e)
        {
            var newShortName = ShortNameTextBox.Text.Trim();

            var newLongName = LongNameTextBox.Text.Trim();

            var link = UrlTextBox.Text.Trim();

            if (string.IsNullOrEmpty(newShortName))
            {
                MessageBox.Show("No short name!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            else if (string.IsNullOrEmpty(newLongName))
            {
                MessageBox.Show("No long name!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            else if (string.IsNullOrEmpty(link))
            {
                MessageBox.Show("No link!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            var names = Helper.ReadNames();

            if (AlreadyExists(names, n => n.ShortName.ToLower() == newShortName.ToLower())
                || AlreadyExists(names, n => n.LongName.ToLower() == newLongName.ToLower()))
            {
                return;
            }

            var newName = new Name()
            {
                ShortName = newShortName,
                LongName = newLongName,
                Link = link,
                OriginalLanguage = OriginalLanguageTextBox.Text.Trim(),
                SortName = !string.IsNullOrWhiteSpace(SortNameTextBox.Text) ? SortNameTextBox.Text.Trim() : null,
                DisplayName = !string.IsNullOrWhiteSpace(DisplayNameTextBox.Text) ? DisplayNameTextBox.Text.Trim() : null,
                LocalizedName = !string.IsNullOrWhiteSpace(LocalizedNameTextBox.Text) ? LocalizedNameTextBox.Text.Trim() : null,
            };

            names.Insert(0, newName);

            Helper.WriteNames(names);

            this.NewShortName = newName.ShortName;

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private static bool AlreadyExists(List<Name> names, Func<Name, bool> comparer)
        {
            var existing = names.FirstOrDefault(comparer);

            if (existing != null)
            {
                if (MessageBox.Show($"{existing.LongName} already exists as {existing.ShortName}", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return true;
                }
            }

            return false;
        }

        private void OnShortNameTextBoxTextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LongNameTextBox.Text))
            {
                LongNameTextBox.Text = ShortNameTextBox.Text;
            }
            else if (ShortNameTextBox.Text.IndexOf(LongNameTextBox.Text) == 0)
            {
                LongNameTextBox.Text = ShortNameTextBox.Text;
            }
            else if (LongNameTextBox.Text.Length > ShortNameTextBox.Text.Length)
            {
                var longName = LongNameTextBox.Text.Substring(0, ShortNameTextBox.Text.Length);

                if (ShortNameTextBox.Text.IndexOf(longName) == 0)
                {
                    LongNameTextBox.Text = ShortNameTextBox.Text;
                }
            }
        }

        private void OnSearchTvdbLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var uriBuilder = new UriBuilder("https://thetvdb.com/search");

            var query = HttpUtility.ParseQueryString(uriBuilder.ToString());

            query.Add("query", LongNameTextBox.Text);

            uriBuilder.Query = query.ToString();

            Process.Start(uriBuilder.ToString());
        }

        private void OnPasteIDButtonClick(object sender, EventArgs e)
        {
            try
            {
                var id = Clipboard.GetText();

                UrlTextBox.Text = $"https://thetvdb.com/?tab=series&id={id}";
            }
            catch
            {

            }
        }
    }
}