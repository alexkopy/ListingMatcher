using System;
using System.Collections.Generic;

namespace ListingMatcher
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Wraps the logic of creating a new dictionary entry for the key before assigning value or adding adding
        /// the value right away, if the associated key already exists.
        /// </summary>
        /// <param name="dictionary">The implicit dictionary instance this function can be called on.</param>
        /// <param name="productName">The key that may or may not already be in the dictionary.</param>
        /// <param name="listing">The value that must be associated with the given key.</param>
        public static void AddOrUpdate(this Dictionary<string, List<Listing>> dictionary, string productName, Listing listing)
        {
            if (listing == null)
                return;

            if (productName == null)
                throw new ArgumentNullException("productName", "productName argument cannot be null.");

            List<Listing> listings;

            if (dictionary.TryGetValue(productName, out listings))
                listings.Add(listing);
            else
                dictionary.Add(productName, new List<Listing> { listing });
        }
    }
}
