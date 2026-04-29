using LinkedinOutReach.browserDriverAction;
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
        private List<BrowserDriverAction> _steps;
        private BrowserDriverAction _actionSelected;

        public BrowserDriver()
        {
            initBrowser().Wait();
        }

        public void SetAction(BrowserDriverAction action)
        {
            this._actionSelected = action;
        }

        public void Run(LinkedinProfile linkedinProfile)
        {
            this.RunStep(linkedinProfile, this._actionSelected).Wait();
        }

        public async Task RunSteps(LinkedinProfile linkedinProfile)
        {
            foreach (BrowserDriverAction step in this._steps)
            {
                await this.RunStep(linkedinProfile, step);
            }
        }

        public async Task RunStep(LinkedinProfile linkedinProfile, BrowserDriverAction step)
        {
            Console.WriteLine($"{step} Tentative");

            await step.Run(linkedinProfile);
            
            Console.WriteLine($"{step} OK");
        }

        public async Task closeBrowser()
        {
            await _browser.CloseAsync();
            Console.WriteLine("Browser closed !");
        }

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
            Console.WriteLine("Browser initialized !");
        }
    }
}
