using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using MonsterHunterModManager.Domain;
using MonsterHunterModManager.Domain.Enums;
using MonsterHunterModManager.Extensions;
using MudBlazor;

namespace MonsterHunterModManager.Shared.Components
{
    public partial class AppBar : IDisposable
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        
        private bool _drawerOpen;
        [Parameter] public bool DrawerOpen
        {
            get => _drawerOpen;
            set
            {
                if (_drawerOpen == value)
                    return;

                _drawerOpen = value;
                DrawerOpenChanged.InvokeAsync(value);
            }
        }
        [Parameter] public EventCallback<bool> DrawerOpenChanged { get; set; }

        private bool _isDarkMode;
        [Parameter] public bool IsDarkMode {
            get => _isDarkMode; 
            set
            {
                if (_isDarkMode == value)
                    return;

                _isDarkMode = value;
                IsDarkModeChanged.InvokeAsync(value);
            }
        }
        [Parameter] public EventCallback<bool> IsDarkModeChanged { get; set; }

        private MudTheme _theme;
        [Parameter] public MudTheme Theme {
            get => _theme;
            set
            {
                if (_theme == value)
                    return;

                _theme = value;
                ThemeChanged.InvokeAsync(value);
            }
        }
        [Parameter] public EventCallback<MudTheme> ThemeChanged { get; set; }
        
        private Games _currentGame = Games.None;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            NavigationManager.LocationChanged += NavigationManagerLocationChanged;
        }

        private void DrawerToggle() => DrawerOpen = !DrawerOpen;

        private string GetThemeSwitchIcon()
        {
            return IsDarkMode ? Icons.Filled.DarkMode : Icons.Outlined.DarkMode;
        }

        private void NavigationManagerLocationChanged(object sender, LocationChangedEventArgs e)
        {
            var location = e.Location.Substring(e.Location.LastIndexOf("/") + 1);

            if (string.IsNullOrEmpty(location))
                _currentGame = Games.None;
            else
                _currentGame = (Games)Enum.Parse(typeof(Games), location.CapitalizeFirst());
            
            StateHasChanged();
        }
        
        private string GetImagePath()
        {
            return _currentGame switch
            {
                Games.World => "images/World.png",
                Games.Rise => "images/Rise.png",
                _ => ""
            };
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= NavigationManagerLocationChanged;
        }
    }
}
