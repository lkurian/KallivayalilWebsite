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
            return PartialView();
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
            var phonesData = HttpHelper.Get<PhonesData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/Phones?ConstituentId=123");

            mapper = new AutoDataContractMapper();
            var phones = new Phones();
            mapper.MapList(phonesData, phones, typeof (Phone));
            return phones;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Create(int PhoneType)
        {
            var phone = new Phone();
            TryUpdateModel(phone);

            phone.Constituent = new Constituent {Id = 123};
            phone.Type = new PhoneType { Id = PhoneType };

            mapper = new AutoDataContractMapper();
            var phoneData = new PhoneData();
            mapper.Map(phone, phoneData);

            var newPhone = HttpHelper.Post(@"http://localhost/kallivayalilService/KallivayalilService.svc/Phones?ConstituentId=123", phoneData);

            return PartialView(new GridModel(GetPhones()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Edit(int id, int PhoneType)
        {
            var phone = new Phone();

            TryUpdateModel(phone);
            phone.Type = new PhoneType {Id = PhoneType};
            phone.Constituent = new Constituent {Id = 123};
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