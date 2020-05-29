using System; 
using System.IO; 
using System.Collections.Generic; 
using System.Text.RegularExpressions;

namespace KodiDirectoryTidy
{
    public class Episode{
        // Properties
        private string originalPath;
        private string originalFileNameWithoutExtension;
        private string originalDirectory;
        private string showTitle;
        private string season;
        private string episode;
        private string seasonAndEpisode;
        private string fileExtension;
        private string kodiPath;
        private string kodiDirectory;
        // Constructor
        public Episode(string path) 
        {

            originalPath = path;
            originalFileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
            originalDirectory = Path.GetDirectoryName(path);
            fileExtension = Path.GetExtension(path);
            kodiPath = determineKodiPath(path);
            kodiDirectory = Path.GetDirectoryName(kodiPath);

        }
        // Methods
        private string determineKodiPath(string path) 
        {

            // Determine showTitle
            string[] directories = path.Split(Path.DirectorySeparatorChar);
            showTitle = directories[3];

            // Determine season and episode
            determineSeasonAndEpisode(path);

            // Construct kodi path
            if (!String.IsNullOrEmpty(season) && !String.IsNullOrEmpty(episode)) 
            {

                return directories[0] 
                    + @"\" + directories[1] 
                    + @"\" + directories[2] 
                    + @"\" + showTitle 
                    + @"\" + "Season " + getSeasonNlz() 
                    + @"\" + showTitle + " " + seasonAndEpisode
                    + fileExtension;

            } else {

                //return path;
                return directories[0] 
                    + @"\" + directories[1] 
                    + @"\" + directories[2]
                    + @"\" + showTitle
                    + @"\" + "Extras"
                    + @"\" + originalFileNameWithoutExtension
                    + fileExtension;
                    
            }
        }
        private void determineSeasonAndEpisode(string fileName) 
        {

            // Regexes to get season and episode groups
            List <Regex> regexes = new List<Regex> 
            {
                new Regex(@"[sS](?<season>\d{1,2})([eE]|\s[eE]|\s[eE][pP])(?<episode>\d{1,2})"),
                new Regex(@"(?<season>\d{1,2})[xX](?<episode>\d{1,2})"),
            };

            foreach(Regex regex in regexes) 
            {

                Match match = regex.Match(fileName);
                if (match.Success) 
                {
                    
                    season = match.Groups["season"].Value;
                    episode = match.Groups["episode"].Value;

                    if (season.Length == 1) 
                    {

                        seasonAndEpisode = "S0" + season + "E" + episode;

                    } else {

                        seasonAndEpisode = "S" + season + "E" + episode;

                    }

                    // Is it a mutli-episode file?
                    match = match.NextMatch();

                    if (match.Success) 
                    {

                        if (season.Length == 1) 
                        {

                            seasonAndEpisode += "-"
                                + "S0" + match.Groups["season"].Value 
                                + "E" + match.Groups["episode"].Value;

                        } else {

                            seasonAndEpisode += "-" 
                                + "S" + match.Groups["season"].Value 
                                + "E" + match.Groups["episode"].Value;

                        }
                    }

                    return;

                }
            }

            Console.WriteLine("No Match for Episode or Season in " + fileName);
            
        }
        private string getSeasonNlz() 
        {

            string seasonNlz = season;

            // Strip any leading zeroes
            while (seasonNlz.Substring(0, 1) == "0") 
            {

                seasonNlz = seasonNlz.Remove(0, 1);

            }

            return seasonNlz;

        }
        public string getKodiPath() 
        {

            return kodiPath;

        }
        public string getKodiDirectory()
        {

            return kodiDirectory;

        }
        public string getOriginalPath()
        {

            return originalPath;

        }

    }
}