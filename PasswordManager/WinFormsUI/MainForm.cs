using PasswordManager.Core.Interfaces;
using PasswordManager.Core.Models;
using PasswordManager.Core.Services;

namespace PasswordManager.WinFormsUI;

/// <summary>
/// Main application window for managing encrypted passwords.
/// Handles file decryption/encryption, entry management, and UI interaction.
/// </summary>
public partial class MainForm : Form
{
    private readonly string _username;
    private readonly string _userPassword;
    private readonly IEncryptionService _encryptionService;
    private readonly IStorageService _storageService;
    private List<PasswordEntry> _entries = new();

    // path to an encrypted file bound to the user
    private string EncryptedFile => $"{_username}.dat";

    // temporary file to store decrypted password data during the session
    private string TempFile => $"{_username}.csv";

    /// <summary>
    /// Initializes the main form, decrypts user file if present, and loads existing entries.
    /// </summary>
    public MainForm(string username, string userPassword)
    {
        InitializeComponent();

        _username = username;
        _userPassword = userPassword;
        _encryptionService = new AesEncryptionService(userPassword);

        // decrypt user's file if exists
        if (File.Exists(EncryptedFile))
        {
            try
            {
                var encryptedBytes = File.ReadAllBytes(EncryptedFile);
                var decryptedCsv = _encryptionService.Decrypt(encryptedBytes);
                File.WriteAllText(TempFile, decryptedCsv);
            }
            catch
            {
                MessageBox.Show("Failed to decrypt the password database. The file may be corrupted.");
                Environment.Exit(1);
            }
        }

        _storageService = new CsvStorageService(TempFile);
        LoadData();
        RefreshList();
    }

    /// <summary>
    /// Loads password entries from the storage service.
    /// </summary>
    private void LoadData()
    {
        _entries = _storageService.Load();
    }

    /// <summary>
    /// Saves the current list of password entries to file.
    /// </summary>
    private void SaveData()
    {
        _storageService.Save(_entries);
    }

    /// <summary>
    /// Refreshes the listbox UI with current entry titles.
    /// </summary>
    private void RefreshList()
    {
        listBoxEntries.Items.Clear();
        foreach (var entry in _entries)
        {
            listBoxEntries.Items.Add(entry.Title);
        }
    }

    /// <summary>
    /// Adds a new password entry with encryption.
    /// </summary>
    private void btnAdd_Click(object sender, EventArgs e)
    {
        var password = txtPassword.Text;
        var encrypted = Convert.ToBase64String(_encryptionService.Encrypt(password));

        var entry = new PasswordEntry
        {
            Title = txtTitle.Text,
            Url = txtUrl.Text,
            Notes = txtNotes.Text,
            EncryptedPassword = encrypted
        };

        _entries.Add(entry);
        RefreshList();
        SaveData();
    }

    /// <summary>
    /// Deletes the selected password entry by title.
    /// </summary>
    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (listBoxEntries.SelectedIndex < 0) return;

        var title = listBoxEntries.SelectedItem.ToString();
        var entry = _entries.FirstOrDefault(e => e.Title == title);

        if (entry != null)
        {
            _entries.Remove(entry);
            RefreshList();
            SaveData();
        }
    }

    /// <summary>
    /// Searches for an entry by title and populates UI fields.
    /// </summary>
    private void btnSearch_Click(object sender, EventArgs e)
    {
        var query = txtSearch.Text.ToLower();
        var found = _entries.FirstOrDefault(e => e.Title.ToLower() == query);

        if (found != null)
        {
            txtTitle.Text = found.Title;
            txtUrl.Text = found.Url;
            txtNotes.Text = found.Notes;
            txtPassword.Text = ""; // don't show password by default
        }
        else
        {
            MessageBox.Show("Entry not found.");
        }
    }

    /// <summary>
    /// Decrypts and displays the password of the current entry.
    /// </summary>
    private void btnShowPassword_Click(object sender, EventArgs e)
    {
        var title = txtTitle.Text;
        var entry = _entries.FirstOrDefault(e => e.Title == title);
        if (entry != null)
        {
            var decrypted = _encryptionService.Decrypt(Convert.FromBase64String(entry.EncryptedPassword));
            txtPassword.Text = decrypted;
        }
    }

    /// <summary>
    /// Generates a random password based on selected criteria.
    /// </summary>
    private void btnGenerate_Click(object sender, EventArgs e)
    {
        int length = (int)numLength.Value;
        var chars = new List<char>();

        if (chkUpper.Checked)
            chars.AddRange("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        if (chkLower.Checked)
            chars.AddRange("abcdefghijklmnopqrstuvwxyz");
        if (chkDigits.Checked)
            chars.AddRange("0123456789");
        if (chkSymbols.Checked)
            chars.AddRange("!@#$%^&*()_+-=[]{}|;:,.<>?");

        if (chars.Count == 0)
        {
            MessageBox.Show("Please select at least one character type.");
            return;
        }

        var random = new Random();
        var password = new string(Enumerable.Range(0, length)
            .Select(_ => chars[random.Next(chars.Count)]).ToArray());

        txtPassword.Text = password;
    }

    /// <summary>
    /// Copies the current password to the clipboard.
    /// </summary>
    private void btnCopy_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            Clipboard.SetText(txtPassword.Text);
            MessageBox.Show("Password copied to clipboard.");
        }
    }

    /// <summary>
    /// Encrypts and saves data on form closing. Deletes temporary CSV.
    /// </summary>
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        SaveData();

        try
        {
            var plainCsv = File.ReadAllText(TempFile);
            var encryptedBytes = _encryptionService.Encrypt(plainCsv);
            File.WriteAllBytes(EncryptedFile, encryptedBytes);
            File.Delete(TempFile);
        }
        catch
        {
            MessageBox.Show("Failed to encrypt and save the password database.");
        }
    }
}
