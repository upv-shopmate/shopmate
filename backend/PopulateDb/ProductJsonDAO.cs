using Newtonsoft.Json;
using System.Collections.Generic;

namespace PopulateDb
{
    internal class ProductJsonDAO
    {
        [JsonProperty("name")]
        public string Name { get; internal set; }

        [JsonProperty("weight")]
        public double? Weight { get; internal set; }

        [JsonProperty("volume")]
        public double? Volume { get; internal set; }

        [JsonProperty("units")]
        public ushort? Units { get; internal set; }

        [JsonProperty("origin_country")]
        public string OriginCountry { get; internal set; }

        [JsonProperty("edible")]
        public bool Edible { get; internal set; }

        [JsonProperty("price")]
        public decimal Price { get; internal set; }

        [JsonProperty("images")]
        public List<string> Pictures { get; internal set; }

        [JsonProperty("available_stock")]
        public uint? AvailableStock { get; internal set; }

        [JsonProperty("times_sold")]
        public uint TimesSold { get; internal set; }

        [JsonProperty("brands")]
        public List<string> Brands { get; internal set; }

        [JsonProperty("categories")]
        public List<string> Categories { get; internal set; }

        [JsonProperty("labels")]
        public List<string> Labels { get; internal set; }
    }
}
