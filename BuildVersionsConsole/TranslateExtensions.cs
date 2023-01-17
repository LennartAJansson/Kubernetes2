namespace BuildVersionsConsole;

using BuildVersionsApi.Model;

using Containers.Common.Types;

using System.Text.Json;

public static class TranslateExtensions
{
    public static object? TranslateResponse(this ResponseObject response)
    {
        //The problem is that the API responds with ResponseObject where Data is typed as an object
        //This will in turn result in that ResponseObject.Data will be typed as a JsonElement from Refit
        //To convert a JsonElement to either IEnumerable<BuildVersion> or single BuildVersion we use the following method:

        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        if (response is not null && response.Data is not null)
        {
            JsonElement element = (JsonElement)response.Data;

            if (element.ValueKind is JsonValueKind.Object)
            {
                //JsonElement is possible to Deserialize into another datatype
                BuildVersion? value = JsonSerializer.Deserialize<BuildVersion>(element, options);
                return value is null ? null : (object)value;
            }
            else if (element.ValueKind is JsonValueKind.Array)
            {
                //JsonElement is possible to Deserialize into another datatype
                IEnumerable<BuildVersion>? value = JsonSerializer.Deserialize<IEnumerable<BuildVersion>>(element, options);
                return value is null ? null : (object)value;
            }
        }

        return null;
    }
}