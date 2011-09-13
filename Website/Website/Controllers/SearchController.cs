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

        [GridAction]
        public ActionResult AllEmails(int constituentId)
        {
            return View(new GridModel(GetEmails(constituentId)));
        }

        private Emails GetEmails(int constituentId)
        {
            var emailsData = HttpHelper.Get<EmailsData>(serviceBaseUri + "/Emails?ConstituentId=" + constituentId);

            mapper = new AutoDataContractMapper();
            var emails = new Emails();
            mapper.MapList(emailsData, emails, typeof(Email));
            return emails;
        }

        [GridAction]
        public ActionResult AllPhones(int constituentId)
        {
            return View(new GridModel(GetPhones(constituentId)));
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

        [GridAction]
        public ActionResult AllEducationDetails(int constituentId)
        {
            return View(new GridModel(GetEducations(constituentId)));
        }

        private EducationDetails GetEducations(int constituentId)
        {
            var educationDetailsData = HttpHelper.Get<EducationDetailsData>(string.Format(serviceBaseUri + "/EducationDetails?ConstituentId={0}", constituentId));

            mapper = new AutoDataContractMapper();
            var educations = new EducationDetails();
            mapper.MapList(educationDetailsData, educations, typeof(EducationDetail));
            return educations;
        }


        [GridAction]
        public ActionResult AllOccupations(int constituentId)
        {
            return PartialView(new GridModel(GetOccupations(constituentId)));
        }

        private Occupations GetOccupations(int constituentId)
        {
            var OccupationsData = HttpHelper.Get<OccupationsData>(string.Format(serviceBaseUri + "/Occupations?ConstituentId={0}", constituentId));

            mapper = new AutoDataContractMapper();
            var Occupations = new Occupations();
            mapper.MapList(OccupationsData, Occupations, typeof(Occupation));
            return Occupations;
        }
    }

}