using System.Web.Mvc;
using Website.Models.ViewModels;

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


        [HttpPost]
        public ActionResult Search(SearchModel strings)
        {

            return View("Index");
        }
    }

}