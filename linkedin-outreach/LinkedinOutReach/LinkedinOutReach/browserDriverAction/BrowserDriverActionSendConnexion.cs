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
            // Implementation for sending connection request
        }
        
        public async Task sendConnexion(LinkedinProfile linkedinProfile, string message)
        {
            // Go to the invite page
            string link = "https://www.linkedin.com/preload/custom-invite/?vanityName=" + linkedinProfile.Id;
            await this.goToPage(link);

            // Without note if wanted
            if (message == "")
            {
                await sendConnexionWithoutNote();

                return;
            }

            // Add note if wanted
            await sendConnexionWithNote(message);

            //await this._browser_page.ClickAsync("span:has-text('Connect')");
            //await Task.Delay(Random.Shared.Next(500, 1000)); // Attendre un peu pour simuler un comportement humain

            await Task.Delay(Random.Shared.Next(1000, 2000));
        }

        private async Task sendConnexionWithoutNote()
        {
            await this._browserDriver._page.ClickAsync("span:has-text('Envoyer sans note')");
            await this._browserDriver._page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await Task.Delay(Random.Shared.Next(3000, 3500));
        }

        private async Task sendConnexionWithNote(string message)
        {
            // Add note if wanted
            await this._browserDriver._page.ClickAsync("span:has-text('Ajouter une note')");
            await this._browserDriver._page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await Task.Delay(Random.Shared.Next(3000, 3500));

            // Fill the textarea
            await this._browserDriver._page.FillAsync("textarea[name='message']", message);
            await this._browserDriver._page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await Task.Delay(Random.Shared.Next(3000, 3500));

            // Send the connexion
            await this._browserDriver._page.ClickAsync("span:has-text('Envoyer')");
            await this._browserDriver._page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await Task.Delay(Random.Shared.Next(3000, 3500));
        }
    }
}
