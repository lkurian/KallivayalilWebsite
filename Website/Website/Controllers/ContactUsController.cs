using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Kallivayalil.Client;
using Website.Helpers;
using Website.Models;
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
            var contactUs = new ContactUs();

            contactUs.Name = model.Name;
            contactUs.Email = model.Email;
            contactUs.Comments = HttpUtility.HtmlDecode(model.Comment);

            var contactUsData = new ContactUsData();
            mapper.Map(contactUs, contactUsData);

            HttpHelper.Post(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/ContactUs"),contactUsData);
            
            return View();
        }

    }
}