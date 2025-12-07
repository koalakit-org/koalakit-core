namespace Koalakit.Primitives.Enumerations;

public abstract class AppEnumeration(int id, string name) : Enumeration(id, name)
{
    public virtual string GetDisplayName()
    {
        if (string.IsNullOrWhiteSpace(Name))
            return Name;

        var result = string.Concat(
            Name.Select((c, i) => i > 0 && char.IsUpper(c) && !char.IsUpper(Name[i - 1])
                ? $" {c}"
                : c.ToString())
        );

        return result;
    }
    
    public virtual string GetLocalizationKey()
    {
        return $"{GetType().Name}.{Name}";
    }
}