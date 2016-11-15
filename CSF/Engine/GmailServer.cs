namespace Engine
{
    class GmailServer : MailParser
    {
        /// <summary>
        /// GmailServer has too little information, this constructor is WRONG!
        /// Add the proper tags
        /// </summary>
        public GmailServer(string emailHeader) : base(emailHeader, "Gmail", "Received: from", "Message-ID:", "From:",
                                                    "CMM-sender-ip:", "User-Agent:", "X-PTMail-Version:", "X-PTMail-User:", "", "")
        { }
    }
}
