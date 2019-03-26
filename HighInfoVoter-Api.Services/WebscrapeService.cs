using HighInfoVoter_Api.Models.Domain;
using HighInfoVoter_Api.Services.Interfaces;
using HtmlAgilityPack;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;

namespace HighInfoVoter_Api.Services
{
    public class WebscrapeService : IWebscrapeService
    {
        private const string _url = "https://ballotpedia.org/";

        public Portrait Webscrape(string name)
        {
            string formattedName = name.Replace(" ", "_");
            var web = new HtmlWeb();
            var doc = web.Load(_url + formattedName);
            var imageURL = "";
            try
            {
                imageURL = HttpUtility.HtmlDecode(doc.DocumentNode.SelectSingleNode(".//img[@class = 'bp-person-image']").GetAttributeValue("src", ""));
            }
            catch (Exception ex1)
            {
                try
                {
                    imageURL = HttpUtility.HtmlDecode(doc.DocumentNode.SelectSingleNode(".//img[@class = 'widget-img']").GetAttributeValue("src", ""));
                }
                catch (Exception ex2)
                {
                    throw new Exception(ex2.Message);
                }
            }

            Portrait portrait = new Portrait()
            {
                Url = imageURL
            };

            using (WebClient webClient = new WebClient())
            {
                webClient.UseDefaultCredentials = true;
                webClient.DownloadFile(new Uri(imageURL), @"c:\scrapings\" + imageURL.Substring(imageURL.LastIndexOf('/') + 1));
            }

            return portrait;
        }

        public void ScrapeAllBPPortraits()
        {
            var web = new HtmlWeb();
            //Senators
            var doc = web.Load("https://www.congress.gov/members?pageSize=250&q=%7B%22congress%22%3A%5B%22116%22%5D%2C%22chamber%22%3A%22Senate%22%7D");
            foreach (var link in doc.DocumentNode.SelectNodes(".//div[@class = 'member-image']"))
            {
                string imageURL = link.FirstChild.GetAttributeValue("src", "");
                string altValue = link.FirstChild.GetAttributeValue("alt", "").Replace(",", "").Replace(".", "");
                string firstLast = altValue.Split()[1] + "_" + altValue.Split()[0];

                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri("https://www.congress.gov" + imageURL), @"c:\scrapings\" + firstLast + ".jpg");
                }
            }
            //House Page 1
            doc = web.Load("https://www.congress.gov/members?pageSize=250&q=%7B%22congress%22%3A%5B%22116%22%5D%2C%22chamber%22%3A%22House%22%7D&page=1");
            foreach (var link in doc.DocumentNode.SelectNodes(".//div[@class = 'member-image']"))
            {
                string imageURL = link.FirstChild.GetAttributeValue("src", "");
                string altValue = link.FirstChild.GetAttributeValue("alt", "").Replace(",", "").Replace(".", "");
                string firstLast = altValue.Split()[1] + "_" + altValue.Split()[0];

                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri("https://www.congress.gov" + imageURL), @"c:\scrapings\" + firstLast + ".jpg");
                }
            }
            //House Page 2
            doc = web.Load("https://www.congress.gov/members?pageSize=250&q=%7B%22congress%22%3A%5B%22116%22%5D%2C%22chamber%22%3A%22House%22%7D&page=2");
            foreach (var link in doc.DocumentNode.SelectNodes(".//div[@class = 'member-image']"))
            {
                string imageURL = link.FirstChild.GetAttributeValue("src", "");
                string altValue = link.FirstChild.GetAttributeValue("alt", "").Replace(",", "").Replace(".", "");
                string firstLast = altValue.Split()[1] + "_" + altValue.Split()[0];

                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri("https://www.congress.gov" + imageURL), @"c:\scrapings\" + firstLast + ".jpg");
                }
            }
        }
    }
}
