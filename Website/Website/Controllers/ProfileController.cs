using System;
using System.Configuration;
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
        private string serviceBaseUri = ConfigurationManager.AppSettings["serviceBaseUri"];
        public ActionResult Profile()
        {
           if(Session["userName"]==null)
               FormsAuthentication.RedirectToLoginPage();
           PopulateConstituentTypes();
            return View();
        }

        private void PopulateConstituentTypes()
        {
            var salutationTypesData = HttpHelper.Get<SalutationTypesData>(serviceBaseUri+"/SalutationTypes");

            var salutationTypes = new SalutationTypes();
            mapper.MapList(salutationTypesData, salutationTypes, typeof(SalutationType));
            ViewData["salutationTypes"] = salutationTypes;
        }

      
    }

        
}
