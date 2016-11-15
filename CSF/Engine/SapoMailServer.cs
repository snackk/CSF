namespace Engine
{
    class SapoMailServer : MailParser
    {
        /// <summary>
        /// SapoMailServer tags that give us the information.
        /// The 2 last tags refers to an email sent from outlook
        /// </summary>
        public SapoMailServer(string emailHeader) : base(emailHeader, "Sapo", "X-PTMail-RemoteIP:", "Message-ID:", "From:",
                                                    "X-Originating-IP:", "User-Agent:", "X-PTMail-Version:", "X-PTMail-User:", "X-Mailer: ", "Content-Language: ")
        { }
    }
}
