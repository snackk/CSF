using System;
using System.Collections.Generic;

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
                    System.Console.WriteLine(temp.Length);
                }
            }
        }
    }
}
