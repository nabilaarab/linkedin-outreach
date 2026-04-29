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

        public BrowserDriver(string email, string password)
        {
            this._steps = new List<BrowserDriverAction>();
            this._steps.Add(new BrowserDriverActionConnexionLinkedin(this, email, password));
            this._steps.Add(new BrowserDriverActionMakeVisit(this));
            this._steps.Add(new BrowserDriverActionSendConnexion(this, "Ceci est un message"));
        }

        public async Task RunSteps(LinkedinProfile linkedinProfile)
        {
            foreach (BrowserDriverAction step in this._steps)
            {
                Console.WriteLine($"{step} Tentative");

                await step.Run(linkedinProfile);

                Console.WriteLine($"{step} OK");
            }
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
