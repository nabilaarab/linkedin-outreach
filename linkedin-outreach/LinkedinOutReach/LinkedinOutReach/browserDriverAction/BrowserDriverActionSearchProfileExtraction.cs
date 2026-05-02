using LinkedinOutReach.models;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedinOutReach.browserDriverAction
{
    internal class BrowserDriverActionSearchProfileExtraction : BrowserDriverAction
    {
        public string KeyWords { get; set; }
        public string? GeoUrn { get; set; }
        public int? PageNumber { get; set; }
        public string? LastPageContent { get; private set; }
        
        public BrowserDriverActionSearchProfileExtraction(BrowserDriver browserDriver, string KeyWords, string? GeoUrn = null, int? PageNumber = null) : base(browserDriver)
        {
            this.KeyWords = KeyWords;
            this.GeoUrn = GeoUrn;
            this.PageNumber = PageNumber;
        }

        public override async Task Run(LinkedinProfile linkedinProfile)
        {
            // Go to the page
            this.goToPeopleSearchPage();

            // Get the content
            this.LastPageContent = await this._browserDriver._page.InnerHTMLAsync("body");
            Task.Delay(Random.Shared.Next(5000, 6000)).Wait();
        }

        private void goToPeopleSearchPage()
        {
            string url = $"https://www.linkedin.com/search/results/people/?keywords={this.KeyWords}";

            if (this.PageNumber.HasValue){
                url += $"&page={this.PageNumber}";
            }

            if (!string.IsNullOrEmpty(this.GeoUrn)){
                url += $"&geoUrn=[\"{this.GeoUrn}\"]";
            }

            this.goToPage(url, 3000, 5000);
        }

        public override string ToString()
        {
            return "Action - Extraction de nouveaux profiles";
        }
    }
}
