using HtmlAgilityPack;
using LinkedinOutReach.browserDriverAction;
using LinkedinOutReach.models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedinOutReach
{
    internal class SeriesActionScrappingProfile : SeriesAction
    {
        public SeriesActionScrappingProfile(BrowserDriver browserDriver, LinkedinProfile[] linkedinProfiles, List<BrowserDriverAction> actionDesired, int minTimeBetweenActions, int maxTimeBetweenActions, string email, string password) :
            base(browserDriver, linkedinProfiles, actionDesired, minTimeBetweenActions, maxTimeBetweenActions, email, password)
        { }

        public static SeriesActionScrappingProfile InitializationDefault()
        {
            // Load ExcelManager
            ExcelManager excelManager = new ExcelManager();
            excelManager.loadLinkedinProfiles();

            // Initialize the browser driver
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.local.json")
            .Build();
            BrowserDriver browserDriver = new BrowserDriver();

            // Define the desired actions to perform on each profile
            List<BrowserDriverAction> actionDesired = new List<BrowserDriverAction>();

            return new SeriesActionScrappingProfile(browserDriver, excelManager.LinkedinProfiles, actionDesired, 3000, 6000, config["Linkedin:Email"], config["Linkedin:Password"]);
        }

        public override void Run()
        {
            // First, connect to LinkedIn using the browser driver
            this._browserDriver.SetAction(
                new BrowserDriverActionConnexionLinkedin(this._browserDriver, this._email, this._password)
            );
            this._browserDriver.Run(null);

            // Then, explore new profiles
            BrowserDriverActionSearchProfileExtraction bdaNewProfilesExtraction = new BrowserDriverActionSearchProfileExtraction(this._browserDriver, "IT Manager");
            this._browserDriver.SetAction(bdaNewProfilesExtraction);

            for (int i = 1; i <= 100; i++)
            {
                bdaNewProfilesExtraction.PageNumber = i;
                this._browserDriver.Run(null);

                string text = this.extractDivProfilesFromBodyPageContent(bdaNewProfilesExtraction.LastPageContent);
                List<string> text_2 = this.extractDivProfileFromDivProfiles(text);
                this.extractLinkedinProfilesFromDiv(text_2);

                //this.extractInformationFromPageContent(bdaNewProfilesExtraction.LastPageContent);
            }
        }

        private void extractInformationFromPageContent(string pageContent)
        {
            // TODO : extract information from the page content and save it in the excel file
            throw new NotImplementedException();
        }

        private string extractDivProfilesFromBodyPageContent(string pageContent)
        {
            // TODO : extract the div containing the profile information from the page content
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageContent);

            // XPath
            string listDiv = doc.DocumentNode.SelectSingleNode("//div[@role='list']").OuterHtml;

            Console.WriteLine(listDiv);

            return listDiv;
            //throw new NotImplementedException();
        }

        private List<string> extractDivProfileFromDivProfiles(string pageContent)
        {
            // TODO : extract the div containing the profile information from the page content
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(pageContent);
            // XPath

            // Get list of Profile
            var listDiv = doc.DocumentNode.SelectSingleNode("//div[@role='list']");

            // Get Each profile
            var items = listDiv.SelectNodes(".//div[@role='listitem']");

            List<string> listDivProfile = new List<string>();
            foreach (var item in items)
            {
                listDivProfile.Add(item.OuterHtml);
            }
            return listDivProfile;
        }

        private List<LinkedinProfile> extractLinkedinProfilesFromDiv(List<string> divProfiles)
        {
            List<LinkedinProfile> linkedinProfiles = new List<LinkedinProfile>();
            HtmlDocument doc = new HtmlDocument();
            foreach (string divProfile in divProfiles)
            {
                doc.LoadHtml(divProfile);
                
                string name = doc.DocumentNode.SelectNodes(".//a").FirstOrDefault(a => !string.IsNullOrEmpty(a.InnerText.Trim())).InnerText.Trim();

                Console.WriteLine();
                //LinkedinProfile profile = new LinkedinProfile();
                //linkedinProfiles.Add(profile);
            }
            return linkedinProfiles;
        }
    }
}
