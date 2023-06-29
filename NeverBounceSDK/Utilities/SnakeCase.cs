namespace NeverBounce.Utilities; 
using System.Text;
using System.Text.Json;

/// <summary>JSON naming policy to convert .NET MyClass into my_class used by OpenAI</summary>
class SnakeCase : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {

        if (string.IsNullOrEmpty(name)) return name;

        if (name.Length < 3)
            return name.ToLowerInvariant(); // don't turn names like "ID" into "i_d", but "TopP" should become "top_p"

        var sb = new StringBuilder();
        sb.Append(char.ToLowerInvariant(name[0]));
        for (int i = 1; i < name.Length; i++)
        {
            if (char.IsUpper(name[i]))
            {
                sb.Append('_');
                sb.Append(char.ToLowerInvariant(name[i]));
            }
            else
                sb.Append(name[i]);
        }

        return sb.ToString();
    }
}
