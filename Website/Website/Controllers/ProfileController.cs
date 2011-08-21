using System;
using System.Web.Mvc;
using System.Web.Security;
using Kallivayalil.Client;
using Website.Helpers;
using Website.Models;
using Website.Models.ReferenceData;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class ProfileController : Controller
    {
        private AutoDataContractMapper mapper = new AutoDataContractMapper();

        public ActionResult Profile()
        {
           if(Session["userName"]==null)
               FormsAuthentication.RedirectToLoginPage();
           PopulateConstituentTypes();
            return View();
        }

        private void PopulateConstituentTypes()
        {
            var salutationTypesData = HttpHelper.Get<SalutationTypesData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/SalutationTypes");

            var salutationTypes = new SalutationTypes();
            mapper.MapList(salutationTypesData, salutationTypes, typeof(SalutationType));
            ViewData["salutationTypes"] = salutationTypes;
        }

        [HttpPost]
        public ActionResult Save(ConstituentInputModel constituent)
        {
            var constituentToSave = new Constituent();

            constituentToSave.Name = new ConstituentName()
                                         {
                                             Id=constituent.NameId,
                                             FirstName = constituent.FirstName,
                                             MiddleName = constituent.MiddleName,
                                             LastName = constituent.LastName,
                                             CreatedBy = constituent.CreatedBy,
                                             CreatedDateTime =  constituent.CreatedDateTime,
                                             PreferedName = "temp",
                                             Salutation = new SalutationType(){Id = 1}

                                         };
            constituentToSave.HouseName = constituent.HouseName;
            constituentToSave.BranchName = constituent.BranchName;
            constituentToSave.Gender = constituent.Gender;
            constituentToSave.MaritialStatus = constituent.MaritialStatus;
            constituentToSave.HasExpired = constituent.HasExpired;
            constituentToSave.IsRegistered = constituent.IsRegistered;
            constituentToSave.CreatedDateTime = constituent.CreatedDateTime;
            constituentToSave.CreatedBy = constituent.CreatedBy;
            constituentToSave.BornOn = constituent.BornOn;

            var mapper = new AutoDataContractMapper();
            var constituentData = new ConstituentData();
            mapper.Map(constituentToSave, constituentData);

            ConstituentData data = HttpHelper.Put(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Constituents/{0}", Session["constituentId"]), constituentData);
            var savedConstituent = new Constituent();
            mapper.Map(data,savedConstituent);
            return View(savedConstituent);
        }
    }

        
}
