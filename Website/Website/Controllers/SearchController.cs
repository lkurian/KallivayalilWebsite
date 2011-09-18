using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;
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
            if(Session["userName"]==null)
                FormsAuthentication.RedirectToLoginPage();
            return View();
        }

   
        public JsonResult Search(SearchModel searchCriteria)
        {
            var uriString = string.Format(serviceBaseUri + @"/Search?firstName={0}&lastName={1}&email={2}&phone={3}&occupationName={4}&occupationDescription={5}&instituteName={6}&instituteLocation={7}&qualification={8}&yearOfGradutation={9}&address={10}&state={11}&city={12}&country={13}&postcode={14}&preferedName={15}&houseName={16}&branch={17}"
                                          ,searchCriteria.FirstName, searchCriteria.LastName,searchCriteria.Email,searchCriteria.Phone,searchCriteria.OccupationName,searchCriteria.OccupationDescription
                                          ,searchCriteria.InstituteName,searchCriteria.InstituteLocation,searchCriteria.Qualification,searchCriteria.YearOfGraduation
                                          ,searchCriteria.Address,searchCriteria.State,searchCriteria.City,searchCriteria.Country,searchCriteria.Postcode
                                          ,searchCriteria.PreferedName,searchCriteria.HouseName,searchCriteria.Branch);
            var constituentsData = HttpHelper.Get<ConstituentsData>(uriString);

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

        [GridAction]
        public ActionResult AllAssociations(int constituentId)
        {
            return View(new GridModel(GetAssociations(constituentId)));
        }

        private Associations GetAssociations(int constituentId)
        {
            var associationsData = HttpHelper.Get<AssociationsData>(string.Format(serviceBaseUri + "/Associations?ConstituentId={0}", constituentId));

            mapper = new AutoDataContractMapper();
            var associations = new Associations();
            mapper.MapList(associationsData, associations, typeof(Association));
            return associations;
        }
    }

}