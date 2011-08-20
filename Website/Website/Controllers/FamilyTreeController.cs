using System.Web.Mvc;
using Website.Helpers;

namespace Website.Controllers
{
    public class FamilyTreeController : Controller
    {

        private AutoDataContractMapper mapper = new AutoDataContractMapper();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}