using LinkedinOutReach.browserDriverAction;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedinOutReach
{
    internal class LinkedinOutReachManager
    {
        private readonly BrowserDriver _browserDriver;
        private readonly LinkedinProfile[] _linkedinProfiles;
        private readonly List<BrowserDriverAction> _actionDesired;
        private readonly int _minTimeBetweenActions;
        private readonly int _maxTimeBetweenActions;
        
        public LinkedinOutReachManager(BrowserDriver browserDriver, LinkedinProfile[] linkedinProfiles, List<BrowserDriverAction> actionDesired, int minTimeBetweenActions, int maxTimeBetweenActions)
        {
            this._browserDriver = browserDriver;
            this._linkedinProfiles = linkedinProfiles;
            this._actionDesired = actionDesired;
            this._minTimeBetweenActions = minTimeBetweenActions;
            this._maxTimeBetweenActions = maxTimeBetweenActions;
        }

        public static LinkedinOutReachManager Initialization()
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
            actionDesired.Add(new BrowserDriverActionVisitProfile(browserDriver));
            actionDesired.Add(new BrowserDriverActionSendConnexion(browserDriver, "Hola !!!!"));

            return new LinkedinOutReachManager(browserDriver, excelManager.LinkedinProfiles, actionDesired, 3000, 6000);
        }

        public void RunCampaign(string email, string password)
        {
            // First, connect to LinkedIn using the browser driver
            this._browserDriver.SetAction(
                new BrowserDriverActionConnexionLinkedin(this._browserDriver, email, password)
            );
            this._browserDriver.Run(null);

            // Second, run actions for each Linkedin profile
            foreach (LinkedinProfile linkedinProfile in this._linkedinProfiles)
            {
                
                foreach (BrowserDriverAction action in _actionDesired)
                {
                    // Realize the action for the current profile
                    this._browserDriver.SetAction(action);
                    this._browserDriver.Run(linkedinProfile);

                    // Wait a random time between actions to simulate human behavior
                    this.waitRandomTime();
                }
                
                Console.WriteLine($"Campaign completed for {linkedinProfile.Name} profile.");
            }
        }

        private void waitRandomTime()
        {
            int delay = Random.Shared.Next(this._minTimeBetweenActions, this._maxTimeBetweenActions);
            Console.WriteLine($"Waiting for {delay} ms before processing the next action...");
            Task.Delay(delay).Wait();
        }
    }
}
