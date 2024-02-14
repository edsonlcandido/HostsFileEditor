using System.Diagnostics;
using System.Text.RegularExpressions;

namespace HostsFileEditor
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            // read file C:\Windows\System32\drivers\etc\hosts
            string path = @"C:\Windows\System32\drivers\etc\hosts";
            // list to store the hosts
            List<HostEntry> hosts = new List<HostEntry>();

            StreamReader sr = new StreamReader(path);
            try
            {
                using (sr)
                {
                    // Read and display lines from the file until the end of the file is reached.
                    string[] fileRows = sr.ReadToEnd().Split('\n');
                    string[] fileRows2 = File.ReadAllLines(path);
                    int numberOfLines = fileRows.Length;
                    int lineNumber = 0;
                    foreach (string line in fileRows)
                    {
                        
                        // create a regex to verify start with # or not
                        // if start with #, it's a comment or disabled entry
                        Regex regex = new Regex(@"^#");
                        // create a regex to verify the IP
                        string ipPattern = @"\b(?:[0-9]{1,3}\.){3}[0-9]{1,3}\b";
                        // create a regex to verify the URL
                        string urlRegex = @"\b(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}\b";
                        // create a regex to verify the domain start from a URL
                        string domainRegex = @"\.([^\.]+)$";
                        
                        //verify if line contains the ipRegex match
                        Match ipMatch = Regex.Match(line, ipPattern);
                        if (ipMatch.Success)
                        {
                            //create a new host entry
                            HostEntry host = new HostEntry();
                            //get the url value
                            Match urlMatch = Regex.Match(line, urlRegex);
                            //get the domain value
                            Match domainMatch = Regex.Match(urlMatch.Value, domainRegex);
                            //set the domain is enable
                            host.Enable = !line.StartsWith("#");
                            host.Ip = ipMatch.Value;
                            if (urlMatch.Value == "")
                            {
                                host.Url = "localhost";
                                host.Domain = "";
                            }
                            else
                            {
                                host.Url = urlMatch.Value;
                                host.Domain = domainMatch.Value;
                            }
                           
                            //get the line number
                            host.LineNumber = lineNumber;
                            //add the host to the list
                            hosts.Add(host);
                        }
                        lineNumber++;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("O arquivo não pôde ser lido:");
                Debug.WriteLine(e.Message);
            }

        Application.Run(new Form1(hosts));
        }
    }
    public class HostEntry
    {
        public bool Enable { get; set; }
        public string Ip { get; set; }
        public string Url { get; set; }
        public string Domain { get; set; }
        public int LineNumber { get; set; }
    }
}