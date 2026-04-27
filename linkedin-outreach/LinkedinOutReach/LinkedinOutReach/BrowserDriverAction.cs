using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedinOutReach
{
    internal abstract class BrowserDriverAction
    {
        private BrowserDriver _browserDriver;

        public BrowserDriverAction(BrowserDriver browserDriver)
        {
            _browserDriver = browserDriver;
        }

        private async Task fillTextInputWithRetry(string selector, string value, int maxRetries = 3, int delayMin = 500, int delayMax = 1000)
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
    }
}
