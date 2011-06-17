using System.Collections.Generic;
using System.Web.Mvc;
using Kallivayalil.Client;
using Telerik.Web.Mvc;
using Website.Helpers;
using Website.Models;

namespace Website.Controllers
{
    public class PhoneController : Controller
    {
        private AutoDataContractMapper mapper;

        [GridAction]
        public ActionResult Index()
        {
            return View(new GridModel(AllPhones()));
        }

        private IEnumerable<Phone> AllPhones()
        {
            var phonesData = HttpHelper.Get<PhonesData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/Phones?ConstituentId=123");
            mapper = new AutoDataContractMapper();
            var phones = new Phones();
            mapper.MapList(phonesData, phones, typeof (Phone));
            return phones;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Create()
        {
            var phone = new Phone();
            TryUpdateModel(phone);

            phone.Constituent = new Constituent {Id = 123};

            mapper = new AutoDataContractMapper();
            var phoneData = new PhoneData();
            mapper.Map(phone, phoneData);

            var newPhone = HttpHelper.Post(@"http://localhost/kallivayalilService/KallivayalilService.svc/Phones?ConstituentId=123", phoneData);

            return View(new GridModel(AllPhones()));
        }

        public ActionResult Edit(int id)
        {
            var phoneData = HttpHelper.Get<PhoneData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/Phones/" + id);
            mapper = new AutoDataContractMapper();
            var phone = new Phone();
            mapper.Map(phoneData, phone);

            return PartialView(phone);
        }

        [GridAction]
        public ActionResult Edit(Phone phone)
        {
            phone.Constituent = new Constituent {Id = 123};
            mapper = new AutoDataContractMapper();
            var phoneData = new PhoneData();
            mapper.Map(phone, phoneData);

            HttpHelper.Put(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Phones?constituentId=1"), phoneData);
            return RedirectToAction("Index");
        }

        [GridAction]
        public ActionResult Delete(int id)
        {
            HttpHelper.DoHttpDelete(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Phones/{0}", id));
            return RedirectToAction("Index");
        }
    }
}