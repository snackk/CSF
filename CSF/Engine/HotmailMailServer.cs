using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    class HotmailMailServer : MailParser
    {
        public HotmailMailServer(string emailHeader) : base(emailHeader, "Received: from", "Message-ID:", "From:",
                                                    "CMM-sender-ip:", "User-Agent:", "X-PTMail-Version:", "X-PTMail-User:"){ }
    }
}
