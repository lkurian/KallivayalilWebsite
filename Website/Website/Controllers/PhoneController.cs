using System.Web.Mvc;
using Kallivayalil.Client;
using Telerik.Web.Mvc;
using Website.Helpers;
using Website.Models;
using System.Linq;

namespace Website.Controllers
{
    public class PhoneController : Controller
    {
        private AutoDataContractMapper mapper;

          [GridAction]
        public ActionResult Index()
        {
            var phonesData = HttpHelper.Get<PhonesData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/Phones?ConstituentId=123");
            mapper = new AutoDataContractMapper();
            var phones = new Phones();
            mapper.MapList(phonesData,phones,typeof(Phone));

            return View(new GridModel(phones));
        }


       public ActionResult Create()
       {           
           return View();
       }

          [GridAction]
        public ActionResult Create(FormCollection formCollection)
        {
            var phone = new Phone();
            TryUpdateModel(phone ,formCollection);

            phone.Constituent = new Constituent{Id = 1};

            mapper = new AutoDataContractMapper();
            var phoneData = new PhoneData();
            mapper.Map(phone,phoneData);

            var newPhone = HttpHelper.Post(@"http://localhost/kallivayalilService/KallivayalilService.svc/Phones?ConstituentId=1", phoneData);

            
            return RedirectToAction("Index");
            
        }

        public ActionResult Edit(int id)
        {
            var phoneData = HttpHelper.Get<PhoneData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/Phones/" + id);
            mapper = new AutoDataContractMapper();
            var phone = new Phone();
            mapper.Map(phoneData,phone);
            
            return PartialView(phone);
        }

          [GridAction]
        public ActionResult Edit(Phone phone)
        {
            phone.Constituent = new Constituent{Id = 123};
            mapper = new AutoDataContractMapper();
            var phoneData = new PhoneData();
            mapper.Map(phone, phoneData);

            HttpHelper.Put(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Phones?constituentId=1"),phoneData);
            return RedirectToAction("Index");
        }

          [GridAction]
        public ActionResult Delete(int id)
        {
            HttpHelper.DoHttpDelete(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Phones/{0}",id));
            return RedirectToAction("Index");            
        }


    }
}
