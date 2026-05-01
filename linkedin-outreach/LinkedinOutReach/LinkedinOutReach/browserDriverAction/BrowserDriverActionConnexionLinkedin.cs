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
            string xPath = "xpath=//*[@id='username' or @id=':r3:']";
            await this.WaitElementToBeVisible(xPath);
            await this.Click(xPath, 0, 100);
            await this.fillTextInputWithRetry(xPath, this._email, 3, 0, 100);

            xPath = "xpath=//*[@id='password' or @id=':r4:']";
            await this.WaitElementToBeVisible(xPath);
            await this.Click(xPath, 0, 100);
            await this.fillTextInputWithRetry(xPath, this._password, 3, 0, 100);

            //await this.Click("xpath=//button[@type='submit' or @type='button']");
            //xPath = "xpath=//button[normalize-space()='Sign in'] | (//button)[4]";
            xPath = "xpath=//button[@type='submit'] | (//button[@type='button' and .//span/span[normalize-space(text())='Sign in']])[2]";
            //xPath = "xpath=//button[@type='submit']";
            await this.Click(xPath);
            //await this._browserDriver._page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            //this.clickSignInButton();
        }

        private void clickSignInButton()
        {
            string xPath = "xpath=(//button[@type='button' and .//span/span[normalize-space(text())='Sign in']])[2]";
            xPath += " | //button[type='submit']";

            var signInButton = this._browserDriver._page.Locator(xPath);

            signInButton.ScrollIntoViewIfNeededAsync().Wait();
            signInButton.ClickAsync().Wait();
            this._browserDriver._page.WaitForLoadStateAsync(LoadState.DOMContentLoaded).Wait();
            Task.Delay(Random.Shared.Next(3000, 5000)).Wait();
        }

        private void CheckIfLoginSuccess()
        {
            string currentUrl = this._browserDriver._page.Url;
            if (currentUrl.Contains("checkpoint"))
            {
                Task.Delay(Random.Shared.Next(30000, 50000)).Wait();
            }
            else if (!currentUrl.Contains("feed"))
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
