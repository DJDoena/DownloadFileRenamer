namespace DoenaSoft.DownloadRenamer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button SelectFileButton;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Button ProcessButton;
            System.Windows.Forms.Button RenameButton;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Button PasteTitleButton;
            System.Windows.Forms.Button NewSeriesButton;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Button PasteTvdbIdButton;
            System.Windows.Forms.Button ParseHtmlButton;
            this.SourceFileTextBox = new System.Windows.Forms.TextBox();
            this.SeriesNameComboBox = new System.Windows.Forms.ComboBox();
            this.EpisodeNumberTextBox = new System.Windows.Forms.TextBox();
            this.AirDateTextBox = new System.Windows.Forms.TextBox();
            this.ResolutionTextBox = new System.Windows.Forms.TextBox();
            this.ExtensionTextBox = new System.Windows.Forms.TextBox();
            this.CrypticNameTextBox = new System.Windows.Forms.TextBox();
            this.TargetFileNameTextBox = new System.Windows.Forms.TextBox();
            this.FullEpisodeNameTextBox = new System.Windows.Forms.TextBox();
            this.EpisodeNameTextBox = new System.Windows.Forms.TextBox();
            this.EpisodeNamesLinkTextBox = new System.Windows.Forms.TextBox();
            this.EpisodeNamesLinkLabel = new System.Windows.Forms.LinkLabel();
            this.TvdbIdTextBox = new System.Windows.Forms.TextBox();
            this.DeCheckbox = new System.Windows.Forms.CheckBox();
            SelectFileButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ProcessButton = new System.Windows.Forms.Button();
            RenameButton = new System.Windows.Forms.Button();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            PasteTitleButton = new System.Windows.Forms.Button();
            NewSeriesButton = new System.Windows.Forms.Button();
            label11 = new System.Windows.Forms.Label();
            PasteTvdbIdButton = new System.Windows.Forms.Button();
            ParseHtmlButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SelectFileButton
            // 
            SelectFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            SelectFileButton.Location = new System.Drawing.Point(648, 12);
            SelectFileButton.Name = "SelectFileButton";
            SelectFileButton.Size = new System.Drawing.Size(75, 23);
            SelectFileButton.TabIndex = 2;
            SelectFileButton.Text = "...";
            SelectFileButton.UseVisualStyleBackColor = true;
            SelectFileButton.Click += new System.EventHandler(this.OnSelectFileButtonClicked);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 17);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(89, 13);
            label1.TabIndex = 0;
            label1.Text = "Source file name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 82);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(66, 13);
            label2.TabIndex = 6;
            label2.Text = "Show name:";
            // 
            // ProcessButton
            // 
            ProcessButton.Location = new System.Drawing.Point(119, 264);
            ProcessButton.Name = "ProcessButton";
            ProcessButton.Size = new System.Drawing.Size(75, 23);
            ProcessButton.TabIndex = 22;
            ProcessButton.Text = "Process";
            ProcessButton.UseVisualStyleBackColor = true;
            ProcessButton.Click += new System.EventHandler(this.OnProcessButtonClicked);
            // 
            // RenameButton
            // 
            RenameButton.Location = new System.Drawing.Point(119, 345);
            RenameButton.Name = "RenameButton";
            RenameButton.Size = new System.Drawing.Size(75, 23);
            RenameButton.TabIndex = 27;
            RenameButton.Text = "&Rename";
            RenameButton.UseVisualStyleBackColor = true;
            RenameButton.Click += new System.EventHandler(this.OnRenameButtonClick);
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 137);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(86, 13);
            label3.TabIndex = 12;
            label3.Text = "Episode number:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(12, 163);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(46, 13);
            label4.TabIndex = 14;
            label4.Text = "Air date:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(12, 189);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(60, 13);
            label5.TabIndex = 16;
            label5.Text = "Resolution:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(12, 215);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(56, 13);
            label6.TabIndex = 18;
            label6.Text = "Extension:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(12, 241);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(87, 13);
            label7.TabIndex = 20;
            label7.Text = "Download name:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(12, 296);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(86, 13);
            label8.TabIndex = 23;
            label8.Text = "Target file name:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(12, 322);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(95, 13);
            label9.TabIndex = 25;
            label9.Text = "Full episode name:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(12, 111);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(77, 13);
            label10.TabIndex = 9;
            label10.Text = "Episode name:";
            // 
            // PasteTitleButton
            // 
            PasteTitleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            PasteTitleButton.Location = new System.Drawing.Point(648, 106);
            PasteTitleButton.Name = "PasteTitleButton";
            PasteTitleButton.Size = new System.Drawing.Size(75, 23);
            PasteTitleButton.TabIndex = 11;
            PasteTitleButton.Text = "Paste";
            PasteTitleButton.UseVisualStyleBackColor = true;
            PasteTitleButton.Click += new System.EventHandler(this.OnPasteTitleButtonClicked);
            // 
            // NewSeriesButton
            // 
            NewSeriesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            NewSeriesButton.Location = new System.Drawing.Point(648, 77);
            NewSeriesButton.Name = "NewSeriesButton";
            NewSeriesButton.Size = new System.Drawing.Size(75, 23);
            NewSeriesButton.TabIndex = 8;
            NewSeriesButton.Text = "New Show";
            NewSeriesButton.UseVisualStyleBackColor = true;
            NewSeriesButton.Click += new System.EventHandler(this.OnNewSeriesButtonClick);
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(12, 56);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(101, 13);
            label11.TabIndex = 4;
            label11.Text = "Episode names link:";
            // 
            // PasteTvdbIdButton
            // 
            PasteTvdbIdButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            PasteTvdbIdButton.Location = new System.Drawing.Point(648, 132);
            PasteTvdbIdButton.Name = "PasteTvdbIdButton";
            PasteTvdbIdButton.Size = new System.Drawing.Size(75, 23);
            PasteTvdbIdButton.TabIndex = 29;
            PasteTvdbIdButton.Text = "Paste";
            PasteTvdbIdButton.UseVisualStyleBackColor = true;
            PasteTvdbIdButton.Click += new System.EventHandler(this.OnPasteTvdbIdButtonClick);
            // 
            // ParseHtmlButton
            // 
            ParseHtmlButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            ParseHtmlButton.Location = new System.Drawing.Point(729, 106);
            ParseHtmlButton.Name = "ParseHtmlButton";
            ParseHtmlButton.Size = new System.Drawing.Size(50, 47);
            ParseHtmlButton.TabIndex = 30;
            ParseHtmlButton.Text = "Parse &HTML";
            ParseHtmlButton.UseVisualStyleBackColor = true;
            ParseHtmlButton.Click += new System.EventHandler(this.OnParseHtmlButtonClick);
            // 
            // SourceFileTextBox
            // 
            this.SourceFileTextBox.Location = new System.Drawing.Point(119, 14);
            this.SourceFileTextBox.Name = "SourceFileTextBox";
            this.SourceFileTextBox.ReadOnly = true;
            this.SourceFileTextBox.Size = new System.Drawing.Size(523, 20);
            this.SourceFileTextBox.TabIndex = 1;
            this.SourceFileTextBox.TextChanged += new System.EventHandler(this.OnSourceFileTextBoxTextChanged);
            // 
            // SeriesNameComboBox
            // 
            this.SeriesNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SeriesNameComboBox.FormattingEnabled = true;
            this.SeriesNameComboBox.Location = new System.Drawing.Point(119, 79);
            this.SeriesNameComboBox.Name = "SeriesNameComboBox";
            this.SeriesNameComboBox.Size = new System.Drawing.Size(523, 21);
            this.SeriesNameComboBox.TabIndex = 7;
            this.SeriesNameComboBox.SelectedIndexChanged += new System.EventHandler(this.OnSeriesNameComboBoxSelectedIndexChanged);
            // 
            // EpisodeNumberTextBox
            // 
            this.EpisodeNumberTextBox.Location = new System.Drawing.Point(119, 134);
            this.EpisodeNumberTextBox.Name = "EpisodeNumberTextBox";
            this.EpisodeNumberTextBox.Size = new System.Drawing.Size(237, 20);
            this.EpisodeNumberTextBox.TabIndex = 13;
            this.EpisodeNumberTextBox.TextChanged += new System.EventHandler(this.OnEpisodeNumberTextBoxTextChanged);
            // 
            // AirDateTextBox
            // 
            this.AirDateTextBox.Location = new System.Drawing.Point(119, 160);
            this.AirDateTextBox.Name = "AirDateTextBox";
            this.AirDateTextBox.Size = new System.Drawing.Size(523, 20);
            this.AirDateTextBox.TabIndex = 15;
            this.AirDateTextBox.TextChanged += new System.EventHandler(this.OnAirDateTextBoxTextChanged);
            // 
            // ResolutionTextBox
            // 
            this.ResolutionTextBox.Location = new System.Drawing.Point(119, 186);
            this.ResolutionTextBox.Name = "ResolutionTextBox";
            this.ResolutionTextBox.Size = new System.Drawing.Size(523, 20);
            this.ResolutionTextBox.TabIndex = 17;
            this.ResolutionTextBox.TextChanged += new System.EventHandler(this.OnResolutionTextBoxTextChanged);
            // 
            // ExtensionTextBox
            // 
            this.ExtensionTextBox.Location = new System.Drawing.Point(119, 212);
            this.ExtensionTextBox.Name = "ExtensionTextBox";
            this.ExtensionTextBox.Size = new System.Drawing.Size(523, 20);
            this.ExtensionTextBox.TabIndex = 19;
            this.ExtensionTextBox.TextChanged += new System.EventHandler(this.OnExtensionTextBoxTextChanged);
            // 
            // CrypticNameTextBox
            // 
            this.CrypticNameTextBox.Location = new System.Drawing.Point(119, 238);
            this.CrypticNameTextBox.Name = "CrypticNameTextBox";
            this.CrypticNameTextBox.Size = new System.Drawing.Size(523, 20);
            this.CrypticNameTextBox.TabIndex = 21;
            // 
            // TargetFileNameTextBox
            // 
            this.TargetFileNameTextBox.Location = new System.Drawing.Point(119, 293);
            this.TargetFileNameTextBox.Name = "TargetFileNameTextBox";
            this.TargetFileNameTextBox.Size = new System.Drawing.Size(523, 20);
            this.TargetFileNameTextBox.TabIndex = 24;
            this.TargetFileNameTextBox.TextChanged += new System.EventHandler(this.OnTargetFileNameTextBoxTextChanged);
            // 
            // FullEpisodeNameTextBox
            // 
            this.FullEpisodeNameTextBox.Location = new System.Drawing.Point(119, 319);
            this.FullEpisodeNameTextBox.Name = "FullEpisodeNameTextBox";
            this.FullEpisodeNameTextBox.Size = new System.Drawing.Size(523, 20);
            this.FullEpisodeNameTextBox.TabIndex = 26;
            this.FullEpisodeNameTextBox.TextChanged += new System.EventHandler(this.OnFullEpisodeNameTextBoxTextChanged);
            // 
            // EpisodeNameTextBox
            // 
            this.EpisodeNameTextBox.Location = new System.Drawing.Point(119, 108);
            this.EpisodeNameTextBox.Name = "EpisodeNameTextBox";
            this.EpisodeNameTextBox.Size = new System.Drawing.Size(523, 20);
            this.EpisodeNameTextBox.TabIndex = 10;
            this.EpisodeNameTextBox.TextChanged += new System.EventHandler(this.OnEpisodeNameTextBoxTextChanged);
            // 
            // EpisodeNamesLinkTextBox
            // 
            this.EpisodeNamesLinkTextBox.Location = new System.Drawing.Point(119, 53);
            this.EpisodeNamesLinkTextBox.Name = "EpisodeNamesLinkTextBox";
            this.EpisodeNamesLinkTextBox.Size = new System.Drawing.Size(523, 20);
            this.EpisodeNamesLinkTextBox.TabIndex = 5;
            // 
            // EpisodeNamesLinkLabel
            // 
            this.EpisodeNamesLinkLabel.AutoSize = true;
            this.EpisodeNamesLinkLabel.Location = new System.Drawing.Point(116, 37);
            this.EpisodeNamesLinkLabel.Name = "EpisodeNamesLinkLabel";
            this.EpisodeNamesLinkLabel.Size = new System.Drawing.Size(55, 13);
            this.EpisodeNamesLinkLabel.TabIndex = 3;
            this.EpisodeNamesLinkLabel.TabStop = true;
            this.EpisodeNamesLinkLabel.Text = "linkLabel1";
            this.EpisodeNamesLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnEpisodeNamesLinkLabelLinkClicked);
            // 
            // TvdbIdTextBox
            // 
            this.TvdbIdTextBox.Location = new System.Drawing.Point(362, 134);
            this.TvdbIdTextBox.Name = "TvdbIdTextBox";
            this.TvdbIdTextBox.Size = new System.Drawing.Size(280, 20);
            this.TvdbIdTextBox.TabIndex = 28;
            this.TvdbIdTextBox.TextChanged += new System.EventHandler(this.OnTvdbIdTextBoxTextChanged);
            // 
            // DeCheckbox
            // 
            this.DeCheckbox.AutoSize = true;
            this.DeCheckbox.Location = new System.Drawing.Point(648, 292);
            this.DeCheckbox.Name = "DeCheckbox";
            this.DeCheckbox.Size = new System.Drawing.Size(41, 17);
            this.DeCheckbox.TabIndex = 31;
            this.DeCheckbox.Text = ".de";
            this.DeCheckbox.UseVisualStyleBackColor = true;
            this.DeCheckbox.CheckedChanged += new System.EventHandler(this.OnDeCheckboxCheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 386);
            this.Controls.Add(this.DeCheckbox);
            this.Controls.Add(ParseHtmlButton);
            this.Controls.Add(PasteTvdbIdButton);
            this.Controls.Add(this.TvdbIdTextBox);
            this.Controls.Add(this.EpisodeNamesLinkLabel);
            this.Controls.Add(label11);
            this.Controls.Add(this.EpisodeNamesLinkTextBox);
            this.Controls.Add(NewSeriesButton);
            this.Controls.Add(PasteTitleButton);
            this.Controls.Add(label10);
            this.Controls.Add(this.EpisodeNameTextBox);
            this.Controls.Add(label9);
            this.Controls.Add(label8);
            this.Controls.Add(label7);
            this.Controls.Add(label6);
            this.Controls.Add(label5);
            this.Controls.Add(label4);
            this.Controls.Add(label3);
            this.Controls.Add(RenameButton);
            this.Controls.Add(this.FullEpisodeNameTextBox);
            this.Controls.Add(this.TargetFileNameTextBox);
            this.Controls.Add(ProcessButton);
            this.Controls.Add(this.CrypticNameTextBox);
            this.Controls.Add(this.ExtensionTextBox);
            this.Controls.Add(this.ResolutionTextBox);
            this.Controls.Add(this.AirDateTextBox);
            this.Controls.Add(this.EpisodeNumberTextBox);
            this.Controls.Add(label2);
            this.Controls.Add(this.SeriesNameComboBox);
            this.Controls.Add(label1);
            this.Controls.Add(this.SourceFileTextBox);
            this.Controls.Add(SelectFileButton);
            this.MinimumSize = new System.Drawing.Size(800, 425);
            this.Name = "MainForm";
            this.Text = "Download File Renamer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox SourceFileTextBox;
        private System.Windows.Forms.ComboBox SeriesNameComboBox;
        private System.Windows.Forms.TextBox EpisodeNumberTextBox;
        private System.Windows.Forms.TextBox AirDateTextBox;
        private System.Windows.Forms.TextBox ResolutionTextBox;
        private System.Windows.Forms.TextBox ExtensionTextBox;
        private System.Windows.Forms.TextBox CrypticNameTextBox;
        private System.Windows.Forms.TextBox TargetFileNameTextBox;
        private System.Windows.Forms.TextBox FullEpisodeNameTextBox;
        private System.Windows.Forms.TextBox EpisodeNameTextBox;
        private System.Windows.Forms.TextBox EpisodeNamesLinkTextBox;
        private System.Windows.Forms.LinkLabel EpisodeNamesLinkLabel;
        private System.Windows.Forms.TextBox TvdbIdTextBox;
        private System.Windows.Forms.CheckBox DeCheckbox;
    }
}

