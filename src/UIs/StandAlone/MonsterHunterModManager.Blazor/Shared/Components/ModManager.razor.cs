using MediatR;
using Microsoft.AspNetCore.Components;
using MonsterHunterModManager.Application.Common.Interfaces;
using MonsterHunterModManager.Application.Features.GameSettings.Commands.SaveGameSettings;
using MonsterHunterModManager.Application.Features.GameSettings.Queries.GetGameSettings;
using MonsterHunterModManager.Application.Features.ModSettings.Commands.ChangeModPositionCommand;
using MonsterHunterModManager.Application.Features.ModSettings.Commands.DisableMods;
using MonsterHunterModManager.Application.Features.ModSettings.Commands.EnableMods;
using MonsterHunterModManager.Application.Features.ModSettings.Commands.RemoveMods;
using MonsterHunterModManager.Application.Features.ModSettings.Commands.UpdateMod;
using MonsterHunterModManager.Application.Features.ModSettings.Commands.UploadMods;
using MonsterHunterModManager.Application.Features.ModSettings.Queries.GetModsSettings;
using MonsterHunterModManager.Blazor.Services;
using MonsterHunterModManager.Blazor.Shared.Dialog;
using MonsterHunterModManager.Domain.Entities;
using MonsterHunterModManager.Domain.Enums;
using MudBlazor;

namespace MonsterHunterModManager.Blazor.Shared.Components
{
    public partial class ModManager
    {
        [Inject] private ISender Mediator { get; set; } = null!;
        [Inject] private IPickerService PickerService { get; set; } = null!;
        [Inject] private IDialogService DialogService { get; set; } = null!;
        [Inject] private IPhysicalFileService PhysicalFileService { get; set; } = null!;
        [Inject] private INxmLinkNotifierService NxmLinkNotifierService { get; set; } = null!;

        [Parameter] public Games Game { get; set; }

        private bool _uploading;
        private bool _initialized;
        private GameSettings _settings = null!;
        private List<ModSettings> _modsSettings = null!;
        private HashSet<ModSettings> _selectedModsSettings = null!;

        protected override async Task OnInitializedAsync()
        {
            await Refresh();
            await base.OnInitializedAsync();

            NxmLinkNotifierService.NotifyNxmLinkEvent += ReceivedLink;
            
            _initialized = true;
        }

        private Task ReceivedLink(string link)
        {
            var parameters = new DialogParameters();
            var options = new DialogOptions {CloseButton = true, CloseOnEscapeKey = true};
            
            parameters.Add(nameof(TestDialog.Link), link);
            DialogService.Show<TestDialog>("test", parameters, options);
            return Task.CompletedTask;
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
                _modsSettings = await Mediator.Send(new UploadModsCommand { Game = Game, Files = files });
            
            _uploading = false;
            await Refresh();
        }

        private async Task RemoveSelectedMods()
        {
            if (_selectedModsSettings == null || _selectedModsSettings.Count == 0)
                return;

            _modsSettings = await Mediator.Send(
                new RemoveModsCommand {Game = Game, ModsSettings = _selectedModsSettings.ToList()}
            );
            
            _selectedModsSettings.Clear();
            await Refresh();
        }

        private async Task Refresh()
        {
            _settings = await Mediator.Send(new GetGameSettingsQuery {Game = Game});
            _modsSettings = (await Mediator.Send(new GetModsSettingsCommand {Game = Game})).OrderBy(ms => ms.Position).ToList();
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
            
            _settings = await Mediator.Send(
                new SaveGameSettingsCommand {Settings = await dialog.GetReturnValueAsync<GameSettings>()}
            );
        }

        private async Task ToggleMods(bool state)
        {
            if (_selectedModsSettings == null || _selectedModsSettings.Count == 0)
                return;

            if (state)
            {
                _modsSettings = await Mediator.Send(
                    new EnableModsCommand {Game = Game, ModsSettings = _selectedModsSettings.ToList()}
                );
            }
            else
            {
                _modsSettings = await Mediator.Send(
                    new DisableModsCommand {Game = Game, ModsSettings = _selectedModsSettings.ToList()}
                );
            }
            
            _selectedModsSettings.Clear();
            await Refresh();
        }

        private async Task UpdateMod(ModSettings modSettings)
        {
            _uploading = true;
            
            var file = PickerService.PickZipFile("Select mod zip file");
            
            if (!string.IsNullOrEmpty(file))
                await Mediator.Send(new UpdateModCommand { ModSettings = modSettings,Game = Game, File = file });
            
            _uploading = false;
            await Refresh();
        }

        private async Task ChangePosition(ModSettings modSettings, int move)
        {
            await Mediator.Send(new ChangeModPositionCommand {Game = modSettings.Game, Id = modSettings.Id, Move = move});
            await Refresh();
        }
    }
}