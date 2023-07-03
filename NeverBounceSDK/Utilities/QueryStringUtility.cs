using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverBounce.Utilities;

public static class QueryStringUtility
{
    /// <summary>Creates a urlencoded query string of the parameters</summary>
    /// <param name="request">The request parameters to encode</param>
    /// <param name="parentProperty">The nested parent parameter</param>
    public static string ToQueryString(object request, string? parentProperty = null)
    {
        // Get all properties on the object
        var properties = request.GetType().GetProperties()
            .Where(x => x.CanRead)
            .Where(x => x.GetValue(request, null) is not null)
            .ToDictionary(
                x => x.Name,
                x => x.PropertyType.ToString().Contains("System.Boolean")
                    ? Convert.ToInt32(x.GetValue(request, null))
                    : x.GetValue(request, null)
            );

        // Get names for all IEnumerable properties (excl. string)
        var propertyNames = properties
            .Where(x => x.Value is not string && x.Value is IEnumerable)
            .Select(x => x.Key)
            .ToList();

        // Concat all IEnumerable properties into a comma separated string
        foreach (string? key in propertyNames)
        {
            object? value = properties[key];
            if (value is null) continue;

            var valueType = value.GetType();
            var valueElemType = valueType.IsGenericType
                ? valueType.GetGenericArguments()[0]
                : valueType.GetElementType();

            if (valueElemType is null) continue;

            if (valueElemType.IsPrimitive || valueElemType == typeof(string))
            {
                var enumerable = value as IEnumerable;
                if (enumerable is not null)
                    properties[key] = string.Join(",", enumerable.Cast<object>());
            }
        }

        // Concat all key/value pairs into a string separated by ampersand
        return string.Join("&", properties
            .Select(x => BuildQueryStringKeyValue(x, parentProperty)));
    }

    /// <summary>Builds URI safe key value pair</summary>
    /// <param name="pair">The key value pair to encode</param>
    /// <param name="parentProperty">The nested parent parameter</param>
    static string? BuildQueryStringKeyValue(KeyValuePair<string, object?> pair, string? parentProperty = null)
    {
        if (pair.Value is null) return null;

        string key = parentProperty is not null
            ? $"{parentProperty}[{Uri.EscapeDataString(pair.Key)}]"
            : Uri.EscapeDataString(pair.Key);

        if (pair.Value.GetType().IsPrimitive || pair.Value.GetType().IsValueType || pair.Value is string)
        {
            string? valueStr = pair.Value.ToString();
            if (valueStr is not null)
                return $"{key}={Uri.EscapeDataString(valueStr)}";
            else
                return null;
        }

        return ToQueryString(pair.Value, key);
    }
}
