namespace DoenaSoft.DownloadRenamer
{
    partial class NewSeriesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewSeriesForm));
            this.ShortNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.UrlTextBox = new System.Windows.Forms.TextBox();
            this.OriginalLanguageTextBox = new System.Windows.Forms.TextBox();
            this.DisplayNameTextBox = new System.Windows.Forms.TextBox();
            this.LocalizedNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.LongNameTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.YearUpDown = new System.Windows.Forms.NumericUpDown();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SortNameTextBox = new System.Windows.Forms.TextBox();
            this.SearchTvdbLinkLabel = new System.Windows.Forms.LinkLabel();
            this.PasteIDButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.YearUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // ShortNameTextBox
            // 
            this.ShortNameTextBox.Location = new System.Drawing.Point(113, 14);
            this.ShortNameTextBox.Name = "ShortNameTextBox";
            this.ShortNameTextBox.Size = new System.Drawing.Size(523, 20);
            this.ShortNameTextBox.TabIndex = 1;
            this.ShortNameTextBox.TextChanged += new System.EventHandler(this.OnShortNameTextBoxTextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Short name:";
            // 
            // UrlTextBox
            // 
            this.UrlTextBox.Location = new System.Drawing.Point(113, 66);
            this.UrlTextBox.Name = "UrlTextBox";
            this.UrlTextBox.Size = new System.Drawing.Size(523, 20);
            this.UrlTextBox.TabIndex = 5;
            // 
            // OriginalLanguageTextBox
            // 
            this.OriginalLanguageTextBox.Location = new System.Drawing.Point(113, 92);
            this.OriginalLanguageTextBox.Name = "OriginalLanguageTextBox";
            this.OriginalLanguageTextBox.Size = new System.Drawing.Size(523, 20);
            this.OriginalLanguageTextBox.TabIndex = 7;
            this.OriginalLanguageTextBox.Text = "eng";
            // 
            // DisplayNameTextBox
            // 
            this.DisplayNameTextBox.Location = new System.Drawing.Point(113, 144);
            this.DisplayNameTextBox.Name = "DisplayNameTextBox";
            this.DisplayNameTextBox.Size = new System.Drawing.Size(523, 20);
            this.DisplayNameTextBox.TabIndex = 11;
            // 
            // LocalizedNameTextBox
            // 
            this.LocalizedNameTextBox.Location = new System.Drawing.Point(113, 170);
            this.LocalizedNameTextBox.Name = "LocalizedNameTextBox";
            this.LocalizedNameTextBox.Size = new System.Drawing.Size(523, 20);
            this.LocalizedNameTextBox.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "URL:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Original language:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Display name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 173);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Localized name:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 198);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Year:";
            // 
            // LongNameTextBox
            // 
            this.LongNameTextBox.Location = new System.Drawing.Point(113, 40);
            this.LongNameTextBox.Name = "LongNameTextBox";
            this.LongNameTextBox.Size = new System.Drawing.Size(523, 20);
            this.LongNameTextBox.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Long name:";
            // 
            // YearUpDown
            // 
            this.YearUpDown.Location = new System.Drawing.Point(113, 196);
            this.YearUpDown.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.YearUpDown.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.YearUpDown.Name = "YearUpDown";
            this.YearUpDown.Size = new System.Drawing.Size(523, 20);
            this.YearUpDown.TabIndex = 15;
            this.YearUpDown.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(113, 223);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 16;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.OnSaveButtonClick);
            // 
            // CloseButton
            // 
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(194, 223);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 17;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.OnCloseButtonClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Sort name:";
            // 
            // SortNameTextBox
            // 
            this.SortNameTextBox.Location = new System.Drawing.Point(113, 118);
            this.SortNameTextBox.Name = "SortNameTextBox";
            this.SortNameTextBox.Size = new System.Drawing.Size(523, 20);
            this.SortNameTextBox.TabIndex = 9;
            // 
            // SearchTvdbLinkLabel
            // 
            this.SearchTvdbLinkLabel.AutoSize = true;
            this.SearchTvdbLinkLabel.Location = new System.Drawing.Point(642, 43);
            this.SearchTvdbLinkLabel.Name = "SearchTvdbLinkLabel";
            this.SearchTvdbLinkLabel.Size = new System.Drawing.Size(88, 13);
            this.SearchTvdbLinkLabel.TabIndex = 18;
            this.SearchTvdbLinkLabel.TabStop = true;
            this.SearchTvdbLinkLabel.Text = "Search on TVDB";
            this.SearchTvdbLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnSearchTvdbLinkLabelLinkClicked);
            // 
            // PasteIDButton
            // 
            this.PasteIDButton.Location = new System.Drawing.Point(642, 64);
            this.PasteIDButton.Name = "PasteIDButton";
            this.PasteIDButton.Size = new System.Drawing.Size(88, 23);
            this.PasteIDButton.TabIndex = 19;
            this.PasteIDButton.Text = "Paste ID";
            this.PasteIDButton.UseVisualStyleBackColor = true;
            this.PasteIDButton.Click += new System.EventHandler(this.OnPasteIDButtonClick);
            // 
            // NewSeriesForm
            // 
            this.AcceptButton = this.SaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.PasteIDButton);
            this.Controls.Add(this.SearchTvdbLinkLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SortNameTextBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.YearUpDown);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.LongNameTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LocalizedNameTextBox);
            this.Controls.Add(this.DisplayNameTextBox);
            this.Controls.Add(this.OriginalLanguageTextBox);
            this.Controls.Add(this.UrlTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ShortNameTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "NewSeriesForm";
            this.Text = "Create New Series";
            ((System.ComponentModel.ISupportInitialize)(this.YearUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ShortNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UrlTextBox;
        private System.Windows.Forms.TextBox OriginalLanguageTextBox;
        private System.Windows.Forms.TextBox DisplayNameTextBox;
        private System.Windows.Forms.TextBox LocalizedNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox LongNameTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown YearUpDown;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SortNameTextBox;
        private System.Windows.Forms.LinkLabel SearchTvdbLinkLabel;
        private System.Windows.Forms.Button PasteIDButton;
    }
}

