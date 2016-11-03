using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    class SapoMailServer : MailParser
    {
        public SapoMailServer(string emailHeader) : base(emailHeader)
        {
            base.tagMailServerIP = "X-PTMail-RemoteIP:";
            base.tagMessageID = "Message-ID:";
            base.tagFrom = "From:";
            base.tagFromIP = "X-Originating-IP:";
            base.tagUserAgent = "User-Agent:";
            base.tagMailVersion = "X-PTMail-Version:";
            base.tagMailUser = "X-PTMail-User:";

            parseAllTags();
        }
    }
}
