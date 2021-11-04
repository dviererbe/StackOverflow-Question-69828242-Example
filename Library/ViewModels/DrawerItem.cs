using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.ViewModels
{

    public class DrawerItem
    {
        [JsonPropertyName("itemIcon")]
        public string Icon { get; set; }

        [JsonPropertyName("itemName")]
        public string Name { get; set; }
    }
}