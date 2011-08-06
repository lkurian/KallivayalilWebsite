using System;
using System.Web.Mvc;
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

        [HttpGet]
        public ActionResult Index()
        {
            PopulatePhoneTypes();
            PopulateAddressTypes();
            return PartialView();
        }

        private void PopulateAddressTypes()
        {
            var addressesData = HttpHelper.Get<AddressesData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/Addresses?constituentId=1");

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
            var phoneTypesData = HttpHelper.Get<PhoneTypesData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/PhoneTypes");

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
            var phonesData = HttpHelper.Get<PhonesData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/Phones?ConstituentId=1");

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

            phone.Constituent = new Constituent {Id = 1};
            phone.Type = new PhoneType { Id = PhoneType };
            phone.Address = new ShortAddress() { Id = ShortAddress };

            mapper = new AutoDataContractMapper();
            var phoneData = new PhoneData();
            mapper.Map(phone, phoneData);

            var newPhone = HttpHelper.Post(@"http://localhost/kallivayalilService/KallivayalilService.svc/Phones?ConstituentId=1", phoneData);

            return PartialView(new GridModel(GetPhones()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Edit(int id, int PhoneType,int ShortAddress)
        {
            var phone = new Phone();

            TryUpdateModel(phone);
            phone.Type = new PhoneType {Id = PhoneType};
            phone.Constituent = new Constituent {Id = 1};
            phone.Address = new ShortAddress() {Id = ShortAddress};
            mapper = new AutoDataContractMapper();
            var phoneData = new PhoneData();
            mapper.Map(phone, phoneData);

            HttpHelper.Put(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Phones/{0}",id), phoneData);
            return PartialView(new GridModel(GetPhones()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Delete(int id)
        {
            HttpHelper.DoHttpDelete(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Phones/{0}", id));
            return PartialView(new GridModel(GetPhones()));
        }
    }
}