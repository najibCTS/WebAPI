using Posp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Posp.Controllers
{
    public class ItemsController : Controller
    {
        HttpClient client;
        readonly string url = "http://localhost:54849/api/Items";

        public ItemsController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Items
        public ActionResult Index()
        {
            List<Item> items = new List<Item>();
            using (client)
            {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url);
                responseTask.Wait();

                //To store result of web api response.   
                HttpResponseMessage result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    Task<List<Item>> readTask = result.Content.ReadAsAsync<List<Item>>();
                    readTask.Wait();

                    items = readTask.Result;
                }
            };
            return View(items);
        }

        // GET: Items/Details/5
        public ActionResult Details(string id)
        {
            Item item = new Item();
            using (client)
            {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url + "/" + id);
                responseTask.Wait();

                //To store result of web api response.   
                HttpResponseMessage result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    Task<Item> readTask = result.Content.ReadAsAsync<Item>();
                    readTask.Wait();

                    item = readTask.Result;
                }
            };
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        public ActionResult Create(Item item)
        {
            try
            {
                using (client)
                {
                    Task<HttpResponseMessage> responseTask = client.PostAsJsonAsync(url, item);
                    responseTask.Wait();

                    //To store result of web api response.   
                    HttpResponseMessage result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        Task<Item> readTask = result.Content.ReadAsAsync<Item>();
                        readTask.Wait();

                        item = readTask.Result;
                    }
                };

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Items/Edit/5
        public ActionResult Edit(string id)
        {
            Item item = new Item();
            using (client)
            {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url + "/" + id);
                responseTask.Wait();

                //To store result of web api response.   
                HttpResponseMessage result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    Task<Item> readTask = result.Content.ReadAsAsync<Item>();
                    readTask.Wait();

                    item = readTask.Result;
                }
            };
            return View(item);
        }

        // POST: Items/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Item item)
        {
            try
            {
                using (client)
                {
                    Task<HttpResponseMessage> responseTask = client.PutAsJsonAsync(url + "/" + id, item);
                    responseTask.Wait();

                    //To store result of web api response.   
                    HttpResponseMessage result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        Task<Item> readTask = result.Content.ReadAsAsync<Item>();
                        readTask.Wait();

                        item = readTask.Result;
                    }
                };

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Items/Delete/5
        public ActionResult Delete(string id)
        {
            Item item = new Item();
            using (client)
            {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url + "/" + id);
                responseTask.Wait();

                //To store result of web api response.   
                HttpResponseMessage result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    Task<Item> readTask = result.Content.ReadAsAsync<Item>();
                    readTask.Wait();

                    item = readTask.Result;
                }
            };
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost]
        public ActionResult Delete(String id, Item item)
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
                        Task<Item> readTask = result.Content.ReadAsAsync<Item>();
                        readTask.Wait();

                        item = readTask.Result;
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
