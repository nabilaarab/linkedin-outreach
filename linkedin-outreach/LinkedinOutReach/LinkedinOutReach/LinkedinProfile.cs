using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedinOutReach
{
    internal class LinkedinProfile
    {
        public string Id { get; }
        public string Link { get; }
        public string Name { get; }
        public string CompanyName { get; }

        public LinkedinProfile(string id, string link, string name, string companyName)
        {
            Id = id;
            Link = link;
            Name = name;
            CompanyName = companyName;
        }
    }
}
