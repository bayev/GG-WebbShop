using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GG_Webbshop.Models.ResponseModel
{
    public partial class UserLoginResponseModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("BillingAdress")]
        public string Billingaddress { get; set; }

        [JsonProperty("defaultshippingaddress")]
        public string Defaultshippingaddress { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("PhoneNumber")]
        public string Phone { get; set; }
    }

    public partial class UserLoginResponseModel
    {
        public static UserLoginResponseModel[] FromJson(string json) => JsonConvert.DeserializeObject<UserLoginResponseModel[]>(json, GG_Webbshop.Converter.Settings);
        public static UserLoginResponseModel FromJsonSingle(string json) => JsonConvert.DeserializeObject<UserLoginResponseModel>(json, GG_Webbshop.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this UserLoginResponseModel[] self) => JsonConvert.SerializeObject(self, GG_Webbshop.Converter.Settings);
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
