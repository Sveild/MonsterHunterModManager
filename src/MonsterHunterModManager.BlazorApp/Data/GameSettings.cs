namespace MonsterHunterModManager.BlazorApp.Data
{
    public class GameSettings
    {
        public Games Game { get; set; }
        public string GameDirectory { get; set; }
        public string ModsDirectory { get; set; }
        public List<Mod> Mods { get; set; }
    }
}
