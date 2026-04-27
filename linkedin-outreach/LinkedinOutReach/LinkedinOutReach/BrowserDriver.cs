using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedinOutReach
{
    internal class BrowserDriver
    {
        private IPlaywright _playwright = null!;
        private IBrowser _browser = null!;
        private IPage _page = null!;

        public async Task initBrowser() 
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = false });
            _page = await _browser.NewPageAsync();
        }

        public async Task connectToLinkedin(string email, string password)
        {
            await _page.GotoAsync("https://www.linkedin.com/login/");
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            await _page.FillAsync("#username", email);
            await Task.Delay(Random.Shared.Next(500, 1000));

            await _page.FillAsync("#password", password);
            await Task.Delay(Random.Shared.Next(500, 1000));

            await _page.ClickAsync("button[type='submit']");
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }

        public async Task makeVisit(LinkedinProfile linkedinProfile) {
            await _page.GotoAsync(linkedinProfile.Link);
        }
        //https://www.linkedin.com/preload/custom-invite/?vanityName=[name]
        public async Task sendConnexion(LinkedinProfile linkedinProfile) {
            // Aller sur le profil
            string link = "https://www.linkedin.com/preload/custom-invite/?vanityName=" + linkedinProfile.Id;
            await _page.GotoAsync(link);
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await Task.Delay(Random.Shared.Next(1000, 1500));

            // Cliquer sur "Se connecter"
            await _page.ClickAsync("span:has-text('Send without a note')");
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await Task.Delay(Random.Shared.Next(1000, 1500));

            //await _page.ClickAsync("span:has-text('Connect')");
            //await Task.Delay(Random.Shared.Next(500, 1000)); // Attendre un peu pour simuler un comportement humain

            await Task.Delay(Random.Shared.Next(1000, 2000));
        }

        public void sendMessage(LinkedinProfile linkedinProfile) {

        }
    }
}
