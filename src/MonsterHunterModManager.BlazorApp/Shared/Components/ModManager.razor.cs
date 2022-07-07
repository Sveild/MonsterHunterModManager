using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MonsterHunterModManager.BlazorApp.Data;
using MonsterHunterModManager.BlazorApp.Services;
using MonsterHunterModManager.BlazorApp.Shared.Dialog;
using MudBlazor;
using Newtonsoft.Json;

namespace MonsterHunterModManager.BlazorApp.Shared.Components
{
    public partial class ModManager
    {
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private ISettingsService SettingsService { get; set; }
        [Inject] private IPhysicalFileService PhysicalFileService { get; set; }

        [Parameter] public Games Game { get; set; }

        private bool _uploading;
        private bool _initialized;
        private GameSettings _settings;
        private HashSet<Mod> _selectedMods;

        protected override async Task OnInitializedAsync()
        {
            _settings = SettingsService.GetGameSettings(Game);
            await base.OnInitializedAsync();
            _initialized = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            
            if (string.IsNullOrEmpty(_settings.GameDirectory))
                await OpenSettingsDialog(false);
        }

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            _uploading = true;
            var files = e.GetMultipleFiles();
            
            foreach(var file in files)
            {
                await PhysicalFileService.UploadFile(_settings, file);
                
                var mod = new Mod
                {
                    ShowDetails = false,
                    FileName = file.Name,
                };

                mod.Enabled = PhysicalFileService.IsModeEnabled(_settings, mod);
                _settings.Mods.Add(mod);
            }
            
            
            SettingsService.SaveGameSettings(_settings);
            _uploading = false;
        }

        private void RemoveSelectedMods()
        {
            if (_selectedMods == null)
                return;
            
            foreach (var mod in _selectedMods)
            {
                PhysicalFileService.DisableMod(_settings, mod);
                PhysicalFileService.DeleteModFile(_settings, mod);
                _settings.Mods.Remove(mod);
            }
            
            SettingsService.SaveGameSettings(_settings);
        }

        private void Refresh()
        {
            _settings = SettingsService.GetGameSettings(Game);
        }

        private async Task OpenSettingsDialog(bool allowCloseModal = true)
        {
            var parameters = new DialogParameters();
            
            parameters.Add(nameof(GameSettingsDialog.Settings), _settings);
            parameters.Add(nameof(GameSettingsDialog.AllowCloseModal), allowCloseModal);
            
            var dialog = DialogService.Show<GameSettingsDialog>("Settings", parameters);
            var result = await dialog.Result;

            if (result.Cancelled)
                return;
            
            _settings = await dialog.GetReturnValueAsync<GameSettings>();
            SettingsService.SaveGameSettings(_settings);
            Refresh();
        }

        private void ToggleMods(bool state)
        {
            if (_selectedMods == null)
                return;

            foreach (var mod in _selectedMods)
            {
                if (state)
                    PhysicalFileService.EnableMod(_settings, mod);
                else
                    PhysicalFileService.DisableMod(_settings, mod);
                
                mod.Enabled = state;
            }

            SettingsService.SaveGameSettings(_settings);
        }
    }
}