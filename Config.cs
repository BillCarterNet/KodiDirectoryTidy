namespace KodiDirectoryTidy
{
    public class Config
    {
        private string TVDramaDirectory = @"E:\Downloads\TV Drama2\";
        private string MoviesSearchDirectory = @"E:\Downloads\Movies\";
        private string OutputDirectory = @".\Report\";
        private string OutputFile = "Report.txt";
        public string getTVDramaDirectory()
        {
            return TVDramaDirectory;
        }
        public string getMoviesSearchDirectory()
        {
            return MoviesSearchDirectory;
        }
        public string getOutputPath()
        {
            return OutputDirectory + OutputFile;
        }
    }
}