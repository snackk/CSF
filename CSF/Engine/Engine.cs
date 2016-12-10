using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Engine
{
    public class Engine
    {
        private MailParser server { set; get; } = null;
        private Dictionary<String, String> geoData = new Dictionary<string, string>();

        public String engineOutput { private set; get; } = "";

        /// <summary>
        /// Engine should test each server and choose the one which gives the most significant data.
        /// </summary>
        public Engine(string mailHeader, string emailToCreate) {
            switch (emailToCreate.ToLower()) {
                case "gmail":
                    server = new GmailServer(mailHeader);
                    break;
                case "hotmail":
                    server = new HotmailMailServer(mailHeader);
                    break;
                case "sapo":
                    server = new SapoMailServer(mailHeader);
                    break;
                case "ist":
                    server = new ISTMailServer(mailHeader);
                    break;
            }
            getEngineOutput();
        }

        /// <summary>
        /// Get a string of all tags.
        /// </summary>
        private void getEngineOutput() {
            string possOutout = "";
            string geo = "";
            string user = server.from;
            string domain = server.getDomain();

            engineOutput += "Result of nslookup -q=mx " + domain + Environment.NewLine;
           
            bool passed = false;

            string[] mxs = GetMxRecordsByDomain(domain);
            for (int i = 0; i < mxs.Length; i++) {
                engineOutput += "      ->MX server " + mxs[i]+Environment.NewLine;
             }
            engineOutput += Environment.NewLine;
            if (mxs.Length != 0)
            {
                if (testForUserOnServer(mxs[0], "", user) || testForUserOnServer(mxs[0], user, user))
                {
                    passed = true;
                    possOutout += "        Email verified, " + user + " exists!" + Environment.NewLine;
                }
            }
            
            if (passed)
                engineOutput += possOutout + "___________________________________________" + Environment.NewLine + server.getAllTags();
            else engineOutput += "        Couldn't verify if email exists." + Environment.NewLine + "___________________________________________" + Environment.NewLine + server.getAllTags();

            if (server.fromIP != null)
            {
                engineOutput += "___________________________________________" + Environment.NewLine;
                getGeoData();
                engineOutput += "Geodata collected" + Environment.NewLine;
                foreach (KeyValuePair<string, string> entry in geoData)
                {
                    engineOutput += "      ->" + entry.Key + ": " + entry.Value + Environment.NewLine;
                }
            }
        }

        private String[] GetMxRecordsByDomain(String domain)
        {
            ProcessStartInfo start = new ProcessStartInfo("nslookup");
            start.RedirectStandardOutput = true;
            start.UseShellExecute = false;
            start.Arguments = "-type=MX " + domain;

            Process nslookup = Process.Start(start);

            List<String> output = new List<String>();
            List<String> mx = new List<String>();

            while (!nslookup.StandardOutput.EndOfStream)
            {
                output.Add(nslookup.StandardOutput.ReadLine());
                if (output.Last().Contains("mail exchanger = "))
                {
                    mx.Add(output.Last().Remove(0, output.Last().IndexOf("mail exchanger = ") + "mail exchanger = ".Length));
                }
            }

            return mx.ToArray();
        }

        private void getGeoData() {
            string url = "http://ip-api.com/xml/" + server.fromIP;
            WebClient wc = new WebClient();
            wc.Proxy = null;
            MemoryStream ms = new MemoryStream(wc.DownloadData(url));
            XmlTextReader rdr = new XmlTextReader(url);
            XmlDocument doc = new XmlDocument();
            ms.Position = 0;
            doc.Load(ms);
            ms.Dispose();
            foreach (XmlElement el in doc.ChildNodes[1].ChildNodes)
            {
                geoData.Add(el.Name, el.InnerText);
            }
        }

        private bool testForUserOnServer(string eachMXserver, string heloUsername, string mailTest){
            TcpClient tClient = null;
            bool exists = false;
            try
            {
                tClient = new TcpClient();
                var result = tClient.BeginConnect(eachMXserver, 25, null, null);

                var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));

                if (!success)
                {
                    Console.WriteLine("Could not connect on port 25 to " + eachMXserver + Environment.NewLine);
                    return false;
                }
            }
            catch (SocketException e) {
                Console.WriteLine("Could not connect on port 25 to " + eachMXserver + Environment.NewLine);
                return false;
            }
            string CRLF = "\r\n";
            byte[] dataBuffer;
            string ResponseString;
            NetworkStream netStream = tClient.GetStream();
            StreamReader reader = new StreamReader(netStream);
            ResponseString = reader.ReadLine();

            /* Perform HELO to SMTP Server and get Response */
            dataBuffer = BytesFromString("HELO" + heloUsername + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            dataBuffer = BytesFromString("MAIL FROM:<" + mailTest + ">" + CRLF);/*DOENST NECESSARY NEEDS TO EXIST*/
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();

            if (GetResponseCode(ResponseString) == 220)
            {
                /* Read Response of the RCPT TO Message to know from google if it exist or not */
                dataBuffer = BytesFromString("RCPT TO:<" + mailTest + ">" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                ResponseString = reader.ReadLine();

                if (GetResponseCode(ResponseString) == 250)
                {
                    exists = true;
                }
                /* QUITE CONNECTION */
                dataBuffer = BytesFromString("QUITE" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                tClient.Close();
            }

            if (exists)
                return true;
            else return false;
        }
        
        private byte[] BytesFromString(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
        private int GetResponseCode(string ResponseString)
        {
            return int.Parse(ResponseString.Substring(0, 3));
        }
    }
}

