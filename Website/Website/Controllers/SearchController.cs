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
        private string firstName;
        private string lastName;

        public ActionResult Index()
        {
//            if(Session["userName"]==null)
//                FormsAuthentication.RedirectToLoginPage();
            return View();
        }

   
        public JsonResult Search(SearchModel searchCriteria)
        {
            firstName = searchCriteria.FirstName;
            lastName = searchCriteria.LastName;
            var constituentsData = HttpHelper.Get<ConstituentsData>(string.Format(serviceBaseUri + "/Search?firstname={0}&lastname={1}", firstName,lastName));

            mapper = new AutoDataContractMapper();
            var constituents = new Constituents();
            mapper.MapList(constituentsData, constituents,typeof(Constituent));
            ViewData["Constituents"] = constituents;

            return this.Json(constituents);
        }
    }

}