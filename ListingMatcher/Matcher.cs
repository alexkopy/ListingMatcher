using System;
using System.Collections.Generic;
using System.Linq;

namespace ListingMatcher
{
    public static class Matcher
    {
        public static List<Result> FindProductToListingMatching(List<Product> products, List<Listing> listings)
        {
            if (listings == null)
                throw new ArgumentNullException("listings");

            if (products == null)
                throw new ArgumentNullException("products");

            var allListingsOfProduct = new Dictionary<string, List<Listing>>();

            foreach (var product in products)
            {
                if(product == null)
                    continue;

                FindAllListingsForProduct(product, listings, allListingsOfProduct);
            }

            return FormatResults(allListingsOfProduct);
        }

        private static void FindAllListingsForProduct(Product product, List<Listing> listings, Dictionary<string, List<Listing>> allListingsOfProduct)
        {
            foreach (var listing in listings)
            {
                if (listing == null || listing.Picked || !ListingMatchesProduct(product.Tokens, listing.Tokens))
                    continue;

                listing.Picked = true;
                allListingsOfProduct.AddOrUpdate(product.ProductName, listing);
            }
        }

        private static bool ListingMatchesProduct(string[] productTokens, string[] listingTokens)
        {
            if(productTokens == null || listingTokens == null)
                return false;

            if (listingTokens.Length < productTokens.Length)
                return false;

            int foundProductTokenCount = 0;

            // foreach would be cleaner, but adds non-negligible overhead in profiler
            // due to enumerator object generation at runtime instead of simple array traversal
            for (int productTokenIndex = 0; productTokenIndex < productTokens.Length; productTokenIndex++)
            {
                if(productTokens[productTokenIndex] == null)
                    continue;

                if (foundProductTokenCount >= productTokens.Length)
                    break;

                foundProductTokenCount += MatchCountForProductToken(productTokens[productTokenIndex], listingTokens);
            }

            return foundProductTokenCount == productTokens.Length;
        }

        private static int MatchCountForProductToken(string productToken, string[] listingTokens)
        {
            int currentMatchCount = 0;
            for (int j = 0; j < listingTokens.Length; j++)
            {
                if (listingTokens[j] == null || productToken.Length != listingTokens[j].Length)
                    continue; //80% time reduction on my machine with sample data, thanks to this

                if (productToken.Equals(listingTokens[j], StringComparison.InvariantCultureIgnoreCase))
                {
                    currentMatchCount++;
                    break;
                }
            }
            return currentMatchCount;
        }

        /// <summary>
        /// Transforms a dictionary mapping product names to listings of that product into a list of Result objects
        /// for JSON serialization.
        /// </summary>
        /// <param name="allListingsOfProduct">Dictionary of product name and associated listings.</param>
        /// <returns>Each Result object holds JSON serializable version of each Key-Value from the input dictionary.</returns>
        private static List<Result> FormatResults(Dictionary<string, List<Listing>> allListingsOfProduct)
        {
            if (allListingsOfProduct == null)
                return null;

            var results = (from tuple in allListingsOfProduct
                           where tuple.Value != null
                           select new Result {ProductName = tuple.Key, Listings = tuple.Value}).ToList();

            return results;
        }
    }
}
