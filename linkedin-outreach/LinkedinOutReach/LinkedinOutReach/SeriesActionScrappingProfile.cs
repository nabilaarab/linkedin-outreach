using LinkedinOutReach.browserDriverAction;
using LinkedinOutReach.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedinOutReach
{
    internal class SeriesActionScrappingProfile : SeriesAction
    {
        public SeriesActionScrappingProfile(BrowserDriver browserDriver, LinkedinProfile[] linkedinProfiles, List<BrowserDriverAction> actionDesired, int minTimeBetweenActions, int maxTimeBetweenActions) : 
            base(browserDriver, linkedinProfiles, actionDesired, minTimeBetweenActions, maxTimeBetweenActions) {}

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
