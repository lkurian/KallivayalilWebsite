using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Website.Helpers;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class ContactUsController : Controller
    {

        private AutoDataContractMapper mapper = new AutoDataContractMapper();

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["userName"] == null)
                FormsAuthentication.RedirectToLoginPage();
            return View();
        }

        
        [HttpPost]
        public ActionResult Submit(ContactUsInputModel model)
        {
            return View();
        }

    }
}