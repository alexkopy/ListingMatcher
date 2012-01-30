using System;
using Newtonsoft.Json;

namespace ListingMatcher
{
    public sealed class Listing
    {
        [JsonIgnore]
        private string[] _tokens;

        [JsonIgnore]
        public bool Picked { get; set; }
        [JsonIgnore]
        public string[] Tokens
        {
            get
            {
                if (_tokens == null)
                {
                    if (Title == null)
                        _tokens = new string[0];
                    else
                    {
                        _tokens = Title.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);

                        // remove leading and trailing whitespace
                        for (int i = 0; i < _tokens.Length; i++)
                        {
                            _tokens[i] = _tokens[i].Trim();
                        }
                    }
                }

                return _tokens;
            }
        }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "price")]
        public string Price { get; set; }
    }
}
