using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrownScrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileText = File.ReadAllLines(@"D:\MyDevelopmentStuff\CrownScrapper\ArtistsInput.txt", Encoding.UTF8);
            var artistList = new List<string>(fileText);
            var cleanedArtistList = new List<string>();

            var crownList = new List<string>();
            var artistLine = "";
            var artistCount = 0;

            for(var i = 1; i <= artistList.Count; i++)
            {
                var selectedArtist = artistList[i - 1];
                if(int.TryParse(selectedArtist, out _) || selectedArtist.StartsWith("Avatar for")
                    || selectedArtist.Contains("scrobbles") || selectedArtist == string.Empty){
                    continue;
                }

                // No Duplicates
                if(cleanedArtistList.Contains(selectedArtist)){
                    continue;
                }

                cleanedArtistList.Add(selectedArtist);
                artistLine += $" {selectedArtist} |";
                artistCount++;

                if(artistCount % 10 == 0){
                    crownList.Add("!cm " + artistLine.Remove(artistLine.Length - 1));
                    artistLine = string.Empty;
                    artistCount = 0;
                } else if (artistCount % 10 != 0 && i == artistList.Count)
                {
                    crownList.Add("!cm " + artistLine.Remove(artistLine.Length - 1));
                }
            }
            
            foreach(var line in crownList) {
                Console.WriteLine(line);
            }

            File.WriteAllLinesAsync(@"D:\MyDevelopmentStuff\CrownScrapper\ArtistsOutput.txt", crownList);
            File.WriteAllLinesAsync(@"D:\MyDevelopmentStuff\CrownScrapper\CleanedArtistsInput.txt", cleanedArtistList);
        }
    }
}
