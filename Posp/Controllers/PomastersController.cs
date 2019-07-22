using Posp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Posp.Controllers
{
    public class PomastersController : Controller
    {
        HttpClient client;
        readonly string url = "http://localhost:54849/api/Pomasters";

        public PomastersController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Pomasters
        public ActionResult Index()
        {
            List<Pomaster> pomasters = new List<Pomaster>();
            using (client)
            {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url);
                responseTask.Wait();

                //To store result of web api response.   
                HttpResponseMessage result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    Task<List<Pomaster>> readTask = result.Content.ReadAsAsync<List<Pomaster>>();
                    readTask.Wait();

                    pomasters = readTask.Result;
                }
            };
            return View(pomasters);
        }

        // GET: Pomasters/Details/5
        public ActionResult Details(string id)
        {
            Pomaster pomaster = new Pomaster();
            using (client)
            {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url + "/" + id);
                responseTask.Wait();

                //To store result of web api response.   
                HttpResponseMessage result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    Task<Pomaster> readTask = result.Content.ReadAsAsync<Pomaster>();
                    readTask.Wait();

                    pomaster = readTask.Result;
                }
            };
            return View(pomaster);
        }

        // GET: Pomasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pomasters/Create
        [HttpPost]
        public ActionResult Create(Pomaster pomaster)
        {
            try
            {
                using (client)
                {
                    Task<HttpResponseMessage> responseTask = client.PostAsJsonAsync(url, pomaster);
                    responseTask.Wait();

                    //To store result of web api response.   
                    HttpResponseMessage result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        Task<Pomaster> readTask = result.Content.ReadAsAsync<Pomaster>();
                        readTask.Wait();

                        pomaster = readTask.Result;
                    }
                };
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pomasters/Edit/5
        public ActionResult Edit(string id)
        {
            Pomaster pomaster = new Pomaster();
            using (client)
            {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url + "/" + id);
                responseTask.Wait();

                //To store result of web api response.   
                HttpResponseMessage result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    Task<Pomaster> readTask = result.Content.ReadAsAsync<Pomaster>();
                    readTask.Wait();

                    pomaster = readTask.Result;
                }
            };
            return View(pomaster);
        }

        // POST: Pomasters/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Pomaster pomaster)
        {
            try
            {
                using (client)
                {
                    Task<HttpResponseMessage> responseTask = client.PutAsJsonAsync(url + "/" + id, pomaster);
                    responseTask.Wait();

                    //To store result of web api response.   
                    HttpResponseMessage result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        Task<Pomaster> readTask = result.Content.ReadAsAsync<Pomaster>();
                        readTask.Wait();

                        pomaster = readTask.Result;
                    }
                };

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pomasters/Delete/5
        public ActionResult Delete(string id)
        {
            Pomaster pomaster = new Pomaster();
            using (client)
            {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url + "/" + id);
                responseTask.Wait();

                //To store result of web api response.   
                HttpResponseMessage result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    Task<Pomaster> readTask = result.Content.ReadAsAsync<Pomaster>();
                    readTask.Wait();

                    pomaster = readTask.Result;
                }
            };
            return View(pomaster);
        }

        // POST: Pomasters/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Pomaster pomaster)
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
                        Task<Pomaster> readTask = result.Content.ReadAsAsync<Pomaster>();
                        readTask.Wait();

                        pomaster = readTask.Result;
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
