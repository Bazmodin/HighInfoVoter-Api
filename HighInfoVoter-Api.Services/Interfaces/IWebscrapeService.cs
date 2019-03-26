using HighInfoVoter_Api.Models.Domain;

namespace HighInfoVoter_Api.Services.Interfaces
{
    public interface IWebscrapeService
    {
        Portrait Webscrape(string name);
        void ScrapeAllBPPortraits();
    }
}
