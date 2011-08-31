using System.Configuration;
using System.Web.Mvc;
using Kallivayalil.Client;
using Telerik.Web.Mvc;
using Website.Helpers;
using Website.Models;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class SearchController : Controller
    {
        private AutoDataContractMapper mapper = new AutoDataContractMapper();
        private string serviceBaseUri = ConfigurationManager.AppSettings["serviceBaseUri"];

        public ActionResult Index()
        {
//            if(Session["userName"]==null)
//                FormsAuthentication.RedirectToLoginPage();
            return View();
        }

     

        [HttpPost]
        public ActionResult Search(SearchModel searchCriteria)
        {
            var constituentsData = HttpHelper.Get<ConstituentsData>(string.Format(serviceBaseUri + "/Search?firstname={0}&lastname={1}", searchCriteria.FirstName,searchCriteria.LastName));

            mapper = new AutoDataContractMapper();
            var constituents = new Constituents();
            mapper.MapList(constituentsData, constituents,typeof(Constituent));
            return View(new GridModel(constituents));
        }
    }

}