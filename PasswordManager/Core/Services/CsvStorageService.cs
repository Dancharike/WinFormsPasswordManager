using System.Text;
using PasswordManager.Core.Interfaces;
using PasswordManager.Core.Models;

namespace PasswordManager.Core.Services;

/// <summary>
/// Service that loads and saves password entries to a CSV file.
/// Implements IStorageService.
/// </summary>
public class CsvStorageService : IStorageService
{
    private readonly string _filePath; // path to the CSV file

    /// <summary>
    /// Initializes the storage service with a file path.
    /// </summary>
    public CsvStorageService(string filePath)
    {
        _filePath = filePath;
    }

    /// <summary>
    /// Loads all password entries from the CSV file.
    /// </summary>
    public List<PasswordEntry> Load()
    {
        var entries = new List<PasswordEntry>();
        if (!File.Exists(_filePath)) return entries;

        // read all lines and split by comma
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

    /// <summary>
    /// Saves a list of password entries into the CSV file.
    /// </summary>
    public void Save(List<PasswordEntry> entries)
    {
        // convert each entry to a CSV-formatted string
        var lines = entries.Select(e => $"{e.Title},{e.EncryptedPassword},{e.Url},{e.Notes}");
        File.WriteAllLines(_filePath, lines, Encoding.UTF8);
    }
}