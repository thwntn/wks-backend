namespace Wks.Mains;

///
/// Summary:
///     Provides utility methods for JSON serialization, deserialization, object mapping and cloning.
///     Supports various JSON operations including type conversion, deep cloning, and property merging.
///
public static class Mapper
{
    private static readonly JsonSerializerSettings jsonSerializerSettings =
        new()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
        };

    private static readonly JsonSerializer jsonSerializer =
        JsonSerializer.Create(
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
            }
        );

    ///
    /// Summary:
    ///     Serializes an object to JSON string with custom settings for API responses.
    ///     Uses camelCase property names, ignores null values and reference loops.
    ///
    /// Parameters:
    ///   value:
    ///     The object to serialize to JSON.
    ///
    /// Returns:
    ///     A JSON string representation of the object with camelCase formatting.
    ///
    public static string Serialize(this object value) =>
        JsonConvert.SerializeObject(
            value,
            Formatting.None,
            jsonSerializerSettings
        );

    ///
    /// Summary:
    ///     Deserializes a JSON string to a strongly-typed object.
    ///
    /// Type parameters:
    ///   TFormat:
    ///     The target type to deserialize the JSON into.
    ///
    /// Parameters:
    ///   json:
    ///     The JSON string to deserialize.
    ///
    /// Returns:
    ///     An object of type TFormat deserialized from the JSON string.
    ///
    public static TFormat Deserialize<TFormat>(
        this string json,
        TFormat @default = default
    )
    {
        try
        {
            return JsonConvert.DeserializeObject<TFormat>(
                json,
                jsonSerializerSettings
            );
        }
        catch (JsonException)
        {
            if (@default.Justifiable())
                return @default;
            throw;
        }
    }

    ///
    /// Summary:
    ///     Converts an object to a JSON string and then escapes it for safe string representation.
    ///     Useful for storing JSON data in string fields or logging.
    ///
    /// Parameters:
    ///   json:
    ///     The object to convert to escaped JSON string.
    ///
    /// Returns:
    ///     An escaped JSON string representation of the object.
    ///
    public static string JsonString(this object json) =>
        JsonConvert.ToString(JsonConvert.SerializeObject(json));

    ///
    /// Summary:
    ///     Maps an object to a different type by serializing and then deserializing it.
    ///     Useful for converting between similar object structures.
    ///
    /// Type parameters:
    ///   TFormat:
    ///     The target type to map the object to.
    ///
    /// Parameters:
    ///   data:
    ///     The source object to map.
    ///
    /// Returns:
    ///     A new object of type TFormat with data from the source object.
    ///
    public static TFormat MapTo<TFormat>(this object data) =>
        JToken.FromObject(data, jsonSerializer).ToObject<TFormat>();

    ///
    /// Summary:
    ///     Creates a deep clone of the specified object by serializing it to a JSON token
    ///     and deserializing it back into a new instance.
    ///     Useful when a completely independent copy of an object is required,
    ///     including nested reference-type properties.
    ///
    /// Type parameters:
    ///   TFormat:
    ///     The type of object to clone.
    ///
    /// Parameters:
    ///   data:
    ///     The source object to clone.
    ///
    /// Returns:
    ///     A new instance of type TFormat containing the same data as the source object.
    ///
    /// Remarks:
    ///     Reference loops are ignored during the cloning process.
    ///     Properties involved in circular references may not be fully preserved.
    ///
    public static TFormat Clone<TFormat>(this TFormat data)
    {
        JsonSerializer serializer = JsonSerializer.Create(
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            }
        );

        return JToken.FromObject(data, serializer).ToObject<TFormat>();
    }

    ///
    /// Summary:
    ///     Merges properties from a source object into a destination object.
    ///     Properties from the source will override existing properties in the destination.
    ///
    /// Type parameters:
    ///   TFormat:
    ///     The type of objects to merge.
    ///
    /// Parameters:
    ///   destination:
    ///     The destination object to merge properties into.
    ///
    ///   from:
    ///     The source object containing properties to merge.
    ///
    /// Returns:
    ///     A new object of type TFormat with merged properties from both objects.
    ///
    public static TFormat Merge<TFormat>(this TFormat destination, object from)
    {
        /// Logic:
        ///     Converts destination object to dictionary for property manipulation.
        ///
        Dictionary<string, object> wait = MapTo<Dictionary<string, object>>(
            destination
        );

        /// Logic:
        ///     Converts source object to dictionary for property extraction.
        ///
        Dictionary<string, object> trigger = MapTo<Dictionary<string, object>>(
            from
        );

        /// Logic:
        ///     Iterates through source properties and merges them into destination.
        ///     Existing properties are updated, new properties are added.
        ///
        foreach (string key in trigger.Keys)
            if (wait.ContainsKey(key) is true)
                wait[key] = trigger[key];
            else
                wait.Add(key, trigger[key]);

        return wait.MapTo<TFormat>();
    }
}
