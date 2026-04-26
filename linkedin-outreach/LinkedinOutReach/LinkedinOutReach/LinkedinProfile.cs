using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinOutReach
{
    public class LinkedinProfile
    {
        public string Id { get; }
        public string Link { get; }
        public string Name { get; }
        public string CompanyName { get; }

        public LinkedinProfile(
            string id, 
            string link, 
            string name, 
            string companyName
        ){
            Id = id;
            Link = link;
            Name = name;
            CompanyName = companyName;
        }
    }
}
