namespace Engine
{
    class GmailServer : MailParser
    {
        /// <summary> 
        /// GmailServer tags that give us the information relative to the header.
        /// The 2 last tags refers to an email sent from outlook
        /// !-! case there is no information
        /// </summary>
        public GmailServer(string emailHeader) : base(emailHeader, "Gmail", "Received: by ", "Message-ID: ", "From: ",
                                                    "To: ", "!-!", "!-!", "!-!", "!-!", "!-!", "!-!")
        { }
    }
}
