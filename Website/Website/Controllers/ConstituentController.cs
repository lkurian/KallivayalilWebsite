using System.Web.Mvc;
using System.Web.Security;
using Kallivayalil.Client;
using Telerik.Web.Mvc;
using Website.Helpers;
using Website.Models;

namespace Website.Controllers
{
    public class ConstituentController : Controller
    {
        private AutoDataContractMapper mapper = new AutoDataContractMapper();

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["userName"] == null)
                FormsAuthentication.RedirectToLoginPage();
            
            return PartialView(GetConstituent());
        }


        [GridAction]
        public ActionResult AllConstituentDetails()
        {
            return PartialView(GetConstituent());
        }

        private Constituent GetConstituent()
        {
            var constituentId = (int)Session["constituentId"];
            var constituentData = HttpHelper.Get<ConstituentData>(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Constituents/{0}", constituentId));

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