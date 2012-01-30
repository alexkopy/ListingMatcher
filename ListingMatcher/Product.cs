using Newtonsoft.Json;
using System.Collections.Generic;

namespace ListingMatcher
{
    public sealed class Product
    {
        [JsonIgnore]
        private string[] _tokens;

        [JsonIgnore]
        public string[] Tokens
        {
            get
            {
                return _tokens ?? (_tokens = AddTags().ToArray());
            }
        }

        private List<string> AddTags()
        {
            var tokens = new List<string>();
            if (!string.IsNullOrWhiteSpace(Manufacturer))
                tokens.Add(Manufacturer);

            if (!string.IsNullOrWhiteSpace(Family))
                tokens.Add(Family);

            if (!string.IsNullOrWhiteSpace(Model))
                tokens.Add(Model);

            return tokens;
        }

        [JsonProperty(PropertyName = "product_name")]
        public string ProductName { get; set; }

        [JsonProperty(PropertyName = "manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty(PropertyName = "family")]
        public string Family { get; set; }

        [JsonProperty(PropertyName = "model")]
        public string Model { get; set; }

        [JsonProperty(PropertyName = "announced-date")]
        public string DateAnnounced { get; set; }
    }
}
