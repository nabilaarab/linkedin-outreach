using LinkedinOutReach.exceptions;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

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
            await this.goToPage("https://www.linkedin.com/login/");

            // Fill form and submit
            await this.fillAndSubmitForm();

            // Check if login was successful by verifying the URL
            this.checkIfLoginSuccess();
        }

        private async Task fillAndSubmitForm()
        {
            await this.fillTextInputWithRetry("#username", this._email);
            await this.fillTextInputWithRetry("#password", this._password);

            await this._browserDriver._page.ClickAsync("button[type='submit']");
            await this._browserDriver._page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        private void checkIfLoginSuccess()
        {
            string currentUrl = this._browserDriver._page.Url;
            if (!currentUrl.Contains("feed"))
            {
                string errorMessage = $"[BrowserDriverActionConnexionLinkedin] Failed to login, current URL: {currentUrl}";
                Console.WriteLine(errorMessage);
                throw new LinkedinLoginFailedException(errorMessage);
            }
        }
    }
}
