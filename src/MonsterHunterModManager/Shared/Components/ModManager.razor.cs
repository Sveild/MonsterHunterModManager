using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Components;
using MonsterHunterModManager.Application;
using MonsterHunterModManager.Domain;
using MonsterHunterModManager.Application;
using MonsterHunterModManager.Application.Common.Interfaces;
using MonsterHunterModManager.Application.Features.GameSettings.Commands.SaveGameSettings;
using MonsterHunterModManager.Application.Features.GameSettings.Queries.GetGameSettings;
using MonsterHunterModManager.Application.Features.ModSettings.Commands.DisableMods;
using MonsterHunterModManager.Application.Features.ModSettings.Commands.EnableMods;
using MonsterHunterModManager.Application.Features.ModSettings.Commands.RemoveMods;
using MonsterHunterModManager.Application.Features.ModSettings.Commands.UploadMods;
using MonsterHunterModManager.Application.Features.ModSettings.Queries.GetModsSettings;
using MonsterHunterModManager.Domain.Entities;
using MonsterHunterModManager.Domain.Enums;
using MonsterHunterModManager.Shared.Dialog;
using MudBlazor;

namespace MonsterHunterModManager.Shared.Components
{
    public partial class ModManager
    {
        [Inject] private ISender MediatR { get; set; }
        [Inject] private IPickerService PickerService { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private IPhysicalFileService PhysicalFileService { get; set; }

        [Parameter] public Games Game { get; set; }

        private bool _uploading;
        private bool _initialized;
        private GameSettings _settings;
        private List<ModSettings> _modsSettings;
        private HashSet<ModSettings> _selectedModsSettings;

        protected override async Task OnInitializedAsync()
        {
            await Refresh();
            await base.OnInitializedAsync();
            _initialized = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            
            if (string.IsNullOrEmpty(_settings.GameDirectory))
                await SettingsDialog(false);
        }

        private async Task UploadFiles()
        {
            _uploading = true;
            
            var files = PickerService.PickZipFiles("Select mod(s) zip file(s)").ToList();
            
            if (files != null && files.Count > 0)
                _modsSettings = await MediatR.Send(new UploadModsCommand { Game = Game, Files = files });
            
            _uploading = false;
        }

        private async Task RemoveSelectedMods()
        {
            if (_selectedModsSettings == null || _selectedModsSettings.Count == 0)
                return;

            _modsSettings = await MediatR.Send(
                new RemoveModsCommand {Game = Game, ModsSettings = _selectedModsSettings.ToList()}
            );
        }

        private async Task Refresh()
        {
            _settings = await MediatR.Send(new GetGameSettingsQuery {Game = Game});
            _modsSettings = await MediatR.Send(new GetModsSettingsCommand {Game = Game});
        }

        private async Task SettingsDialog(bool allowCloseModal = true)
        {
            var parameters = new DialogParameters();
            
            parameters.Add(nameof(GameSettingsDialog.Settings), _settings);
            parameters.Add(nameof(GameSettingsDialog.AllowCloseModal), allowCloseModal);
            
            var dialog = DialogService.Show<GameSettingsDialog>("Settings", parameters);
            var result = await dialog.Result;

            if (result.Cancelled)
                return;
            
            _settings = await MediatR.Send(
                new SaveGameSettingsCommand {Settings = await dialog.GetReturnValueAsync<GameSettings>()}
            );
        }

        private async Task ToggleMods(bool state)
        {
            if (_selectedModsSettings == null || _selectedModsSettings.Count == 0)
                return;

            if (state)
            {
                _modsSettings = await MediatR.Send(
                    new EnableModsCommand {Game = Game, ModsSettings = _selectedModsSettings.ToList()}
                );
            }
            else
            {
                _modsSettings = await MediatR.Send(
                    new DisableModsCommand {Game = Game, ModsSettings = _selectedModsSettings.ToList()}
                );
            }
        }
    }
}