using Microsoft.WindowsAPICodePack.Dialogs;
using MonsterHunterModManager.Application.Common.Interfaces;

namespace MonsterHunterModManager.Infrastructure.Services;

public class PickerService : IPickerService
{
    public string PickFolder(string title, string basePath = "")
    {
        var dialog = new CommonOpenFileDialog();
        
        dialog.IsFolderPicker = true;
        dialog.Title = title;
        
        if (!string.IsNullOrEmpty(basePath))
            dialog.DefaultDirectory = basePath;

        return dialog.ShowDialog() == CommonFileDialogResult.Ok ? dialog.FileName : "";
    }

    public string PickZipFile(string title, string basePath = "")
    {
        var dialog = new CommonOpenFileDialog();

        dialog.Multiselect = false;
        dialog.Title = title;
        dialog.Filters.Add(new CommonFileDialogFilter("*.zip", ".zip"));
        
        if (!string.IsNullOrEmpty(basePath))
            dialog.DefaultDirectory = basePath;

        return dialog.ShowDialog() == CommonFileDialogResult.Ok ? dialog.FileNames.First() : string.Empty;
    }
    
    public IEnumerable<string> PickZipFiles(string title, string basePath = "")
    {
        var dialog = new CommonOpenFileDialog();

        dialog.Multiselect = true;
        dialog.Title = title;
        dialog.Filters.Add(new CommonFileDialogFilter("*.zip", ".zip"));
        
        if (!string.IsNullOrEmpty(basePath))
            dialog.DefaultDirectory = basePath;

        return dialog.ShowDialog() == CommonFileDialogResult.Ok ? dialog.FileNames : new List<string>();
    }
}