namespace Engine
{
    class HotmailMailServer : MailParser
    {
        /// <summary>
        /// HotmailMailServer tags that give us the information.
        /// </summary>
        public HotmailMailServer(string emailHeader) : base(emailHeader, "Hotmail", "Received: from", "Message-ID:", "From:",
                                                    "CMM-sender-ip:", "User-Agent:", "X-PTMail-Version:", "X-PTMail-User:", "", ""){ }
    }
}
