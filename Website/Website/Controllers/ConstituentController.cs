using System.Web.Mvc;
using Kallivayalil.Client;
using Telerik.Web.Mvc;
using Website.Helpers;
using Website.Models;
using Website.Models.ReferenceData;

namespace Website.Controllers
{
    public class ConstituentController : Controller
    {
        private AutoDataContractMapper mapper = new AutoDataContractMapper();

        [HttpGet]
        public ActionResult Index()
        {
//            PopulateConstituentTypes();
            return PartialView(GetConstituent());
        }

        private void PopulateConstituentTypes()
        {
            var salutationTypesData = HttpHelper.Get<SalutationTypesData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/SalutationTypes");

            var salutationTypes = new SalutationTypes();
            mapper.MapList(salutationTypesData, salutationTypes, typeof (SalutationType));
            ViewData["salutationTypes"] = salutationTypes;
        }

        [GridAction]
        public ActionResult AllConstituentDetails()
        {
            return PartialView(GetConstituent());
        }

        private Constituent GetConstituent()
        {
            var constituentData = HttpHelper.Get<ConstituentData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/Constituents/1");

            mapper = new AutoDataContractMapper();
            var constituent = new Constituent();
            mapper.Map(constituentData, constituent);
            return constituent;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Edit(int id)
        {
            var constituent = new Constituent();

            TryUpdateModel(constituent);
            
            mapper = new AutoDataContractMapper();
            var constituentData = new ConstituentData();
            mapper.Map(constituent, constituentData);

            HttpHelper.Put(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Constituents/{0}",id),constituentData);
            return PartialView(GetConstituent());
        }

    }
}