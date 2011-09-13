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
            var constituentsData = HttpHelper.Get<SearchResultsData>(string.Format(serviceBaseUri + "/Search?firstname={0}&lastname={1}", firstName,lastName));

            mapper = new AutoDataContractMapper();
            var constituents = new SearchResults();
            mapper.MapList(constituentsData, constituents,typeof(SearchResult));
            ViewData["Constituents"] = constituents;

            return this.Json(constituents);
        }


        [GridAction]
        public ActionResult AllPhones(int constituentId)
        {
            return PartialView(new GridModel(GetPhones(constituentId)));
        }

        private Phones GetPhones(int constituentId)
        {
            var phonesData = HttpHelper.Get<PhonesData>(string.Format(serviceBaseUri + "/Phones?ConstituentId={0}", constituentId));

            mapper = new AutoDataContractMapper();
            var phones = new Phones();
            mapper.MapList(phonesData, phones, typeof(Phone));
            return phones;
        }

        private Addresses GetAddress(int constituentId)
        {
            var addressesData = HttpHelper.Get<AddressesData>(string.Format(serviceBaseUri + "/Addresses?ConstituentId={0}", constituentId));

            mapper = new AutoDataContractMapper();
            var addresses = new Addresses();
            mapper.MapList(addressesData, addresses, typeof(Address));
            return addresses;
        }

        [GridAction]
        public ActionResult AllAddresses(int constituentId)
        {
            return View(new GridModel(GetAddress(constituentId)));
        }
    }

}