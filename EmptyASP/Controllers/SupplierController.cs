using EmptyASP.Models;
using EmptyASP.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace EmptyASP.Controllers
{
    public class SupplierController : Controller
    {
        MyContext myContext = new MyContext();
        string BaseURL = "http://localhost:52207/api/";
        HttpClient client = new HttpClient();

        // GET: Supplier
        public ActionResult Index()
        {
            IEnumerable<SupplierVM> supplierList = null;

            //initiate client
            client.BaseAddress = new Uri(BaseURL);
            //client.DefaultRequestHeaders.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync("Suppliers");
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var getSupplier = result.Content.ReadAsAsync<IList<SupplierVM>>();
                getSupplier.Wait();
                supplierList = getSupplier.Result;
            }
            else
            {
                supplierList = Enumerable.Empty<SupplierVM>();
                ModelState.AddModelError(string.Empty, "Server Error, try after some time");
            }

            return View(supplierList);
            //return View(myContext.Suppliers.Where(x => x.IsDelete == false).ToList());
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            return View(myContext.Suppliers.Find(id));
        }
        
        public JsonResult Get(int id)
        {
            SupplierVM get = null;

            //initiate client
            client.BaseAddress = new Uri(BaseURL); 
            var response = client.GetAsync("Suppliers/"+id);
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var getSupplier = result.Content.ReadAsAsync<SupplierVM>();
                getSupplier.Wait();
                get = getSupplier.Result;
            }
            else
            {
                get = null;
                ModelState.AddModelError(string.Empty, "Server Error, try after some time");
            }
            return Json(new { Id = get.Id, Name = get.Name });
        }

        // POST: Supplier/Create        
        public JsonResult Create(SupplierVM modelSupplier)
        {
            if (String.IsNullOrWhiteSpace(modelSupplier.Name))
            {
                ModelState.AddModelError("Name", "Name is empty");
                return Json(new { success = false, responseText = "Failed" });
                //return View();
            }
            else
            {
                // TODO: Add insert logic here
                //Supplier newSupplier = new Supplier()
                //{
                //    Name = modelSupplier.Name                    
                //};
                //myContext.Suppliers.Add(newSupplier);
                //myContext.SaveChanges();
                //TempData["message"] = "New Supplier Data added";                
                //return View();
                client.BaseAddress = new Uri(BaseURL);
                //var content = JsonConvert.SerializeObject(modelSupplier);
                //var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                //var byteContent = new ByteArrayContent(buffer);
                //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //var response = client.PostAsync("", byteContent).Result;
                var response = client.PostAsJsonAsync("Suppliers", modelSupplier);
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    //TempData["message"] = "New Supplier Data added";
                    //return RedirectToAction("Index");
                    return Json(new { success = true, responseText = "Success" });
                }
                else
                {
                    //return RedirectToAction("Index");
                    return Json(new { success = false, responseText = "Failed" });
                }
            }
        }        

        // POST: Supplier/Edit/5        
        public JsonResult Edit(int id, SupplierVM supplierVM)
        {
            if (String.IsNullOrWhiteSpace(supplierVM.Name))
            {
                ModelState.AddModelError("Name", "Name is empty");
                return Json(new { success = false, responseText = "Failed" });
            }
            else
            {
                // TODO: Add edit logic here
                //var get = myContext.Suppliers.Find(id);
                //get.Name = suppliers.Name;
                //myContext.Entry(get).State = EntityState.Modified;
                //myContext.SaveChanges();
                //TempData["message"] = "Supplier "+id+" has been updated";
                //return RedirectToAction("Index");
                client.BaseAddress = new Uri(BaseURL);
                var response = client.PutAsJsonAsync("Suppliers/" + id, supplierVM);
                response.Wait();
                var result = response.Result;
                result.EnsureSuccessStatusCode();
                return Json(new { success = true, responseText = "Success" });
            }            
        }        

        // POST: Supplier/Delete/5        
        public JsonResult Delete(int id)
        {
            // TODO: Add delete logic here
            //var get = myContext.Suppliers.Find(id);
            //get.IsDelete = true;
            //myContext.Entry(get).State = EntityState.Modified;
            //myContext.SaveChanges();
            //TempData["message"] = "Supplier " + id + " has been deleted";
            //return RedirectToAction("Index");
            client.BaseAddress = new Uri(BaseURL);
            var response = client.DeleteAsync("Suppliers/" + id);
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                return Json(new { success = true, responseText = "Success" });
            }
            else
            {
                return Json(new { success = false, responseText = "Failed" });
            }
        }

        //for datatables
        public ActionResult LoadData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][Name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                //Paging Size (10,20,50,100)    
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Supplier data
                IEnumerable<SupplierVM> supplierData = null;
                client.BaseAddress = new Uri(BaseURL);
                var response = client.GetAsync("Suppliers");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var getSupplier = result.Content.ReadAsAsync<IList<SupplierVM>>();
                    getSupplier.Wait();
                    supplierData = getSupplier.Result;
                }
                else
                {
                    supplierData = Enumerable.Empty<SupplierVM>();
                    ModelState.AddModelError(string.Empty, "Server Error, try after some time");
                }
                //var supplierData = (from tempsupplier in myContext.Suppliers
                //                where tempsupplier.IsDelete == false
                //                select tempsupplier);
                
                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    supplierData = supplierData.OrderBy(sortColumn + " " + sortColumnDir);
                }
                //Search    
                if (!string.IsNullOrEmpty(searchValue))
                {
                    supplierData = supplierData.Where(m => m.Name.Contains(searchValue));
                }

                //total number of rows count     
                recordsTotal = supplierData.Count();
                //Paging     
                var data = supplierData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data    
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }

        //load data supplier for item form
        public JsonResult LoadSupplier()
        {
            IEnumerable<SupplierVM> supplierVM = null;
            client.BaseAddress = new Uri(BaseURL);
            var responseTask = client.GetAsync("Suppliers");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<SupplierVM>>();
                readTask.Wait();
                supplierVM = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(supplierVM, JsonRequestBehavior.AllowGet);
        }
    }    
}