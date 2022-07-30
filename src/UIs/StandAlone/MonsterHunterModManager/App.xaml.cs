using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using MonsterHunterModManager.Domain.ValueObjects;

namespace MonsterHunterModManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private const string NXM_KEY = "nxm";
        private const string NXM_DEFAULT_NAME = "";
        private const string NXM_DEFAULT_VALUE = "URL:nxm";
        private const string NXM_URL_PROTOCOL_NAME = "URL Protocol";
        private const string NXM_URL_PROTOCOL_VALUE = "";
        
        private const string SHELL_KEY = "shell";
        
        private const string OPEN_KEY = "open";
        
        private const string COMMAND_KEY = "command";
        private const string COMMAND_DEFAULT_NAME = "";

        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
            {
                MessageBox.Show(error.ExceptionObject.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            };
            
            // var exists = System.Diagnostics.Process
            //     .GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly()?.Location))
            //     .Length > 1;
            //
            // if (e.Args.Length > 0)
            // {
            //     if (exists)
            //         HandleNxmLink(e.Args[0]);
            //     
            //     NxmLinkNotifierService.Instance.Links.Add(e.Args[0]);
            //     CreateNamedPipeServer();
            // }
            // else if (exists)
            // {
            //     MessageBox.Show("Application is already running.");
            //     Current.Shutdown();
            // }
            // else
            //     CreateNamedPipeServer();
            //
            // AddLinkHandler();
        }

        // private void CreateNamedPipeServer()
        // {
        // }
        //
        //
        // private void HandleNxmLink(string link)
        // {
        //     Current.Shutdown();
        // }
        //
        // private void AddLinkHandler()
        // {
        //     var identity = WindowsIdentity.GetCurrent();
        //     var principal = new WindowsPrincipal(identity);
        //
        //     if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
        //         return;
        //     
        //     CreateRegistryKeyIfNotExists(Registry.ClassesRoot, NXM_KEY);
        //     var nxmKey = Registry.ClassesRoot.OpenSubKey(NXM_KEY, true);
        //     CreateValueIfNotExistsElseReplaceIt(nxmKey, NXM_DEFAULT_NAME, NXM_DEFAULT_VALUE);
        //     CreateValueIfNotExistsElseReplaceIt(nxmKey, NXM_URL_PROTOCOL_NAME, NXM_URL_PROTOCOL_VALUE);
        //     
        //     CreateRegistryKeyIfNotExists(nxmKey, SHELL_KEY);
        //     var shellKey = nxmKey.OpenSubKey(SHELL_KEY, true);
        //     
        //     CreateRegistryKeyIfNotExists(shellKey, OPEN_KEY);
        //     var openKey = shellKey.OpenSubKey(OPEN_KEY, true);
        //
        //     var executablePath = Environment.GetCommandLineArgs()[0];
        //
        //     if (executablePath.EndsWith(".dll"))
        //         executablePath = executablePath.Replace(".dll", ".exe");
        //     
        //     CreateRegistryKeyIfNotExists(openKey, COMMAND_KEY);
        //     var commandKey = openKey.OpenSubKey(COMMAND_KEY, true);
        //     CreateValueIfNotExistsElseReplaceIt(commandKey, COMMAND_DEFAULT_NAME, $"\"{executablePath}\" \"%1\"");
        // }
        //
        // private void CreateRegistryKeyIfNotExists(RegistryKey parentKey, string key)
        // {
        //     if (!parentKey.GetSubKeyNames().Any(k => k == key))
        //         parentKey.CreateSubKey(key);
        // }
        //
        // private void CreateValueIfNotExistsElseReplaceIt(RegistryKey parentKey, string name, string value)
        // {
        //     if (!parentKey.GetValueNames().Any(valueName => valueName == name))
        //         parentKey.SetValue(name, value);
        //     else
        //     {
        //         var currentValue = (string?)parentKey.GetValue(name);
        //
        //         if (currentValue != value)
        //             parentKey.SetValue(name, value);
        //     }
        // }
    }
}