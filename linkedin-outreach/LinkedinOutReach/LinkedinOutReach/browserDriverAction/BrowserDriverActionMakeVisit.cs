using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedinOutReach.browserDriverAction
{
    internal class BrowserDriverActionMakeVisit : BrowserDriverAction
    {
        public BrowserDriverActionMakeVisit(BrowserDriver browserDriver) : base(browserDriver) { }

        public override Task Run(LinkedinProfile linkedinProfile)
        {
            await this.goToPage(linkedinProfile.Link);
            throw new NotImplementedException();
        }
    }
}
