using System.Collections.Generic;
using Newtonsoft.Json;

namespace ListingMatcher
{
    public sealed class Result
    {
        [JsonProperty(PropertyName = "product_name")]
        public string ProductName { get; set; }

        [JsonProperty(PropertyName = "listings")]
        public List<Listing> Listings { get; set; }
    }
}
