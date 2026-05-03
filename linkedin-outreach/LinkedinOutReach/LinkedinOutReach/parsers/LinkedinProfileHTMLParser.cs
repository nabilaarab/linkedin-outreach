using HtmlAgilityPack;
using LinkedinOutReach.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace LinkedinOutReach.parsers
{
    internal class LinkedinProfileHTMLParser
    {
        private string _htmlContent;
        private HtmlDocument _htmlDocument;

        public LinkedinProfileHTMLParser(string htmlContent)
        {
            this._htmlContent = htmlContent;
            this._htmlDocument = new HtmlDocument();
            this._htmlDocument.LoadHtml(this._htmlContent);
        }

        public List<LinkedinProfile> ExtractLinkedinProfiles()
        {
            // Get List of Linkedin Profiles in HTML
            var listHTMLNode = this._htmlDocument.DocumentNode.SelectSingleNode("//div[@role='list']");

            // Split the HTML content of each Linkedin profile
            var itemProfilesNodes = listHTMLNode.SelectNodes("//div[@role='listitem']");

            // Iterate profiles and extract Linkedin Profile
            LinkedinProfile linkedinProfile;
            List<LinkedinProfile> linkedinProfiles;

            string id;
            string link;
            JobCompanyHTMLParser jobCompanyHTMLParser = new JobCompanyHTMLParser(this._htmlContent);
            linkedinProfiles = new List<LinkedinProfile>();
            foreach (var item in itemProfilesNodes){

                // Fetch the <a> element that contains the name and the link of the profile
                HtmlNode element = this.extractNodeNameLinkProfile(item);

                link = element.GetAttributeValue("href", "");
                id = link.Remove(0, link.LastIndexOf("/in/") + 4);
                id = id.Replace("/", "");

                jobCompanyHTMLParser.updateHtmlContent(item.OuterHtml);
                jobCompanyHTMLParser.fetchContent();

                linkedinProfile = new LinkedinProfile(
                    id,
                    link,
                    element.InnerText.Trim(),
                    jobCompanyHTMLParser.CompanyName,
                    jobCompanyHTMLParser.CurrentJob,
                    null
                );
                linkedinProfiles.Add(linkedinProfile);
            }

            return linkedinProfiles;
        }

        private HtmlNode extractNodeNameLinkProfile(HtmlNode htmlNode)
        {
            HtmlNode? element = htmlNode.SelectNodes(".//a").
                FirstOrDefault(a => !string.IsNullOrEmpty(a.InnerText.Trim()));

            if (element == null){
                throw new NodeNotFoundException("Node that contains name and link of the profile not found.");
            }

            return element;
        }
    }
}
