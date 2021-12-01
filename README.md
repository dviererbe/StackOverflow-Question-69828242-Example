# Example solution for the [Stack Overflow Question 69828242](https://stackoverflow.com/questions/69828242/how-to-pass-none-english-letters-to-json)

This solution demonstrates how to use the [System.Text.Json.JsonSerializer](https://docs.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializer?view=net-6.0)
to deserialize an json string that is encoded with non ASCII Characters like `ä`, `ü`, `ö`, `ß` without the use of escaping (see [RFC 8259 Section 7](https://datatracker.ietf.org/doc/html/rfc8259#section-7)).

## TL;DR

### make it work for `netstandard2.0` or `netstandard2.1`

If you are targeting `netstandard2.0` or `netstandard2.1` add a reference to the `System.Text.Json` Nuget Package. (https://www.nuget.org/packages/System.Text.Json).

You can do this with the .NET CLI (Command Line Interface):
```bash
dotnet add package System.Text.Json
```
<details>
    <summary><b>Why?</b> (expand for more info)</summary>
    The Reason for this is that the <i>System.Text.Json</i> API's were first shipped with <i>.NET Core 3</i> (https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-apis/) and are therefore not included in the <i>netstandard2.0</i> or <i>netstandard2.1</i> framework, which existed before <i>.NET Core 3</i>.
</details>

### Code

```C#
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

const string Json = @"{
    ""text"": ""Text with ä, ü, ö and ß.""
}";

JsonSerializerOptions serializerOptions = new JsonSerializerOptions
{
    // This Encoder allows to parse Unicode Symbols without \uXXXX escaping
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true,
};

var deserializedItem = JsonSerializer.Deserialize<Item>(Json, serializerOptions);
Console.WriteLine($"Text of deserialized Item: {deserializedItem.Text}\n");

Console.WriteLine("serialize deserialized Item back:");
Console.WriteLine(JsonSerializer.Serialize(deserializedItem, serializerOptions));

public class Item
{
    public string Text { get; set; }
}
```

This should output:
```
Text of deserialized Item: Text with ä, ü, ö and ß.

serialize deserialized Item back:
{
  "text": "Text with ä, ü, ö and ß."
}
```

## References

- https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-character-encoding
- https://stackoverflow.com/questions/69828242/how-to-pass-none-english-letters-to-json

## LICENSE

WTFPL - See [LICENSE](https://github.com/dviererbe/StackOverflow-Question-69828242-Example/blob/master/LICENSE) for more info.
