using LinkedinOutReach.models;
using Microsoft.Playwright;
using NuGet.Protocol.Plugins;
using System.Xml.Linq;

namespace LinkedinOutReach.browserDriverAction
{
    internal abstract class BrowserDriverAction
    {
        protected BrowserDriver _browserDriver;

        public BrowserDriverAction(BrowserDriver browserDriver)
        {
            _browserDriver = browserDriver;
        }

        public abstract Task Run(LinkedinProfile linkedinProfile);

        protected async Task Click(string selector, int delayMin = 3000, int delayMax = 3500)
        {
            await this._browserDriver._page.ClickAsync(selector);
            await this._browserDriver._page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await Task.Delay(Random.Shared.Next(delayMin, delayMax));
        }

        protected async Task Fill(string selector, string value, int delayMin = 3000, int delayMax = 3500)
        {
            await this._browserDriver._page.FillAsync(selector, value);
            await this._browserDriver._page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await Task.Delay(Random.Shared.Next(delayMin, delayMax));
        }

        protected async Task fillTextInputWithRetry(string selector, string value, int maxRetries = 3, int delayMin = 500, int delayMax = 1000)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                await _browserDriver._page.FillAsync(selector, value);
                await Task.Delay(Random.Shared.Next(delayMin, delayMax));

                string currentValue = await _browserDriver._page.InputValueAsync(selector);

                if (currentValue == value)
                    break;
            }
        }

        protected async Task goToPage(string url, int delayMin = 500, int delayMax = 1000)
        {
            await this._browserDriver._page.GotoAsync(url);
            await this._browserDriver._page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            if (delayMax > 0)
            {
                await Task.Delay(Random.Shared.Next(delayMin, delayMax));
            }
        }

        protected async Task WaitElementToBeVisible(string selector, int timeout = 50000)
        {

            //await this._browserDriver._page.WaitForSelectorAsync(selector, new() { State = WaitForSelectorState.Visible, Timeout = timeout });
            await this._browserDriver._page.WaitForSelectorAsync(selector);
        }

        public override string ToString()
        {
            return "Action";
        }
    }
}
