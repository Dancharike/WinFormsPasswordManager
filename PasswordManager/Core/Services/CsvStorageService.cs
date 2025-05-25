using System.Text;
using PasswordManager.Core.Interfaces;
using PasswordManager.Core.Models;

namespace PasswordManager.Core.Services;

public class CsvStorageService : IStorageService
{
    private readonly string _filePath;

    public CsvStorageService(string filePath)
    {
        _filePath = filePath;
    }

    public List<PasswordEntry> Load()
    {
        var entries = new List<PasswordEntry>();
        if (!File.Exists(_filePath)) return entries;

        var lines = File.ReadAllLines(_filePath, Encoding.UTF8);
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            if (parts.Length >= 4)
            {
                entries.Add(new PasswordEntry
                {
                    Title = parts[0],
                    EncryptedPassword = parts[1],
                    Url = parts[2],
                    Notes = parts[3]
                });
            }
        }

        return entries;
    }

    public void Save(List<PasswordEntry> entries)
    {
        var lines = entries.Select(e => $"{e.Title},{e.EncryptedPassword},{e.Url},{e.Notes}");
        File.WriteAllLines(_filePath, lines, Encoding.UTF8);
    }
}