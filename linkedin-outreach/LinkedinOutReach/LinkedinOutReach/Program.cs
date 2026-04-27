using LinkedinOutReach;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.local.json")
    .Build();

Console.WriteLine("Hello, World!");
Console.WriteLine(config["Linkedin:Email"]);
Console.WriteLine(config["Linkedin:Password"]);
ExcelManager excelManager = new ExcelManager();

excelManager.loadLinkedinProfiles();

BrowserDriver browserDriver = new BrowserDriver();

await browserDriver.initBrowser();

await browserDriver.connectToLinkedin(config["Linkedin:Email"], config["Linkedin:Password"]);

await browserDriver.makeVisit(excelManager._linkedinProfiles[0]);

await browserDriver.sendConnexion(excelManager._linkedinProfiles[0], "Ceci est un message");

Console.WriteLine();
