using LinkedinOutReach;
using LinkedinOutReach.browserDriverAction;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.local.json")
    .Build();

Console.WriteLine("Hello, World!");
Console.WriteLine(config["Linkedin:Email"]);
Console.WriteLine(config["Linkedin:Password"]);

ExcelManager excelManager = new ExcelManager();

excelManager.loadLinkedinProfiles();
Console.WriteLine("Excel chargé !");

BrowserDriver browserDriver = new BrowserDriver(config["Linkedin:Email"], config["Linkedin:Password"]);

await browserDriver.initBrowser();
await browserDriver.RunSteps(excelManager._linkedinProfiles[0]);

Console.WriteLine();
