namespace MonsterHunterModManager.Application.Common.Interfaces;

public interface IPickerService
{
    string PickFolder(string title, string basePath = "");
    IEnumerable<string> PickZipFiles(string title, string basePath = "");
}