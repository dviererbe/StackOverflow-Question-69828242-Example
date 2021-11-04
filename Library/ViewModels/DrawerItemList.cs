using System.Collections.Generic;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class DrawerItemList
    {
        private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        [JsonPropertyName("itemList")]
        public IEnumerable<DrawerItem> DrawerItems { get; set; }

        public static DrawerItemList LoadFromEmmbeddedJsonFile(CancellationToken cancellationToken = default)
        {
            return LoadFromEmmbeddedJsonFileAsync(cancellationToken).Result;
        }

        public static async Task<DrawerItemList> LoadFromEmmbeddedJsonFileAsync(CancellationToken cancellationToken = default)
        {
            const string FileName = "Library.Data.drawer.json";

            var assembly = Assembly.GetExecutingAssembly();
            var files = assembly.GetManifestResourceNames();

            using (var stream = assembly.GetManifestResourceStream(FileName))
            {
                return await JsonSerializer.DeserializeAsync<DrawerItemList>(stream, _serializerOptions, cancellationToken); 
            }
        }
    }
}