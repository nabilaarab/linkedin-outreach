using LinkedinOutReach.browserDriverAction;
using LinkedinOutReach.models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedinOutReach
{
    internal abstract class SeriesAction
    {
        protected readonly BrowserDriver _browserDriver;
        protected readonly LinkedinProfile[] _linkedinProfiles;
        protected readonly List<BrowserDriverAction> _actionDesired;
        protected readonly int _minTimeBetweenActions;
        protected readonly int _maxTimeBetweenActions;
        
        public SeriesAction(BrowserDriver browserDriver, LinkedinProfile[] linkedinProfiles, List<BrowserDriverAction> actionDesired, int minTimeBetweenActions, int maxTimeBetweenActions)
        {
            this._browserDriver = browserDriver;
            this._linkedinProfiles = linkedinProfiles;
            this._actionDesired = actionDesired;
            this._minTimeBetweenActions = minTimeBetweenActions;
            this._maxTimeBetweenActions = maxTimeBetweenActions;
        }

        public abstract void Run();

        protected void waitRandomTime()
        {
            int delay = Random.Shared.Next(this._minTimeBetweenActions, this._maxTimeBetweenActions);
            Console.WriteLine($"Waiting for {delay} ms before processing the next action...");
            Task.Delay(delay).Wait();
        }
    }
}
