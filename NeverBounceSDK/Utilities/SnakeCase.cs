namespace NeverBounce.Utilities; 
using System.Text;
using System.Text.Json;

/// <summary>JSON naming policy to convert .NET MyClass into my_class</summary>
class SnakeCase : JsonNamingPolicy
{
    public override string ConvertName(string name) => Convert(name);

    public static string Convert(string name) {
        if (string.IsNullOrEmpty(name)) return name;

        if (name.Length < 3)
            return name.ToLowerInvariant(); // short-cut, 2 chars will always be all lowercase

        var sb = new StringBuilder();
        char first = name[0];
        bool prevUpper = char.IsUpper(name[0]);
        sb.Append(char.ToLowerInvariant(first));
        for (int i = 1; i < name.Length; i++)
        {
            if (char.IsUpper(name[i]))
            {
                if (!prevUpper)
                    sb.Append('_'); // avoid chain of _ (e.g. AbcDEF goes to abc_def, not abc_d_e_f)

                prevUpper = true;
                sb.Append(char.ToLowerInvariant(name[i]));
            }
            else
            {
                prevUpper = false;
                sb.Append(name[i]);
            }
        }

        return sb.ToString();
    }
}
