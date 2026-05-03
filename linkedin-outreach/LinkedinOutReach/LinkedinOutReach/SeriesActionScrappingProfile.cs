using HtmlAgilityPack;
using LinkedinOutReach.browserDriverAction;
using LinkedinOutReach.models;
using LinkedinOutReach.parsers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml.Linq;

namespace LinkedinOutReach
{
    internal class SeriesActionScrappingProfile : SeriesAction
    {
        public SeriesActionScrappingProfile(BrowserDriver browserDriver, LinkedinProfile[] linkedinProfiles, List<BrowserDriverAction> actionDesired, int minTimeBetweenActions, int maxTimeBetweenActions, string email, string password) :
            base(browserDriver, linkedinProfiles, actionDesired, minTimeBetweenActions, maxTimeBetweenActions, email, password)
        {}

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

            List<LinkedinProfile> linkedinProfiles = new List<LinkedinProfile>();
            List<LinkedinProfile> linkedinProfilesToConcat;
            for (int i = 1; i <= 100; i++)
            {
                // Change the current page
                bdaNewProfilesExtraction.PageNumber = i;
                this._browserDriver.Run(null);

                // Extract the information from the page content
                //string htmlProfiles = this.extractDivProfilesFrom(bdaNewProfilesExtraction.LastPageContent);

                //List<string> htmlListItemProfile = this.extractDivProfileFromDivProfiles(htmlProfiles);

                //linkedinProfiles = this.extractLinkedinProfilesFromDiv(htmlListItemProfile);
                LinkedinProfileHTMLParser htmlParser;
                htmlParser = new LinkedinProfileHTMLParser(bdaNewProfilesExtraction.LastPageContent);
                linkedinProfilesToConcat = htmlParser.ExtractLinkedinProfiles();

                linkedinProfiles.AddRange(linkedinProfilesToConcat);
            }
        }
    }
}
