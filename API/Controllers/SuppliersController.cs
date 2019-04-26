using BusinessLogic.Applications.Interfaces;
using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class SuppliersController : ApiController
    {
        private readonly ISupplierApplication _supplierApplication;
        public SuppliersController(ISupplierApplication supplierApplication)
        {
            _supplierApplication = supplierApplication;
        }

        // GET: api/Suppliers
        public HttpResponseMessage Get()
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Data Available");
            List<Supplier> get = _supplierApplication.Get();
            if(get.Count() != 0)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, get);
                return message;
            }
            return message;
            //return new string[] { "value1", "value2" };
        }

        // GET: api/Suppliers/5
        public HttpResponseMessage Get(int id)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Such Data Exist");
            var get = _supplierApplication.Get(id);
            if(get != null)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, get);
                return message;
            }
            return message;
            //return "value";
        }

        // POST: api/Suppliers
        public HttpResponseMessage Post([FromBody]SupplierVM supplierVM)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            var result = _supplierApplication.Insert(supplierVM);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, supplierVM);
                return message;
            }
            return message;
        }

        // PUT: api/Suppliers/5
        public HttpResponseMessage Put(int id, [FromBody]SupplierVM supplierVM)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            var result = _supplierApplication.Update(id, supplierVM);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, supplierVM);
                return message;
            }
            return message;
        }

        // DELETE: api/Suppliers/5
        public HttpResponseMessage Delete(int id)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Such Data Exist");
            var get = _supplierApplication.Delete(id);
            if (get)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, "Data has successfully deleted");
                return message;
            }
            return message;
        }
    }
}
