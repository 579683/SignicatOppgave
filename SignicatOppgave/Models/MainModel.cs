using System;
using System.Collections.Generic;
using System.Text;

namespace SignicatOppgave.Models
{
    // Model used to display JSON data
    public class Identity
    {
        public string providerId { get; set; }
        public string fullName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
        public string nin { get; set; }
    }

    public class Environment
    {
        public string userAgent { get; set; }
        public string ipAddress { get; set; }
    }

    public class RedirectSettings
    {
        public string successUrl { get; set; }
        public string abortUrl { get; set; }
        public string errorUrl { get; set; }
    }

    public class MainRoot
    {
        public string id { get; set; }
        public string url { get; set; }
        public string status { get; set; }
        public DateTime created { get; set; }
        public DateTime expires { get; set; }
        public string provider { get; set; }
        public Identity identity { get; set; }
        public Environment environment { get; set; }
        public List<string> allowedProviders { get; set; }
        public string language { get; set; }
        public string flow { get; set; }
        public List<string> include { get; set; }
        public RedirectSettings redirectSettings { get; set; }
        public string externalReference { get; set; }
    }

}
