namespace Engine
{
    class SapoMailServer : MailParser
    {
        /// <summary> 
        /// SapoMailServer tags that give us the information relative to the header.
        /// The 2 last tags refers to an email sent from outlook
        /// !-! case there is no information
        /// </summary>
        public SapoMailServer(string emailHeader) : base(emailHeader, "Sapo", "X-PTMail-RemoteIP:", "Message-ID:", "From:", "To: ",
                                                    "X-Originating-IP: ::ffff:", "User-Agent:", "X-PTMail-Version:", "X-PTMail-User:", "X-Mailer: ", "Content-Language: ")
        { }
    }
}
