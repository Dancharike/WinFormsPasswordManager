namespace PasswordManager.WinFormsUI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {

            this.listBoxEntries = new System.Windows.Forms.ListBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnShowPassword = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.numLength = new System.Windows.Forms.NumericUpDown();
            this.chkUpper = new System.Windows.Forms.CheckBox();
            this.chkLower = new System.Windows.Forms.CheckBox();
            this.chkDigits = new System.Windows.Forms.CheckBox();
            this.chkSymbols = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).BeginInit();
            this.SuspendLayout();

            // listBoxEntries
            this.listBoxEntries.FormattingEnabled = true;
            this.listBoxEntries.Location = new System.Drawing.Point(12, 12);
            this.listBoxEntries.Size = new System.Drawing.Size(200, 310);

            // txtTitle
            this.txtTitle.Location = new System.Drawing.Point(230, 12);
            this.txtTitle.Size = new System.Drawing.Size(200, 23);
            this.txtTitle.PlaceholderText = "Title";

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(230, 41);
            this.txtPassword.Size = new System.Drawing.Size(200, 23);
            this.txtPassword.PlaceholderText = "Password";

            // txtUrl
            this.txtUrl.Location = new System.Drawing.Point(230, 70);
            this.txtUrl.Size = new System.Drawing.Size(200, 23);
            this.txtUrl.PlaceholderText = "URL or App";

            // txtNotes
            this.txtNotes.Location = new System.Drawing.Point(230, 99);
            this.txtNotes.Size = new System.Drawing.Size(200, 60);
            this.txtNotes.Multiline = true;
            this.txtNotes.PlaceholderText = "Notes";

            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(230, 170);
            this.txtSearch.Size = new System.Drawing.Size(200, 23);
            this.txtSearch.PlaceholderText = "Search by Title";

            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(230, 210);
            this.btnAdd.Size = new System.Drawing.Size(95, 30);
            this.btnAdd.Text = "Add New";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(335, 210);
            this.btnDelete.Size = new System.Drawing.Size(95, 30);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnSearch
            this.btnSearch.Location = new System.Drawing.Point(230, 250);
            this.btnSearch.Size = new System.Drawing.Size(95, 30);
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // btnShowPassword
            this.btnShowPassword.Location = new System.Drawing.Point(335, 250);
            this.btnShowPassword.Size = new System.Drawing.Size(95, 30);
            this.btnShowPassword.Text = "Show Password";
            this.btnShowPassword.Click += new System.EventHandler(this.btnShowPassword_Click);

            // btnGenerate
            this.btnGenerate.Location = new System.Drawing.Point(230, 290);
            this.btnGenerate.Size = new System.Drawing.Size(95, 30);
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);

            // btnCopy
            this.btnCopy.Location = new System.Drawing.Point(335, 290);
            this.btnCopy.Size = new System.Drawing.Size(95, 30);
            this.btnCopy.Text = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);

            // numLength
            this.numLength.Location = new System.Drawing.Point(450, 12);
            this.numLength.Minimum = 4;
            this.numLength.Maximum = 64;
            this.numLength.Value = 12;
            this.numLength.Size = new System.Drawing.Size(60, 23);

            // chkUpper
            this.chkUpper.Location = new System.Drawing.Point(450, 45);
            this.chkUpper.Size = new System.Drawing.Size(100, 20);
            this.chkUpper.Text = "A-Z (Upper)";
            this.chkUpper.Checked = true;

            // chkLower
            this.chkLower.Location = new System.Drawing.Point(450, 70);
            this.chkLower.Size = new System.Drawing.Size(100, 20);
            this.chkLower.Text = "a-z (Lower)";
            this.chkLower.Checked = true;

            // chkDigits
            this.chkDigits.Location = new System.Drawing.Point(450, 95);
            this.chkDigits.Size = new System.Drawing.Size(100, 20);
            this.chkDigits.Text = "0-9 (Digits)";
            this.chkDigits.Checked = true;

            // chkSymbols
            this.chkSymbols.Location = new System.Drawing.Point(450, 120);
            this.chkSymbols.Size = new System.Drawing.Size(120, 20);
            this.chkSymbols.Text = "!@#... (Symbols)";
            this.chkSymbols.Checked = true;

            // MainForm
            this.ClientSize = new System.Drawing.Size(600, 340);
            this.Controls.Add(this.listBoxEntries);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnShowPassword);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.numLength);
            this.Controls.Add(this.chkUpper);
            this.Controls.Add(this.chkLower);
            this.Controls.Add(this.chkDigits);
            this.Controls.Add(this.chkSymbols);

            this.ResumeLayout(false);
            this.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).EndInit();
        }

        private System.Windows.Forms.ListBox listBoxEntries;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnShowPassword;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.NumericUpDown numLength;
        private System.Windows.Forms.CheckBox chkUpper;
        private System.Windows.Forms.CheckBox chkLower;
        private System.Windows.Forms.CheckBox chkDigits;
        private System.Windows.Forms.CheckBox chkSymbols;
    }
}
