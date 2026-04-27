using Microsoft.Playwright;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedinOutReach
{
    internal class BrowserDriver
    {
        public IPlaywright _playwright { get; private set; } = null!;
        public IBrowser _browser { get; private set; } = null!;
        public IPage _page { get; private set; } = null!;

        public async Task initBrowser()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new() { 
                    Headless = false,
                    Args = new[] { "--lang=en-US" }
                }
            );
            _page = await _browser.NewPageAsync(
                new()
                {
                    Locale = "en-US"
                }
            );
        }

        public async Task connectToLinkedin(string email, string password)
        {
            await _page.GotoAsync("https://www.linkedin.com/login/");
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await Task.Delay(Random.Shared.Next(500, 1000));


            await fillTextInputWithRetry("#username", email);
            await fillTextInputWithRetry("#password", password);

            await _page.ClickAsync("button[type='submit']");
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        public async Task makeVisit(LinkedinProfile linkedinProfile)
        {
            await _page.GotoAsync(linkedinProfile.Link);
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        //https://www.linkedin.com/preload/custom-invite/?vanityName=[name]
        public async Task sendConnexion(LinkedinProfile linkedinProfile, string message)
        {
            // Go to the invite page
            string link = "https://www.linkedin.com/preload/custom-invite/?vanityName=" + linkedinProfile.Id;
            await _page.GotoAsync(link);
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await Task.Delay(Random.Shared.Next(1000, 1500));

            // Without note if wanted
            if (message == "") {
                await sendConnexionWithoutNote();

                return;
            }

            // Add note if wanted
            await sendConnexionWithNote(message);

            //await _page.ClickAsync("span:has-text('Connect')");
            //await Task.Delay(Random.Shared.Next(500, 1000)); // Attendre un peu pour simuler un comportement humain

            await Task.Delay(Random.Shared.Next(1000, 2000));
        }

        private async Task sendConnexionWithoutNote()
        {
            await _page.ClickAsync("span:has-text('Envoyer sans note')");
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await Task.Delay(Random.Shared.Next(3000, 3500));
        }

        private async Task sendConnexionWithNote(string message)
        {
            // Add note if wanted
            await _page.ClickAsync("span:has-text('Ajouter une note')");
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await Task.Delay(Random.Shared.Next(3000, 3500));

            // Fill the textarea
            await _page.FillAsync("textarea[name='message']", message);
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await Task.Delay(Random.Shared.Next(3000, 3500));

            // Send the connexion
            await _page.ClickAsync("span:has-text('Envoyer')");
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await Task.Delay(Random.Shared.Next(3000, 3500));
        }

        public void sendMessage(LinkedinProfile linkedinProfile)
        {
            throw new NotImplementedException();
        }

        private async Task fillTextInputWithRetry(string selector, string value, int maxRetries = 3, int delayMin=500, int delayMax=1000)
        {
            for(int i = 0 ; i < maxRetries ; i++)
            {
                await _page.FillAsync(selector, value);
                await Task.Delay(Random.Shared.Next(delayMin, delayMax));

                string currentValue = await _page.InputValueAsync(selector);

                if (currentValue == value)
                    break;
            }
        }
    }
}
