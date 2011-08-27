using System;
using System.Configuration;
using System.Web.Mvc;
using Kallivayalil.Client;

namespace Website.Controllers
{
    public class RegistrationController : Controller
    {
        private string serviceBaseUri = ConfigurationManager.AppSettings["serviceBaseUri"];
        public ActionResult Index()
        {
            return View();
        }

        public void Register(FormCollection formCollection)
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
                                                Type = new AddressTypeData {Id = 1},
                                                IsPrimary = true
                                            };
            registerationData.Constituent = new ConstituentData
                                                {
                                                    BornOn = DateTime.Parse(formCollection["dob"]).Date,
                                                    BranchName = Convert.ToInt16(formCollection["branchname"]),
                                                    Gender = formCollection["gender"],
                                                    HouseName = formCollection["housename"],
                                                    MaritialStatus = Convert.ToInt16(formCollection["maritalstatus"]),
                                                    Name = new ConstituentNameData
                                                               {
                                                                   FirstName = formCollection["firstname"],
                                                                   MiddleName = formCollection["middlename"],
                                                                   LastName = formCollection["lastname"],
                                                                   PreferedName = formCollection["preferedname"],
                                                                   Salutation = new SalutationTypeData {Id = Convert.ToInt16(formCollection["salutation"])}
                                                               }
                                                };
            registerationData.Phone = new PhoneData
                                          {
                                              Number = formCollection["phonenumber"],
                                              Type = new PhoneTypeData {Id = Convert.ToInt16(formCollection["phonetype"])},
                                              IsPrimary = true
                                          };

            registerationData.Email = formCollection["email"];
            registerationData.Password = formCollection["password"];

            var registeredData = HttpHelper.Post(serviceBaseUri+"/Registration",registerationData);
            RedirectToAction("Index", "Home");
        }
    }
}