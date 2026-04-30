using LinkedinOutReach.exceptions;
using LinkedinOutReach.models;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace LinkedinOutReach.browserDriverAction
{
    internal class BrowserDriverActionConnexionLinkedin : BrowserDriverAction
    {
        private readonly string _email;
        private readonly string _password;

        public BrowserDriverActionConnexionLinkedin(BrowserDriver browserDriver, string email, string password) : base(browserDriver)
        {
            this._email = email;
            this._password = password;
        }

        public override async Task Run(LinkedinProfile linkedinProfile)
        {
            // Go to LinkedIn login page
            await this.goToPage("https://www.linkedin.com/login/", 3000, 5000);

            // Fill form and submit
            await this.FillAndSubmitForm();

            // Check if login was successful by verifying the URL
            this.CheckIfLoginSuccess();
        }

        private async Task FillAndSubmitForm()
        {
            await this.WaitElementToBeVisible("#username");
            await this.Click("#username");
            await this.fillTextInputWithRetry("#username", this._email);

            await this.WaitElementToBeVisible("#password");
            await this.Click("#password");
            await this.fillTextInputWithRetry("#password", this._password);

            await this.Click("button[type='submit']");
            await this._browserDriver._page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        private void CheckIfLoginSuccess()
        {
            string currentUrl = this._browserDriver._page.Url;
            if (!currentUrl.Contains("feed"))
            {
                string errorMessage = $"[BrowserDriverActionConnexionLinkedin] Failed to login, current URL: {currentUrl}";
                Console.WriteLine(errorMessage);
                throw new LinkedinLoginFailedException(errorMessage);
            }
        }

        public override string ToString()
        {
            return "Action - Connexion à Linkedin";
        }
    }
}
