using EmptyASP.Models;
using EmptyASP.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using API.Controllers;
using Newtonsoft.Json;

namespace EmptyASP.Controllers
{
    public class ItemController : Controller
    {
        MyContext myContext = new MyContext();
        string BaseURL = "http://localhost:52207/api/";
        HttpClient client = new HttpClient();

        // GET: Item
        public ActionResult Index()
        {
            IEnumerable<ItemVM> itemVM = null;            
            client.BaseAddress = new Uri(BaseURL);
            var responseTask = client.GetAsync("Items");
            responseTask.Wait();
            var result = responseTask.Result;
            //result.EnsureSuccessStatusCode();
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<ItemVM>>();
                readTask.Wait();
                itemVM = readTask.Result;
            }
            else
            {
                itemVM = Enumerable.Empty<ItemVM>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return View(itemVM);
        }

        public JsonResult Get(int id)
        {
            ItemVM get = null;

            //initiate client
            client.BaseAddress = new Uri(BaseURL);
            var response = client.GetAsync("Items/" + id);
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var getSupplier = result.Content.ReadAsAsync<ItemVM>();
                getSupplier.Wait();
                get = getSupplier.Result;
            }
            else
            {
                get = null;
                ModelState.AddModelError(string.Empty, "Server Error, try after some time");
            }
            return Json(new { Id = get.Id, Name = get.Name, Price = get.Price, Stock = get.Stock, Supplier_Id = get.Suppliers_Id });
        }

        // GET: Item
        // with async task
        //public async Task<ActionResult> Index()
        //{
        //    List<Item> itemList = new List<Item>();

        //    //initiate client
        //    client.BaseAddress = new Uri(BaseURL);
        //    client.DefaultRequestHeaders.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
        //    HttpResponseMessage Res = await client.GetAsync("api/Items");

        //    //Checking the response is successful or not which is sent using HttpClient  
        //    if (Res.IsSuccessStatusCode)
        //    {
        //        //Storing the response details recieved from web api   
        //        var getItem = Res.Content.ReadAsStringAsync().Result;                

        //        //Deserializing the response recieved from web api and storing into the Item list  
        //        itemList = JsonConvert.DeserializeObject<List<Item>>(getItem);
        //    }

        //    return View(itemList);
        //    //return View(myContext.Items.Include("Suppliers").Where(x => x.IsDelete == false).ToList());
        //}

        // GET: Item/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Item/Create
        //public ActionResult Create()
        //{
            ////var list = myContext.Suppliers.Where(x => x.IsDelete == false).ToList();
            ////List<SelectListItem> SupplierList = new List<SelectListItem>();
            ////foreach(var addList in list)
            ////{
            ////    SupplierList.Add(new SelectListItem
            ////    {
            ////        Text = addList.Name,
            ////        Value = addList.Id.ToString(),
            ////        Selected = false
            ////    });
            ////}
            ////ViewBag.Suppliers = SupplierList;
            //ViewBag.Suppliers =  new SelectList(myContext.Suppliers.Where(x => x.IsDelete == false).ToList(), "Id", "Name", "0");
            //return View();
        //}

