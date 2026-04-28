using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
