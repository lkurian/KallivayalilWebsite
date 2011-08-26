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
    public class AddressController : Controller
    {
        private AutoDataContractMapper mapper = new AutoDataContractMapper();
        private string serviceBaseUri = ConfigurationManager.AppSettings["serviceBaseUri"];
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["userName"] == null)
                FormsAuthentication.RedirectToLoginPage();
            PopulateAddressTypes();
            return PartialView();
        }

        private void PopulateAddressTypes()
        {
            var addressTypesData = HttpHelper.Get<AddressTypesData>(serviceBaseUri+"/AddressTypes");

            var addressTypes = new AddressTypes();
            mapper.MapList(addressTypesData, addressTypes, typeof (AddressType));
            ViewData["addressTypes"] = addressTypes;
        }

        [GridAction]
        public ActionResult AllAddresses()
        {
            return PartialView(new GridModel(GetAddress()));
        }

        private Addresses GetAddress()
        {
            var constituentId = (int)Session["constituentId"];
            var addressesData = HttpHelper.Get<AddressesData>(string.Format(serviceBaseUri+"/Addresses?ConstituentId={0}", constituentId));

            mapper = new AutoDataContractMapper();
            var addresses = new Addresses();
            mapper.MapList(addressesData, addresses, typeof (Address));
            return addresses;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Create(int addressType)
        {
            var address = new Address();
            TryUpdateModel(address);
            var constituentId = (int)Session["constituentId"];
            address.Constituent = new Constituent {Id = constituentId};
            address.Type = new AddressType { Id = addressType };

            mapper = new AutoDataContractMapper();
            var addressData = new AddressData();
            mapper.Map(address, addressData);

            var newAddress = HttpHelper.Post(string.Format(serviceBaseUri+"/Addresses?ConstituentId={0}", constituentId), addressData);

            return PartialView(new GridModel(GetAddress()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Edit(int id, int addressType)
        {
            var address = new Address();
            var constituentId = (int)Session["constituentId"];

            TryUpdateModel(address);
            address.Type = new AddressType {Id = addressType};
            address.Constituent = new Constituent {Id = constituentId};
            
            mapper = new AutoDataContractMapper();
            var addressData = new AddressData();
            mapper.Map(address, addressData);

            HttpHelper.Put(string.Format(serviceBaseUri+"/Addresses/{0}",id),addressData);
            return PartialView(new GridModel(GetAddress()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Delete(int id)
        {
            HttpHelper.DoHttpDelete(string.Format(serviceBaseUri+"/Addresses/{0}", id));
            return PartialView(new GridModel(GetAddress()));
        }
    }
}