        // POST: Item/Create        
        public JsonResult Create(ItemVM itemVM)//int id, Item items)
        {
            //var supplier = myContext.Suppliers.Find(id);
            //items.Suppliers = supplier;

            /* MVC create function
            var validity = true;
            if (String.IsNullOrWhiteSpace(itemVM.Name))
            {                
                ModelState.AddModelError("Name", "Name is Empty");
                validity = false;
            }
            if(itemVM.Price < 0)
            {
                ModelState.AddModelError("Price", "Price value cannot below zero");
                validity = false;
            }
            if(itemVM.Stock < 0)
            {
                ModelState.AddModelError("Stock", "Stock value cannot below zero");
                validity = false;
            }            
            if (validity)
            {
                var supplier = myContext.Suppliers.Find(itemVM.Suppliers_Id);
                Item item = new Item()
                {
                    Name = itemVM.Name,
                    Price = itemVM.Price,
                    Stock = itemVM.Stock,
                    Suppliers = supplier
                };
                myContext.Items.Add(item);
                myContext.SaveChanges();
                TempData["message"] = "New Item Data added";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Item data creation failed";
                ViewBag.Suppliers = new SelectList(myContext.Suppliers.Where(x => x.IsDelete == false).ToList(), "Id", "Name", "0");
                return View();
            }*/

            //Web API function with async task
            //initiate client
            //client.BaseAddress = new Uri(BaseURL);
            //client.DefaultRequestHeaders.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpResponseMessage Res = await client.PostAsJsonAsync("api/Items", itemVM);
            ////Res.EnsureSuccessStatusCode();
            //if (Res.IsSuccessStatusCode)
            //{
            //    TempData["message"] = "New Item Data added";
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    TempData["message"] = "Item data creation failed";
            //    ViewBag.Suppliers = new SelectList(myContext.Suppliers.Where(x => x.IsDelete == false).ToList(), "Id", "Name", "0");
            //    return View();
            //}

            bool valid = false;
            string nameValidity = "", priceValidity = "", stockValidity = "", supplierValidity = "";

            //validasi
            if (String.IsNullOrWhiteSpace(itemVM.Name))
            {
                nameValidity = "Item Name is empty";
                valid = false;
            }

            if (String.IsNullOrWhiteSpace(itemVM.Price.ToString()) || itemVM.Price < 0)
            {
                priceValidity = "Item Price is invalid";
                valid = false;
            }

            if (String.IsNullOrWhiteSpace(itemVM.Stock.ToString()) || itemVM.Stock < 0)
            {
                stockValidity = "Item Stock is invalid";
                valid = false;
            }

            if (String.IsNullOrWhiteSpace(itemVM.Suppliers_Id.ToString()) || itemVM.Suppliers_Id == 0)
            {
                supplierValidity = "Item Supplier is invalid";
                valid = false;
            }

            if (valid)
            {
                //fungsi create
                client.BaseAddress = new Uri(BaseURL);
                var response = client.PostAsJsonAsync("Items", itemVM);
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
            else
            {
                return Json(new { success = false, name = nameValidity, price = priceValidity, stock = stockValidity, supplier = supplierValidity });
            }
        }

        public JsonResult Edit(int id, ItemVM itemVM)
        {
            bool valid = false;
            string nameValidity = "", priceValidity = "", stockValidity = "", supplierValidity = "";

            //validasi
            if (String.IsNullOrWhiteSpace(itemVM.Name))
            {                
                nameValidity = "Item Name is empty";
                valid = false;                
            }

            if (String.IsNullOrWhiteSpace(itemVM.Price.ToString()) || itemVM.Price < 0)
            {
                priceValidity = "Item Price is invalid";
                valid = false;                
            }

            if (String.IsNullOrWhiteSpace(itemVM.Stock.ToString()) || itemVM.Stock < 0)
            {
                stockValidity = "Item Stock is invalid";
                valid = false;                
            }

            if (String.IsNullOrWhiteSpace(itemVM.Suppliers_Id.ToString()) || itemVM.Suppliers_Id == 0)
            {
                supplierValidity = "Item Supplier is invalid";
                valid = false;                
            }

            if (valid)
            {
                client.BaseAddress = new Uri(BaseURL);
                var response = client.PutAsJsonAsync("Items/" + id, itemVM);
                //var response = client.PutAsJsonAsync("Items/" + itemVM.Id, itemVM);
                response.Wait();
                var result = response.Result;
                result.EnsureSuccessStatusCode();
                return Json(new { success = true, responseText = "Success" });
            }
            else
            {
                //TempData["message"] = "Please check error note";
                return Json(new { success = false, name = nameValidity, price = priceValidity, stock = stockValidity, supplier = supplierValidity });
            }
        }

        // GET: Item/Edit/5
        //public ActionResult Edit(int id)
        //{            
        //    var getItem = myContext.Items.Find(id);
        //    ItemVM itemVM = new ItemVM()
        //    {
        //        Id = getItem.Id,
        //        Name = getItem.Name,
        //        Price = getItem.Price,
        //        Stock = getItem.Stock,
        //        Suppliers_Id = getItem.Suppliers.Id
        //    };
        //    ViewBag.Suppliers = new SelectList(myContext.Suppliers.Where(x => x.IsDelete == false).ToList(), "Id", "Name", "0");
        //    return View(itemVM);
        //}

        // POST: Item/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, ItemVM itemVM)
        //{
        //    var validity = true;
        //    if (String.IsNullOrWhiteSpace(itemVM.Name))
        //    {
        //        ModelState.AddModelError("Name", "Name is Empty");
        //        validity = false;
        //    }
        //    if (itemVM.Price < 0)
        //    {
        //        ModelState.AddModelError("Price", "Price value cannot below zero");
        //        validity = false;
        //    }
        //    if (itemVM.Stock < 0)
        //    {
        //        ModelState.AddModelError("Stock", "Stock value cannot below zero");
        //        validity = false;
        //    }
        //    if (validity)
        //    {
        //        var get = myContext.Items.Find(id);
        //        var supplier = myContext.Suppliers.Find(itemVM.Suppliers_Id);
        //        get.Name = itemVM.Name;
        //        get.Price = itemVM.Price;
        //        get.Stock = itemVM.Stock;
        //        get.Suppliers = supplier;
        //        myContext.Entry(get).State = EntityState.Modified;
        //        myContext.SaveChanges();
        //        TempData["message"] = "Item " + id + " has been updated";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        TempData["message"] = "Item data creation failed";
        //        ViewBag.Suppliers = new SelectList(myContext.Suppliers.Where(x => x.IsDelete == false).ToList(), "Id", "Name", "0");
        //        return View();
        //    }
        //}

        // GET: Item/Delete/5
        //public ActionResult Delete()
        //{
        //    return View();
        //}

        //// POST: Item/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    var get = myContext.Items.Find(id);
        //    get.IsDelete = true;
        //    myContext.Entry(get).State = EntityState.Modified;
        //    myContext.SaveChanges();
        //    TempData["message"] = "Item " + id + " has been deleted";
        //    return RedirectToAction("Index");            
        //}

        public JsonResult Delete(int id)
        {            
            client.BaseAddress = new Uri(BaseURL);
            var response = client.DeleteAsync("Items/" + id);
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

                // Getting all Item data
                IEnumerable<Item> itemData = null;
                client.BaseAddress = new Uri(BaseURL);
                var responseTask = client.GetAsync("Items");
                responseTask.Wait();
                var result = responseTask.Result;
                //result.EnsureSuccessStatusCode();
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Item>>();
                    readTask.Wait();
                    itemData = readTask.Result;
                }
                else
                {
                    itemData = Enumerable.Empty<Item>();                    
                }

                //var itemData = (from tempitem in myContext.Items
                //                where tempitem.IsDelete == false
                //                select tempitem);
                //var asd = myContext.Items.Include("Suppliers").Where(x => x.IsDelete == false);
                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    itemData = itemData.OrderBy(sortColumn + " " + sortColumnDir);
                }
                //Search    
                if (!string.IsNullOrEmpty(searchValue))
                {
                    itemData = itemData.Where(m => m.Name == searchValue);
                }

                //total number of rows count     
                recordsTotal = itemData.Count();
                //Paging     
                var data = itemData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data    
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
