using LinkedinOutReach;

Console.WriteLine("Hello, World!");
ExcelManager excelManager = new ExcelManager();

excelManager.loadLinkedinProfiles();

BrowserDriver browserDriver = new BrowserDriver();

await browserDriver.initBrowser();

await browserDriver.connectToLinkedin("", "");

await browserDriver.sendConnexion(excelManager._linkedinProfiles[0]);

Console.WriteLine();