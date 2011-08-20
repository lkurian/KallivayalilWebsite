using System.Web.Mvc;
using System.Web.Security;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult Profile()
        {
           if(Session["userName"]==null)
               FormsAuthentication.RedirectToLoginPage();
            return View();
        }

        [HttpPost]
        public ActionResult Save(ConstituentInputModel constituent)
        {
            return Json(constituent);
        }
    }

        
}
