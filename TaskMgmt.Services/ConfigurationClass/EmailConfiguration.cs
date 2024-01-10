namespace TaskMgmt.Services.ConfigurationClass
{
    public class EmailConfiguration
    {
        public string SenderEmail { get; set; } = null!;
        public string SenderName { get; set; } = null!;
        public string SenderPassword { get; set; } = null!;
        public string Host { get; set; } = null!;
        public int Port { get; set; }
        public bool UseSsl { get; set; }
    }

}
