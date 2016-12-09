namespace Engine
{
    class HotmailMailServer : MailParser
    {
        /// <summary> 
        /// HotmailMailServer tags that give us the information relative to the header.
        /// The 2 last tags refers to an email sent from outlook
        /// !-! case there is no information
        /// </summary>
        public HotmailMailServer(string emailHeader) : base(emailHeader, "Hotmail", "Received: from", "Message-ID:", "From:", "To: ",
                                                    "CMM-sender-ip: ", "User-Agent:", "X-PTMail-Version:", "X-PTMail-User:", "!-!", "!-!"){ }
    }
}
