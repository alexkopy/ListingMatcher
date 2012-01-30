using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ListingMatcher
{
    public static class JsonIO
    {
        // fairly standard pattern
        // http://stackoverflow.com/questions/1271225/c-sharp-reading-a-file-line-by-line
        private static IEnumerable<string> ReadLines(string filePath)
        {
            if(!File.Exists(filePath))
                throw new ArgumentException("Requested file: " + filePath + " doesn't exist.");

            using (var reader = File.OpenText(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        public static List<T> JsonDeserialize<T>(string filePath)
        {
            var records = from line in ReadLines(filePath)
                          let record = JsonConvert.DeserializeObject<T>(line)
                          select record;

            return records.ToList();
        }

        public static string[] JsonSerialize<T>(List<T> records)
        {
            if (records == null)
                return new string[0];

            var lines = from record in records
                    let json = JsonConvert.SerializeObject(record)
                    select json;

            return lines.ToArray();
        }
    }
}
