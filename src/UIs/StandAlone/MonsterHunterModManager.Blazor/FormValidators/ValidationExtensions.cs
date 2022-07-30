using FluentValidation;

namespace MonsterHunterModManager.Blazor.FormValidators;

public static class ValidationExtensions
{
    public static IRuleBuilder<T, string> FolderExists<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(Directory.Exists).WithMessage("Directory must exists");
    }
}