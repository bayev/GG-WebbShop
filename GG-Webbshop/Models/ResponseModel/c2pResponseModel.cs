using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GG_Webbshop.Models.ResponseModel
{
    

    public partial class c2pResponseModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("weight")]
        public decimal Weight { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("createDate")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("c2pID")]
        public string C2PId { get; set; }

        [JsonProperty("discount")]
        public decimal Discount { get; set; }
    }
    public partial class c2pResponseModel
    {
        public static c2pResponseModel[] FromJson(string json) => JsonConvert.DeserializeObject<c2pResponseModel[]>(json, GG_Webbshop.Converter.Settings);
        public static c2pResponseModel FromJsonSingle(string json) => JsonConvert.DeserializeObject<c2pResponseModel>(json, GG_Webbshop.Converter.Settings);
    }
}
