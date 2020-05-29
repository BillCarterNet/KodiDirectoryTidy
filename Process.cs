using System;
using System.IO;

namespace KodiDirectoryTidy
{
    public class Process
    {
        private string report;
        public void CreateOutputFile(string outputPath) 
        {

            var line = "Kodi Directory Tidy Report" + System.Environment.NewLine;
            DateTime start = DateTime.Now;
            line += "Start: " + start + System.Environment.NewLine;

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(outputPath)) 
            {

                file.WriteLine(line);

            }
        }
        public void DirectorySearch(string searchDirectory) 
        {
            try 
            {

                foreach (string d in Directory.GetDirectories(searchDirectory))
                {

                    foreach (string f in Directory.GetFiles(d))
                    {

                        Episode episode = new Episode(f);
                        report += "Original:  " + f + System.Environment.NewLine;
                        report += "Processed: " + episode.getKodiPath() + System.Environment.NewLine;

                        if (f == episode.getKodiPath())
                        {

                            report += "No Processing Needed" + System.Environment.NewLine;

                        }
                        else
                        {

                            moveEpisode(episode);

                        }

                        report += System.Environment.NewLine;

                    }

                    DirectorySearch(d);

                }

            }
            catch (System.Exception excpt)
            {

                Console.WriteLine(excpt.Message);

            }                           
        }
        public void writeReport(string outputPath)    
        {

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(outputPath, true))
            {

                file.WriteLine(report);

            }

        }
        private void moveEpisode(Episode episode)
        {

            // Create destination (does nothing if already exists)
            Directory.CreateDirectory(episode.getKodiDirectory());
            // Move file
            try
            {

                File.Move(episode.getOriginalPath(),episode.getKodiPath());
                report += "File Moved" + System.Environment.NewLine;

            }
            catch (System.Exception excpt)
            {

                Console.WriteLine("Unable To Move File");
                Console.WriteLine(excpt.Message);

            }

        }
        public void deleteEmptyDirectories(string startDirectory)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(startDirectory))
                {

                    if (Directory.GetFiles(d).Length == 0 && Directory.GetDirectories(d).Length == 0)
                    {

                        Directory.Delete(d, false);
                        report += "Deleting " + d + System.Environment.NewLine;

                    }

                    deleteEmptyDirectories(d);

                }
            }
            catch (System.Exception excpt)
            {
                
                Console.WriteLine(excpt.Message);

            }
        }
    }
}