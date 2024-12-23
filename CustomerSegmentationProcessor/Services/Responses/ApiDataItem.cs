using Newtonsoft.Json;

namespace CustomerSegmentationProcessor.Services.Responses
{
    public class ApiDataItem
    {
        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }
        public string Title { get; set; }
        [JsonProperty("prizm_id")]
        public int PrizmId { get; set; }
        public string Id { get; set; }
    }
}
