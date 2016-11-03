using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    class SapoMailServer : MailParser
    {
        public SapoMailServer(string emailHeader) : base(emailHeader, "X-PTMail-RemoteIP:", "Message-ID:", "From:",
                                                    "X-Originating-IP:", "User-Agent:", "X-PTMail-Version:", "X-PTMail-User:"){ }
    }
}
