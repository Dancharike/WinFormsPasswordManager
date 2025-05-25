using PasswordManager.Core.Models;

namespace PasswordManager.Core.Interfaces;

public interface IStorageService
{
    List<PasswordEntry> Load();
    void Save(List<PasswordEntry> entries);
}