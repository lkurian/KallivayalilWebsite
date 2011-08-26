using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Kallivayalil.Client;
using Telerik.Web.Mvc;
using Website.Helpers;
using Website.Models;
using Website.Models.ReferenceData;

namespace Website.Controllers
{
    public class PhoneController : Controller
    {
        private AutoDataContractMapper mapper = new AutoDataContractMapper();
        private string serviceBaseUri = ConfigurationManager.AppSettings["serviceBaseUri"];
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["userName"] == null)
                FormsAuthentication.RedirectToLoginPage();
            PopulatePhoneTypes();
            PopulateAddressTypes();
            return PartialView();
        }

        private void PopulateAddressTypes()
        {
            var constituentId = (int)Session["constituentId"];
            var addressesData = HttpHelper.Get<AddressesData>(string.Format(serviceBaseUri+"/Addresses?constituentId={0}", constituentId));

            var addresses = new ShortAddresses();
            
            addressesData.ForEach(data => addresses.Add( new ShortAddress()
                                                             {
                                                                 Id = data.Id,
                                                                 Description = string.Format("{0},{1},{2},{3}-{4},{5}",data.Line1,data.Line2,data.City,data.State,data.PostCode,data.Country)
                                                             }));

            ViewData["addresses"] = addresses;
            ;
        }

        private void PopulatePhoneTypes()
        {
            var phoneTypesData = HttpHelper.Get<PhoneTypesData>(serviceBaseUri+"/PhoneTypes");

            var phoneTypes = new PhoneTypes();
            mapper.MapList(phoneTypesData, phoneTypes, typeof (PhoneType));
            ViewData["phoneTypes"] = phoneTypes;
        }

        [GridAction]
        public ActionResult AllPhones()
        {
            return PartialView(new GridModel(GetPhones()));
        }

        private Phones GetPhones()
        {
            var constituentId = (int)Session["constituentId"];
            var phonesData = HttpHelper.Get<PhonesData>(string.Format(serviceBaseUri+"/Phones?ConstituentId={0}", constituentId));

            mapper = new AutoDataContractMapper();
            var phones = new Phones();
            mapper.MapList(phonesData, phones, typeof (Phone));
            return phones;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Create(int PhoneType,int ShortAddress)
        {
            var phone = new Phone();
            TryUpdateModel(phone);

            var constituentId = (int)Session["constituentId"];
            phone.Constituent = new Constituent {Id = constituentId};
            phone.Type = new PhoneType { Id = PhoneType };
            phone.Address = new ShortAddress() { Id = ShortAddress };

            mapper = new AutoDataContractMapper();
            var phoneData = new PhoneData();
            mapper.Map(phone, phoneData);

            var newPhone = HttpHelper.Post(string.Format(serviceBaseUri+"/Phones?ConstituentId={0}", constituentId), phoneData);

            return PartialView(new GridModel(GetPhones()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Edit(int id, int PhoneType,int ShortAddress)
        {
            var phone = new Phone();

            var constituentId = (int)Session["constituentId"];

            TryUpdateModel(phone);
            phone.Type = new PhoneType {Id = PhoneType};
            phone.Constituent = new Constituent {Id = constituentId};
            phone.Address = new ShortAddress() {Id = ShortAddress};
            mapper = new AutoDataContractMapper();
            var phoneData = new PhoneData();
            mapper.Map(phone, phoneData);

            HttpHelper.Put(string.Format(serviceBaseUri+"/Phones/{0}",id), phoneData);
            return PartialView(new GridModel(GetPhones()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Delete(int id)
        {
            HttpHelper.DoHttpDelete(string.Format(serviceBaseUri+"/Phones/{0}", id));
            return PartialView(new GridModel(GetPhones()));
        }
    }
}