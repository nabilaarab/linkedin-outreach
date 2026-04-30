using LinkedinOutReach.browserDriverAction;
using LinkedinOutReach.models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedinOutReach
{
    internal class SeriesActionCampaign : SeriesAction
    {
        private readonly string _email;
        private readonly string _password;
        public SeriesActionCampaign(
            BrowserDriver browserDriver, 
            LinkedinProfile[] linkedinProfiles, 
            List<BrowserDriverAction> actionDesired, 
            int minTimeBetweenActions, 
            int maxTimeBetweenActions,
            string email,
            string password) : base(
                browserDriver, 
                linkedinProfiles, 
                actionDesired, 
                minTimeBetweenActions, 
                maxTimeBetweenActions

        ){
            this._email = email;
            this._password = password;
        }

        public static SeriesActionCampaign InitializationDefault()
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

            return new SeriesActionCampaign(browserDriver, excelManager.LinkedinProfiles, actionDesired, 3000, 6000, config["Linkedin:Email"], config["Linkedin:Password"]);
        }

        public override void Run()
        {
            // First, connect to LinkedIn using the browser driver
            this._browserDriver.SetAction(
                new BrowserDriverActionConnexionLinkedin(this._browserDriver, this._email, this._password)
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
    }
}
