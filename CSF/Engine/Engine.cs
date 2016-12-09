using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Engine
{
    public class Engine
    {
        private MailParser server { set; get; } = null;
        public String engineOutput { private set; get; } = "";
        private List<MailParser> servers = new List<MailParser>();

        /// <summary>
        /// Engine should test each server and choose the one which gives the most significant data.
        /// </summary>
        public Engine(string mailHeader) {
            servers.Add(new SapoMailServer(mailHeader));
            servers.Add(new HotmailMailServer(mailHeader));
            servers.Add(new GmailServer(mailHeader));

            getEngineOutput();
        }

        public static String[] GetMxRecordsByDomain(String domain)
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

        /// <summary>
        /// Get a string of all tags.
        /// </summary>
        private void getEngineOutput() {
            String temp;
            foreach (MailParser mp in servers) {
                temp = mp.getAllTags();
                if (temp.Split('\n').Length > engineOutput.Split('\n').Length)
                {
                    engineOutput = temp;
                    Console.WriteLine(temp.Length);
                }
            }
            engineOutput = "";
            engineOutput += test("smtp.tecnico.ulisboa.pt");
            string[] ola = GetMxRecordsByDomain("gmail.com");
            for (int i = 0; i < ola.Length; i++) {
                engineOutput += ola[i]+Environment.NewLine;
                //engineOutput += test(ola[i]);
            }
        }
        private string test(string eachMXserver)
        {
            string val = "";

            TcpClient tClient = new TcpClient(eachMXserver, 25);
            string CRLF = "\r\n";
            byte[] dataBuffer;
            string ResponseString;
            NetworkStream netStream = tClient.GetStream();
            StreamReader reader = new StreamReader(netStream);
            ResponseString = reader.ReadLine();

            /* Perform HELO to SMTP Server and get Response */
            dataBuffer = BytesFromString("HELO ist173972" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            dataBuffer = BytesFromString("MAIL FROM:<diasogo.p.dos.santos@ist.utl.pt>" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            /* Read Response of the RCPT TO Message to know from google if it exist or not */
            dataBuffer = BytesFromString("RCPT TO:<" + "diogo.p.dos.santos@ist.utl.pt" + ">" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();

             val = "does exist";
            if (GetResponseCode(ResponseString) == 550)
                {
                /*DOES NOT EXIST*/
                val = "does not exist";
                }
                /* QUITE CONNECTION */
                dataBuffer = BytesFromString("QUITE" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                tClient.Close();
            return val;
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

