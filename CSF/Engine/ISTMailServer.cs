using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    class ISTMailServer : MailParser
    {
        /// <summary> 
        /// ISTMailServer tags that give us the information relative to the header.
        /// The 2 last tags refers to an email sent from outlook
        /// !-! case there is no information
        /// </summary>
        public ISTMailServer(string emailHeader) : base(emailHeader, "IST", "!-!", "Message-ID:", "From: ", "To: ",
                                                    "!-!", "User-Agent: ", "!-!", "!-!", "!-! ", "!-!")
        { }
    }
}
