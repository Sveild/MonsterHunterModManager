﻿using FluentValidation;
using Microsoft.AspNetCore.Components;
using MonsterHunterModManager.Application.Common.Interfaces;
using MonsterHunterModManager.Blazor.FormValidators;
using MonsterHunterModManager.Domain.Entities;
using MudBlazor;

namespace MonsterHunterModManager.Blazor.Shared.Dialog;

public partial class GameSettingsDialog
{
    [Inject] private IPickerService PickerService { get; set; }
    
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
 
    [Parameter] public bool AllowCloseModal { get; set; }
    [Parameter] public GameSettings Settings { get; set; }

    private readonly FluentValueValidator<string> _folderValidator = new(x => x.NotEmpty().FolderExists());
    
    private MudForm _form;
    private bool _isValid;

    protected override Task OnInitializedAsync()
    {
        MudDialog.Options.CloseButton = false;
        MudDialog.Options.CloseOnEscapeKey = false;
        MudDialog.Options.DisableBackdropClick = true;
        MudDialog.SetOptions(MudDialog.Options);

        return base.OnInitializedAsync();
    }

    private async Task PickGameFolder()
    {
        var folder = PickerService.PickFolder("Select game directory", Settings.GameDirectory);
        Settings.GameDirectory = string.IsNullOrEmpty(folder) ? Settings.GameDirectory : folder;
        await _form.Validate();
    }
    
    private async Task PickModsFolder()
    {
        var folder = PickerService.PickFolder("Select mods directory", Settings.ModsDirectory);
        Settings.ModsDirectory = string.IsNullOrEmpty(folder) ? Settings.ModsDirectory : folder;
        await _form.Validate();
    }

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    private void Save()
    {
        MudDialog.Close(DialogResult.Ok(Settings));
    }
}