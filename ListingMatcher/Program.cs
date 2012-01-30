using System;
using System.IO;

namespace ListingMatcher
{
    public static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Console.WriteLine(
                    "The program expects listings.txt and products.txt in the executable's directory, there are no options.");
                return;
            }

            // Since users aren't defined in the problem statement, no error handling is imposed
            // beyound parameter checking within supporting code

            var products = JsonIO.JsonDeserialize<Product>("products.txt");
            var listings = JsonIO.JsonDeserialize<Listing>("listings.txt");

            var matches = Matcher.FindProductToListingMatching(products, listings);

            var lines = JsonIO.JsonSerialize(matches);
            File.WriteAllLines("results.txt", lines);
        }
    }
}
