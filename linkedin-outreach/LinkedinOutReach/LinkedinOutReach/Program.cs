using LinkedinOutReach;
using LinkedinOutReach.browserDriverAction;
using Microsoft.Extensions.Configuration;

SeriesAction linkedinOutReachManager = SeriesActionCampaign.InitializationDefault();

//var config = new ConfigurationBuilder()
//            .AddJsonFile("appsettings.local.json")
//            .Build();
//linkedinOutReachManager.Run();

SeriesActionScrappingProfile saScrappingProfile = SeriesActionScrappingProfile.InitializationDefault();

saScrappingProfile.Run();