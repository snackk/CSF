using System.Text.RegularExpressions;

namespace Engine
{
    abstract class MailParser
    {
        public string emailHeader { private set; get; }
        public string emailServerUsed {private set; get; }

        //Variables after parsing the emailHeader
        public string mailServerIP { private set; get;}
        public string messageID { private set; get; }
        public string from { private set; get; }
        public string to { private set; get; }
        public string fromIP { private set; get; }
        public string userAgent { private set; get; }
        public string mailVersion { private set; get; }
        public string mailUser { private set; get; }
        public string mailApp { private set; get; }
        public string mailLang { private set; get; }

        //Variables to know what to parse from
        private string tagEmailHeader { set; get; }
        private string tagMailServerIP { set; get; }
        private string tagMessageID { set; get; }
        private string tagFrom { set; get; }
        private string tagTo { set; get; }
        private string tagFromIP { set; get; }
        private string tagUserAgent { set; get; }
        private string tagMailVersion { set; get; }
        private string tagMailUser { set; get; }
        private string tagMailApp { set; get; }
        private string tagMailLang { set; get; }

        /// <summary>
        /// MailParser receives string arguments, in case of none, pass "!-!"
        /// @Argument _emailHeader -> Email header
        /// @Argument _emailServerUsed -> Email server used
        /// @Argument _tagMailServerIP -> What's your server's IP
        /// @Argument _tagMessageID -> Message ID on the server
        /// </summary>
        public MailParser(string _emailHeader,string _emailServerUsed, string _tagMailServerIP, string _tagMessageID, string _tagFrom, string _tagTo,
                        string _tagFromIP, string _tagUserAgent, string _tagMailVersion, string _tagMailUser,
                        string _tagMailApp, string _tagMailLang) {

            this.emailHeader = _emailHeader;
            this.emailServerUsed = _emailServerUsed;

            this.tagMailServerIP = _tagMailServerIP;
            this.tagMessageID = _tagMessageID;
            this.tagFrom = _tagFrom;
            this.tagTo = _tagTo;
            this.tagFromIP = _tagFromIP;
            this.tagUserAgent = _tagUserAgent;
            this.tagMailVersion = _tagMailVersion;
            this.tagMailUser = _tagMailUser;

            this.tagMailApp = _tagMailApp;
            this.tagMailLang = _tagMailLang;

            parseAllTags();
        }

        /// <summary>
        /// Returns the parsed data from a variable.
        /// </summary>
        private void parseAllTags() {
            int index;
            string[] header_lines = Regex.Split(emailHeader, "\r\n|\r|\n");

            foreach(string hl in header_lines) {

                if (hl.Contains("!-!"))
                    continue;

                if (hl.Contains(tagMailServerIP)) {
                    index = hl.IndexOf(tagMailServerIP);
                    if(mailServerIP == null)
                        mailServerIP = hl.Substring(index + tagMailServerIP.Length); 
                    else mailServerIP += System.Environment.NewLine + hl.Substring(index + tagMailServerIP.Length); 
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
                if (hl.Contains(tagTo))
                {
                    index = hl.IndexOf(tagTo);
                    to = hl.Substring(index + tagTo.Length);
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
                if (hl.Contains(tagMailApp))
                {
                    index = hl.IndexOf(tagMailApp);
                    mailApp = hl.Substring(index + tagMailApp.Length);
                }
                if (hl.Contains(tagMailLang))
                {
                    index = hl.IndexOf(tagMailLang);
                    mailLang = hl.Substring(index + tagMailLang.Length);
                }
            }

        }

        /// <summary>
        /// Return all tags by string
        /// </summary>
        public virtual string getAllTags()
        {
            string allTags = "The email header refers to " + emailServerUsed + " server." + System.Environment.NewLine + System.Environment.NewLine;
            if (!(mailServerIP == null))
                allTags += emailServerUsed + " server(s) IP: " + mailServerIP + System.Environment.NewLine;
            if (!(messageID == null))
                allTags += "MessageID on " + emailServerUsed + ": " + messageID + System.Environment.NewLine;
            if (!(from == null))
                allTags += "From: " + from + System.Environment.NewLine;
            if (!(to == null))
                allTags += "To: " + to + System.Environment.NewLine;
            if (!(fromIP == null))
                allTags += "From IP: " + fromIP + System.Environment.NewLine;
            if (!(userAgent == null))
                allTags += "User Agent: " + userAgent + System.Environment.NewLine;
            if (!(mailVersion == null))
                allTags += "Mail Version: " + mailVersion + System.Environment.NewLine;
            if (!(mailUser == null))
                allTags += "Mail User: " + mailUser + System.Environment.NewLine;
            if (!(mailApp == null))
                allTags += "Mail App used: " + mailApp + System.Environment.NewLine;
            if (!(mailLang == null))
                allTags += "Language of the App used: " + mailLang + System.Environment.NewLine;

            return allTags;
        }
    }
}
