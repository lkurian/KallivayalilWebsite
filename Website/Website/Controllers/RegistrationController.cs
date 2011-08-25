using System.Web.Mvc;
using Kallivayalil.Client;

namespace Website.Controllers
{
    public class RegistrationController : Controller
    {
        public ActionResult Index()
        {
            return View();
        } 
        
        public void Register( FormCollection formCollection)
        {
            var registerationData = new RegisterationData();

            registerationData.Address = new AddressData
                                            {
                                                Line1 = formCollection["line1"],
                                                Line2 = formCollection["line2"],
                                                City = formCollection["city"],
                                                State = formCollection["state"],
                                                PostCode = formCollection["postcode"],
                                                Country = formCollection["country"],
                                                Type = new AddressTypeData {Id = 1}
                                            };


            RedirectToAction("Index", "Home");
        }
    }
}