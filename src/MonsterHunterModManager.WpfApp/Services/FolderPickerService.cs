using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Dialogs;
using MonsterHunterModManager.BlazorApp.Services;

namespace MonsterHunterModManager.WpfApp.Services;

public class FolderPickerService : IFolderPickerService
{
    public async Task<string> PickFolder(string title, string basePath = "")
    {
        var dialog = new CommonOpenFileDialog();
        
        dialog.IsFolderPicker = true;
        dialog.Title = title;
        
        if (!string.IsNullOrEmpty(basePath))
            dialog.DefaultDirectory = basePath;

        return dialog.ShowDialog() == CommonFileDialogResult.Ok ? dialog.FileName : "";
    }
}