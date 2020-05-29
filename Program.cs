using System;

namespace KodiDirectoryTidy
{
    class Program
    {
        static void Main(string[] args)
        {

            var process = new Process();
            var config = new Config();

            process.CreateOutputFile(config.getOutputPath());
            process.DirectorySearch(config.getTVDramaDirectory());
            process.deleteEmptyDirectories(config.getTVDramaDirectory());
            process.writeReport(config.getOutputPath());
            
        }
    }
}
