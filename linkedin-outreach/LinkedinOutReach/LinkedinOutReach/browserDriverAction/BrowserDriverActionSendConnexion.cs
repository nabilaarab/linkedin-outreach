using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedinOutReach.browserDriverAction
{
    internal class BrowserDriverActionSendConnexion : BrowserDriverAction
    {
        public string _message { get; set; }

        public BrowserDriverActionSendConnexion(BrowserDriver browserDriver, string message) : base(browserDriver) { 
            this._message = message;
        }

        public override async Task Run(LinkedinProfile linkedinProfile)
        {
            // Go to the invite page
            string link = "https://www.linkedin.com/preload/custom-invite/?vanityName=" + linkedinProfile.Id;
            await this.goToPage(link);

            // Without note if wanted
            if (this._message == "")
            {
                await sendConnexionWithoutNote();

                return;
            }

            // Add note if wanted
            await sendConnexionWithNote(this._message);

            //await this._browser_page.ClickAsync("span:has-text('Connect')");
            //await Task.Delay(Random.Shared.Next(500, 1000)); // Attendre un peu pour simuler un comportement humain

            await Task.Delay(Random.Shared.Next(1000, 2000));
        }

        private async Task sendConnexionWithoutNote()
        {
            await this.Click("span:has-text('Envoyer sans note')");
        }

        private async Task sendConnexionWithNote(string message)
        {
            // Add note if wanted
            await this.Click("span:has-text('Ajouter une note')");

            // Fill the textarea
            await this.Fill("textarea[name='message']", message);

            // Send the connexion
            await this.Click("span:has-text('Envoyer')");
        }
    }
}
