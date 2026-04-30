using LinkedinOutReach.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedinOutReach.browserDriverAction
{
    internal class BrowserDriverActionNewProfilesExtraction : BrowserDriverAction
    {
        public BrowserDriverActionNewProfilesExtraction(BrowserDriver browserDriver) : base(browserDriver) { }
        public override async Task Run(LinkedinProfile linkedinProfile)
        {
            for (int i = 1; i <= 100; i++)
            {
                this.goToPeopleSearchPage("IT Manager", i);
                Task.Delay(Random.Shared.Next(5000, 6000)).Wait();
            }
        }

        private void goToPeopleSearchPage(string keyword, int pageNumber)
        {
            string url = $"https://www.linkedin.com/search/results/people/?keywords={keyword}&page={pageNumber}";
            this.goToPage(url);
        }

        public override string ToString()
        {
            return "Action - Extraction de nouveaux profils";
        }
    }
}
