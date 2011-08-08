using System.Web.Mvc;
using System.Web.Security;

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
    }
}
