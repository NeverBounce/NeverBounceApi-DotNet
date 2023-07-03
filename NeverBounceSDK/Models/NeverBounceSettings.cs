namespace NeverBounce.Models;

public record NeverBounceSettings(string Key, string Url);

public record NeverBounceConfigurationSettings(string Key, string? Version = null, string? Host = null);