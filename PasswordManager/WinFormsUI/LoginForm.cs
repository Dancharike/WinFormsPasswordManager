using System.Security.Cryptography;
using PasswordManager.Core.Services;

namespace PasswordManager.WinFormsUI;

public partial class LoginForm : Form
{
    private const string UserFile = "users.csv";

    public LoginForm()
    {
        InitializeComponent();
    }

    private void btnRegister_Click(object sender, EventArgs e)
    {
        var username = txtUsername.Text.Trim();
        var password = txtPassword.Text;

        if (username == "" || password == "")
        {
            MessageBox.Show("Please enter both username and password.");
            return;
        }

        if (UserExists(username))
        {
            MessageBox.Show("User already exists.");
            return;
        }

        var salt = GenerateSalt();
        var hash = HashPassword(password, salt);

        var record = $"{username},{Convert.ToBase64String(hash)},{Convert.ToBase64String(salt)}";
        File.AppendAllLines(UserFile, new[] { record });

        MessageBox.Show("User registered successfully.");
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        var username = txtUsername.Text.Trim();
        var password = txtPassword.Text;

        if (!TryGetUser(username, out var storedHash, out var salt))
        {
            MessageBox.Show("Invalid username or password.");
            return;
        }

        var hash = HashPassword(password, salt);

        if (!storedHash.SequenceEqual(hash))
        {
            MessageBox.Show("Invalid username or password.");
            return;
        }

        // open encrypted file and decrypt it for MainForm
        var dataFile = $"{username}.dat";
        var csvFile = "data.csv"; // temp file used by CsvStorageService

        if (File.Exists(dataFile))
        {
            var encryptionService = new AesEncryptionService(password);
            try
            {
                var encrypted = File.ReadAllBytes(dataFile);
                var decrypted = encryptionService.Decrypt(encrypted);
                File.WriteAllText(csvFile, decrypted);
            }
            catch
            {
                MessageBox.Show("Failed to decrypt password file.");
                return;
            }
        }

        Hide();
        var mainForm = new MainForm(username, password);
        mainForm.FormClosed += (_, _) => Close();
        mainForm.Show();
    }

    private static bool UserExists(string username)
    {
        if (!File.Exists(UserFile)) return false;
        return File.ReadLines(UserFile).Any(line => line.StartsWith(username + ","));
    }

    private static bool TryGetUser(string username, out byte[] hash, out byte[] salt)
    {
        hash = Array.Empty<byte>();
        salt = Array.Empty<byte>();

        if (!File.Exists(UserFile)) return false;

        foreach (var line in File.ReadLines(UserFile))
        {
            var parts = line.Split(',');
            if (parts.Length != 3) continue;
            if (parts[0] != username) continue;

            hash = Convert.FromBase64String(parts[1]);
            salt = Convert.FromBase64String(parts[2]);
            return true;
        }

        return false;
    }

    private static byte[] GenerateSalt()
    {
        var salt = new byte[16];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }

    private static byte[] HashPassword(string password, byte[] salt)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
        return pbkdf2.GetBytes(32);
    }
}
