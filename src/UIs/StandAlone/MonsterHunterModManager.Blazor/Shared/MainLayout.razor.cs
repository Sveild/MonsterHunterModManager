using MonsterHunterModManager.Domain.Enums;
using MudBlazor;

namespace MonsterHunterModManager.Blazor.Shared
{
    public partial class MainLayout
    {
        private bool _isDarkMode = true;
        private MudTheme _theme = new();
        private bool _drawerOpen = false;
        
        private Games _currentGame = Games.None;
    }
}
