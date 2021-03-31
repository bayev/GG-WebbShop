namespace GG_Webbshop.Models
{
    using Newtonsoft.Json;
    using System;
    public partial class OrderResponseModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("orderEmail")]
        public string OrderEmail { get; set; }

        [JsonProperty("orderDate")]
        public DateTime OrderDate { get; set; }

        [JsonProperty("orderStatus")]
        public string OrderStatus { get; set; }

        [JsonProperty("paymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonProperty("orderDetails")]
        public object[] OrderDetails { get; set; }
    }

    public partial class OrderResponseModel
    {
        public static OrderResponseModel FromJsonSingle(string json) => JsonConvert.DeserializeObject<OrderResponseModel>(json, GG_Webbshop.Converter.Settings);
        public static OrderResponseModel[] FromJson(string json) => JsonConvert.DeserializeObject<OrderResponseModel[]>(json, GG_Webbshop.Converter.Settings);
    }


}








