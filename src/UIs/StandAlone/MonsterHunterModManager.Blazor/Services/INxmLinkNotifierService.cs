namespace MonsterHunterModManager.Blazor.Services;

public interface INxmLinkNotifierService
{
    delegate Task NotifyNxmLinkEventHandler(string link);

    event NotifyNxmLinkEventHandler NotifyNxmLinkEvent;
    List<string> Links { get; set; }
}