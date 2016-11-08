using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Engine
    {
        private MailParser server { set; get; } = null;

        /// <summary>
        /// Engine should test each server and choose the one which gives the most significant data.
        /// </summary>
        public Engine(string mailHeader) {
            server = new HotmailMailServer(mailHeader);
        }

        /// <summary>
        /// Get a string of all tags.
        /// </summary>
        public string getEngineOutput() {
            return server.getAllTags();
        }
    }
}
