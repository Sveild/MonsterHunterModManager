namespace MonsterHunterModManager.BlazorApp.Services;

public interface IFolderPickerService
{
    Task<string> PickFolder(string title, string basePath = "");
}