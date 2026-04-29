using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedinOutReach.browserDriverAction
{
    internal class BrowserDriverActionMakeVisit : BrowserDriverAction
    {
        public BrowserDriverActionMakeVisit(BrowserDriver browserDriver) : base(browserDriver) { }

        public override async Task Run(LinkedinProfile linkedinProfile)
        {
            await this.goToPage(linkedinProfile.Link);
        }

        public override string ToString()
        {
            return "Action - Visite";
        }
    }
}
