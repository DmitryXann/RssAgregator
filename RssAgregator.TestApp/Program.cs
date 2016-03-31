using RssAgregator.CORE.Parcers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RssAgregator.CORE.Parcers.DOMObjectModel;
using RssAgregator.CORE.Models.Enums;
using RssAgregator.DAL;
using RssAgregator.BAL.Services;

namespace RssAgregator.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var radio = new OnlineRadioService();
            //var result = radio.Search(new Models.Services.OnlineRadio.OnlineRadioServiceSearchModel { Question = "rammstein", PageNumber = 1 });
            //result.Wait();

            //var result1 = 1;
            //var eleemtn = new StringBuilder("<table id=\"inner_wrap_3843015\"\n\t\t\t\t\tclass=\"b-story inner_wrap\"\n\t\t\t\t\tcellspacing=\"0\" cellpadding=\"0\"\n\t\t\t\t\t scrolled=\"0\"\n\t\t\t\t\tattr=\"3843015\" data-story-id=\"3843015\">");

            //var domEl = new DOMElement(null, eleemtn);
            //var o1 = domEl.TagClass;
            //var o2 = domEl.TagId;
            //var o3 = domEl.TagName;
            //var o4 = domEl.BadTag;

           var olol = new RssAgregator.CORE.DataSourceProcessing();
           olol.ProcessAllActiveDataSources();

            //var vk = ParcerProviderFactory.GetFactory(DataSourceEnum.VK);

            //var resutl3 = vk.GetContent(new Uri("http://vk.com/wall-10639516"));
            //resutl3.Wait();

            //var pikabu = ParcerProviderFactory.GetFactory(ParcerTypeEnum.Pikabu);

            //var resutl = pikabu.GetContent(new Uri("http://pikabu.ru/"));
            //resutl.Wait();

            //var mainfun = ParcerProviderFactory.GetFactory(ParcerTypeEnum.Mainfun);
            //var resutl = mainfun.GetContent(new Uri("http://mainfun.ru/"));
            //resutl.Wait();

            //var zaycev = ParcerProviderFactory.GetFactory(DataSourceEnum.Zaycev);
            //zaycev.AddSearchCriteria("rammstein");
            //var resutl4 = zaycev.GetContent(new Uri("http://zaycev.net/search.html"));
            //resutl4.Wait();
        }
    }
}
