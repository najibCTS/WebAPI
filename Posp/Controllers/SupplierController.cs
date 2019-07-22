using Posp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Posp.Controllers
{
    public class SupplierController : Controller
    {
        HttpClient client;
        readonly string url = "http://localhost:54849/api/Suppliers";

        public SupplierController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Supplier
        public ActionResult Index()
        {
            List<Supplier> suppliers = new List<Supplier>();
            using (client)
            {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url);
                responseTask.Wait();

                //To store result of web api response.   
                HttpResponseMessage result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    Task<List<Supplier>> readTask = result.Content.ReadAsAsync<List<Supplier>>();
                    readTask.Wait();

                    suppliers = readTask.Result;
                }
            };
            return View(suppliers);
        }

        // GET: Supplier/Details/5
        public ActionResult Details(string id)
        {
            Supplier supplier = new Supplier();
            using (client)
            {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url + "/" + id);
                responseTask.Wait();

                //To store result of web api response.   
                HttpResponseMessage result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    Task<Supplier> readTask = result.Content.ReadAsAsync<Supplier>();
                    readTask.Wait();

                    supplier = readTask.Result;
                }
            };
            return View(supplier);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            try
            {
                using (client)
                {
                    Task<HttpResponseMessage> responseTask = client.PostAsJsonAsync(url, supplier);
                    responseTask.Wait();

                    //To store result of web api response.   
                    HttpResponseMessage result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        Task<Supplier> readTask = result.Content.ReadAsAsync<Supplier>();
                        readTask.Wait();

                        supplier = readTask.Result;
                    }
                };
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(string id)
        {
            Supplier supplier = new Supplier();
            using (client)
            {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url + "/" + id);
                responseTask.Wait();

                //To store result of web api response.   
                HttpResponseMessage result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    Task<Supplier> readTask = result.Content.ReadAsAsync<Supplier>();
                    readTask.Wait();

                    supplier = readTask.Result;
                }
            };
            return View(supplier);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Supplier supplier)
        {
            try
            {
                using (client)
                {
                    Task<HttpResponseMessage> responseTask = client.PutAsJsonAsync(url + "/" + id, supplier);
                    responseTask.Wait();

                    //To store result of web api response.   
                    HttpResponseMessage result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        Task<Supplier> readTask = result.Content.ReadAsAsync<Supplier>();
                        readTask.Wait();

                        supplier = readTask.Result;
                    }
                };
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(string id)
        {
            Supplier supplier = new Supplier();
            using (client)
            {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url + "/" + id);
                responseTask.Wait();

                //To store result of web api response.   
                HttpResponseMessage result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    Task<Supplier> readTask = result.Content.ReadAsAsync<Supplier>();
                    readTask.Wait();

                    supplier = readTask.Result;
                }
            };
            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Supplier supplier)
        {
            try
            {
                using (client)
                {
                    Task<HttpResponseMessage> responseTask = client.DeleteAsync(url + "/" + id);
                    responseTask.Wait();

                    //To store result of web api response.   
                    HttpResponseMessage result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        Task<Supplier> readTask = result.Content.ReadAsAsync<Supplier>();
                        readTask.Wait();

                        supplier = readTask.Result;
                    }
                };

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
