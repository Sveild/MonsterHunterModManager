using Newtonsoft.Json;

namespace MonsterHunterModManager.BlazorApp.Data
{
    public class Mod
    {
        [JsonIgnore] public bool ShowDetails { get; set; }

        public string FileName { get; set; }
        public bool Enabled { get; set; }
    }
}