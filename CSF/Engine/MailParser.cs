using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Engine
{
    abstract class MailParser
    {
        public string emailHeader { private set; get; }

        //Variables after parsing the emailHeader
        public string mailServerIP { private set; get;}
        public string messageID { private set; get; }
        public string from { private set; get; }
        public string fromIP { private set; get; }
        public string userAgent { private set; get; }
        public string mailVersion { private set; get; }
        public string mailUser { private set; get; }

        //Variables to know what to parse from
        public string tagEmailHeader { private set; get; }
        public string tagMailServerIP { private set; get; }
        public string tagMessageID { private set; get; }
        public string tagFrom { private set; get; }
        public string tagFromIP { private set; get; }
        public string tagUserAgent { private set; get; }
        public string tagMailVersion { private set; get; }
        public string tagMailUser { private set; get; }

        /// <summary>
        /// Input: emailHeader & Email header tags
        /// Stores: All the header's data
        /// </summary>
        public MailParser(string emailHeader, string _tagMailServerIP, string _tagMessageID, string _tagFrom,
                        string _tagFromIP, string _tagUserAgent, string _tagMailVersion, string _tagMailUser) {

            this.emailHeader = emailHeader;

            this.tagMailServerIP = _tagMailServerIP;
            this.tagMessageID = _tagMessageID;
            this.tagFrom = _tagFrom;
            this.tagFromIP = _tagFromIP;
            this.tagUserAgent = _tagUserAgent;
            this.tagMailVersion = _tagMailVersion;
            this.tagMailUser = _tagMailUser;

            parseAllTags();
        }

        /// <summary>
        /// Returns the parsed data from a variable.
        /// </summary>
        private void parseAllTags() {
            int index;
            string[] header_lines = Regex.Split(emailHeader, "\r\n|\r|\n");

            foreach(string hl in header_lines) {

                if (hl.Contains(tagMailServerIP)) {
                    index = hl.IndexOf(tagMailServerIP);
                    mailServerIP += hl.Substring(index + tagMailServerIP.Length) + System.Environment.NewLine;
                }
                if (hl.Contains(tagMessageID))
                {
                    index = hl.IndexOf(tagMessageID);
                    messageID = hl.Substring(index + tagMessageID.Length);
                }
                if (hl.Contains(tagFrom))
                {
                    index = hl.IndexOf(tagFrom);
                    from = hl.Substring(index + tagFrom.Length);
                }
                if (hl.Contains(tagFromIP))
                {
                    index = hl.IndexOf(tagFromIP);
                    fromIP = hl.Substring(index + tagFromIP.Length);
                }
                if (hl.Contains(tagUserAgent))
                {
                    index = hl.IndexOf(tagUserAgent);
                    userAgent = hl.Substring(index + tagUserAgent.Length);
                }
                if (hl.Contains(tagMailVersion))
                {
                    index = hl.IndexOf(tagMailVersion);
                    mailVersion = hl.Substring(index + tagMailVersion.Length);
                }
                if (hl.Contains(tagMailUser))
                {
                    index = hl.IndexOf(tagMailUser);
                    mailUser = hl.Substring(index + tagMailUser.Length);
                }
            }

        }

        /// <summary>
        /// Return all tags by string
        /// </summary>
        public virtual string getAllTags() {
            return "Server IP: " + mailServerIP + System.Environment.NewLine +
                "Message ID: " + messageID + System.Environment.NewLine +
                "From: " + from + System.Environment.NewLine +
                "From IP: " + fromIP + System.Environment.NewLine +
                "User Agent: " + userAgent + System.Environment.NewLine +
                "Mail Version: " + mailVersion + System.Environment.NewLine +
                "Mail User: " + mailUser + System.Environment.NewLine;
        }
    }
}
