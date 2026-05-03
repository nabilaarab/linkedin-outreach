using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedinOutReach.models
{
    internal class LinkedinProfile
    {
        public string Id { get; }
        public string Link { get; }
        public string Name { get; }
        public string? CompanyName { get; }
        public string? CurrentJob { get; }
        public bool? IsConnected { get; }

        public LinkedinProfile(string id, string link, string name, string? companyName = null, string? currentJob = null, bool? isConnected = null)
        {
            Id = id;
            Link = link;
            Name = name;
            CompanyName = companyName;
            CurrentJob = currentJob;
            IsConnected = isConnected;
        }
    }
}
