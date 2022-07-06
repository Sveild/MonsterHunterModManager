using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using MonsterHunterModManager.BlazorApp.Services;
using MonsterHunterModManager.WpfApp.Services;
using MudBlazor.Services;

namespace MonsterHunterModManager.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            var serviceCollection = new ServiceCollection();
            
            serviceCollection.AddWpfBlazorWebView();
#if DEBUG
            serviceCollection.AddBlazorWebViewDeveloperTools();
#endif
            
            serviceCollection.AddTransient<IFolderPickerService, FolderPickerService>();
            serviceCollection.AddMudServices();
            serviceCollection.AddSingleton<ISettingsService, SettingsService>();
            serviceCollection.AddSingleton<IPhysicalFileService, PhysicalFileService>();
            
            Resources.Add("services", serviceCollection.BuildServiceProvider());
        }
    }
    
    // Workaround for compiler error "error MC3050: Cannot find the type 'local:Main'"
    // It seems that, although WPF's design-time build can see Razor components, its runtime build cannot.
    public partial class Main { }
}