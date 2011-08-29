using System.Web.Mvc;

namespace Website.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Index()
        {
//            if(Session["userName"]==null)
//                FormsAuthentication.RedirectToLoginPage();
            return View();
        }

    }
}