
namespace GG_Webbshop
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class AllProductsResponseModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("weight")]
        public long Weight { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("createDate")]
        public DateTimeOffset CreateDate { get; set; }

        [JsonProperty("stock")]
        public long Stock { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("discount")]
        public double Discount { get; set; }
        [JsonProperty("highlighted")]
        public bool Highlighted { get; set; }
    }

    public partial class AllProductsResponseModel
    {
        public static AllProductsResponseModel[] FromJson(string json) => JsonConvert.DeserializeObject<AllProductsResponseModel[]>(json, GG_Webbshop.Converter.Settings);
        public static AllProductsResponseModel FromJsonSingle(string json) => JsonConvert.DeserializeObject<AllProductsResponseModel>(json, GG_Webbshop.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this AllProductsResponseModel[] self) => JsonConvert.SerializeObject(self, GG_Webbshop.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
