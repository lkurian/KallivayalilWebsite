using System.Web.Mvc;
using System.Web.Security;
using Website.Helpers;

namespace Website.Controllers
{
    public class FamilyTreeController : Controller
    {

        private AutoDataContractMapper mapper = new AutoDataContractMapper();

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["userName"] == null)
                FormsAuthentication.RedirectToLoginPage();
            return View();
        }
    }
}