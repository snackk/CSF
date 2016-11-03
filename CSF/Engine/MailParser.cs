using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    abstract class MailParser
    {
        public string emailHeader { private set; get; }
        public string mailServerIP { private set; get;}
        public string messageID { private set; get; }
        public string from { private set; get; }
        public string fromIP { private set; get; }
        public string userAgent { private set; get; }
        public string mailVersion { private set; get; }
        public string mailUser { private set; get; }

        public string tagEmailHeader { set; get; }
        public string tagMailServerIP { set; get; }
        public string tagMessageID { set; get; }
        public string tagFrom { set; get; }
        public string tagFromIP { set; get; }
        public string tagUserAgent { set; get; }
        public string tagMailVersion { set; get; }
        public string tagMailUser { set; get; }

        /// <summary>
        /// explain each argument.
        /// </summary>
        public MailParser(string emailHeader) {
            this.emailHeader = emailHeader;
        }

        /// <summary>
        /// explain each argument.
        /// </summary>
        private string parseData(String tag) {
            int index = emailHeader.IndexOf(tag);
            string parsed = "";
            if (index != -1)
            {
                parsed = emailHeader.Substring(index + tag.Length);
            }
            return parsed;
        }

        public void parseAllTags() {
            mailServerIP = parseData(tagMailServerIP);
            messageID = parseData(tagMessageID);
            from = parseData(tagFrom);
            fromIP = parseData(tagFromIP);
            userAgent = parseData(tagUserAgent);
            mailVersion = parseData(tagMailVersion);
            mailUser = parseData(tagMailUser);
        }

        public virtual string getAllTags() {
            return "Server IP: " + mailServerIP + System.Environment.NewLine +
                "Message ID: " + messageID + System.Environment.NewLine +
                "From: " + from + System.Environment.NewLine +
                "From IP: " + fromIP + System.Environment.NewLine +
                "User Agent: " + userAgent + System.Environment.NewLine +
                "Mail Version: " + mailVersion + System.Environment.NewLine +
                "Mail User: " + mailUser + System.Environment.NewLine;
        }

        public void Delete() {
        }
    }
}
