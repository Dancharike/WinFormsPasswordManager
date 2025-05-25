namespace PasswordManager.Core.Models;

public class PasswordEntry
{
    public string Title { get; set; } = "";
    public string EncryptedPassword { get; set; } = "";
    public string Url { get; set; } = "";
    public string Notes { get; set; } = "";
}