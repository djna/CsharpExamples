using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;


namespace djna
{
    class Gutenberg
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText(@"Gutenberg/Wonder.txt");
            IEnumerable<string> words = Regex.Split(text, @"\W+")
                    .Where( w => w.Length > 2)
                    .Select(u => u.ToLower());
            
            string stoptext = File.ReadAllText(@"Gutenberg/stopwords");
            IEnumerable<string> stopwords = Regex.Split(stoptext, @"\W");

            Console.WriteLine("Read {0} words of more than 2 characters  and {1} stopwords", words.Count(), stopwords.Count());

            IEnumerable<string> uncommonWords = words.Where( w => ! stopwords.Contains(w) );

             Console.WriteLine("{0} uncommon words", uncommonWords.Count());

            var frequency = uncommonWords.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count())
               .OrderByDescending(key => key.Value)
               .Take(10)
               ;

            foreach (KeyValuePair<string, int> kvp in frequency) {
               Console.WriteLine("{0} - {1}", kvp.Key, kvp.Value);
            }
            

        }
    }
}
