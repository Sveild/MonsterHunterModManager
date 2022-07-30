using Microsoft.AspNetCore.Components.WebView.Wpf;
using Microsoft.Extensions.FileProviders;

namespace MonsterHunterModManager.Controls;

public class EmbeddedResourceBlazorWebView : BlazorWebView
{
    public override IFileProvider CreateFileProvider(string contentRootDir)
    {
        var type = typeof(Blazor.Main);
        return new EmbeddedFileProvider(type.Assembly, $"{type.Namespace}.wwwroot");
    }
}