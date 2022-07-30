namespace MonsterHunterModManager.Blazor.Services;

public class NxmLinkNotifierService : INxmLinkNotifierService
{
    private static NxmLinkNotifierService? _instance = null;
    public static NxmLinkNotifierService Instance
    {
        get { return _instance ??= new NxmLinkNotifierService(); }
    }

    public event INxmLinkNotifierService.NotifyNxmLinkEventHandler? NotifyNxmLinkEvent;
    public List<string> Links { get; set; } = new();

    public async Task RaiseEvent(string link)
    {
        if (NotifyNxmLinkEvent != null)
        {
            await NotifyNxmLinkEvent(link);
        }
    }
}