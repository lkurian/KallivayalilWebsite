using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Kallivayalil.Client;
using Telerik.Web.Mvc;
using Website.Helpers;
using Website.Models;
using Website.Models.ReferenceData;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class ConstituentController : Controller
    {
        private AutoDataContractMapper mapper = new AutoDataContractMapper();
        private string serviceBaseUri = ConfigurationManager.AppSettings["serviceBaseUri"];
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["userName"] == null)
                FormsAuthentication.RedirectToLoginPage();
            PopulateBranchTypes();
            return PartialView(GetConstituent());
        }

        private void PopulateBranchTypes()
        {
            var branchTypesData = HttpHelper.Get<BranchTypesData>(serviceBaseUri + "/BranchTypes");

            var branchTypes = new BranchTypes();
            mapper.MapList(branchTypesData, branchTypes, typeof(BranchType));
            ViewData["branchTypes"] = branchTypes;
        }

        [GridAction]
        public ActionResult AllConstituentDetails()
        {
            return PartialView(GetConstituent());
        }

        private Constituent GetConstituent()
        {
            var constituentId = (int)Session["constituentId"];
            var constituentData = HttpHelper.Get<ConstituentData>(string.Format(serviceBaseUri+"/Constituents/{0}", constituentId));

            mapper = new AutoDataContractMapper();
            var constituent = new Constituent();
            mapper.Map(constituentData, constituent);
            return constituent;
        }

//        [AcceptVerbs(HttpVerbs.Post)]
//        [GridAction]
//        public ActionResult Edit(int id)
//        {
//            var constituent = new Constituent();
//
//            TryUpdateModel(constituent);
//            
//            mapper = new AutoDataContractMapper();
//            var constituentData = new ConstituentData();
//            mapper.Map(constituent, constituentData);
//
//            HttpHelper.Put(string.Format(serviceBaseUri+"/Constituents/{0}",id),constituentData);
//            return PartialView(GetConstituent());
//        }

        [HttpPost]
        public ActionResult Save(ConstituentInputModel constituent)
        {
            var constituentToSave = new Constituent();

            constituentToSave.Name = new ConstituentName()
            {
                Id = constituent.NameId,
                FirstName = constituent.FirstName,
                MiddleName = constituent.MiddleName,
                LastName = constituent.LastName,
                CreatedBy = constituent.CreatedBy,
                CreatedDateTime = constituent.CreatedDateTime,
                PreferedName = "temp",
                Salutation = new SalutationType() { Id = 1 }

            };
            constituentToSave.HouseName = constituent.HouseName;
            constituentToSave.BranchName = new BranchType { Id = constituent.BranchName };
            constituentToSave.Gender = constituent.Gender;
            constituentToSave.MaritialStatus = constituent.MaritalStatus;
            constituentToSave.HasExpired = constituent.HasExpired;
            constituentToSave.IsRegistered = constituent.IsRegistered;
            constituentToSave.CreatedDateTime = constituent.CreatedDateTime;
            constituentToSave.CreatedBy = constituent.CreatedBy;
            constituentToSave.BornOn = constituent.BornOn;
            constituentToSave.Id = (int)Session["constituentId"];

            var mapper = new AutoDataContractMapper();
            var constituentData = new ConstituentData();
            mapper.Map(constituentToSave, constituentData);

            ConstituentData data = HttpHelper.Put(string.Format(serviceBaseUri + "/Constituents/{0}", Session["constituentId"]), constituentData);
            var savedConstituent = new Constituent();
            mapper.Map(data, savedConstituent);
            return RedirectToAction("Index","Home");
        }

    }
}