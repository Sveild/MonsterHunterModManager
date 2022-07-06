namespace MonsterHunterModManager.BlazorApp.Extensions;

public static class StringExtensions
{
    public static string CapitalizeFirst(this string str)
    {
        return char.ToUpper(str[0]) + str.Substring(1);
    }
}