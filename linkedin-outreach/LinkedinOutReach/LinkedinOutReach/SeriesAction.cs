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
        protected string _email;
        protected string _password;

        public SeriesAction(BrowserDriver browserDriver, LinkedinProfile[] linkedinProfiles, List<BrowserDriverAction> actionDesired, int minTimeBetweenActions, int maxTimeBetweenActions, string email, string password)
        {
            this._browserDriver = browserDriver;
            this._linkedinProfiles = linkedinProfiles;
            this._actionDesired = actionDesired;
            this._minTimeBetweenActions = minTimeBetweenActions;
            this._maxTimeBetweenActions = maxTimeBetweenActions;
            this._email = email;
            this._password = password;
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
