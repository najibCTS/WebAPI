using Posp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Posp.Controllers
{
    public class PodetailsController : Controller
    {
        HttpClient client;
        readonly string url = "http://localhost:54849/api/Podetails";

        public PodetailsController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Podetails
        public ActionResult Index()
        {
            List<Podetail> podetails = new List<Podetail>();
            using (client)
            {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url);
                responseTask.Wait();

                //To store result of web api response.   
                HttpResponseMessage result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    Task<List<Podetail>> readTask = result.Content.ReadAsAsync<List<Podetail>>();
                    readTask.Wait();

                    podetails = readTask.Result;
                }
            };
            return View(podetails);
        }

        // GET: Podetails/Details/5
        public ActionResult Details(string pono, string itcode)
        {
            Podetail podetail = new Podetail();
            using (client)
            {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url + "?pono=" + pono + "&itcode=" + itcode);
                responseTask.Wait();

                //To store result of web api response.   
                HttpResponseMessage result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    Task<Podetail> readTask = result.Content.ReadAsAsync<Podetail>();
                    readTask.Wait();

                    podetail = readTask.Result;
                }
            };
            return View(podetail);
        }

        // GET: Podetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Podetails/Create
        [HttpPost]
        public ActionResult Create(Podetail podetail)
        {
            try
            {
                using (client)
                {
                    Task<HttpResponseMessage> responseTask = client.PostAsJsonAsync(url, podetail);
                    responseTask.Wait();

                    //To store result of web api response.   
                    HttpResponseMessage result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        Task<Podetail> readTask = result.Content.ReadAsAsync<Podetail>();
                        readTask.Wait();

                        podetail = readTask.Result;
                    }
                };

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Podetails/Edit/5
        public ActionResult Edit(string pono, string itcode)
        {
            Podetail podetail = new Podetail();
            using (client)
            {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url + "?pono=" + pono + "&itcode=" + itcode);
                responseTask.Wait();

                //To store result of web api response.   
                HttpResponseMessage result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    Task<Podetail> readTask = result.Content.ReadAsAsync<Podetail>();
                    readTask.Wait();

                    podetail = readTask.Result;
                }
            };
            return View(podetail);
        }

        // POST: Podetails/Edit/5
        [HttpPost]
        public ActionResult Edit(string pono, string itcode, Podetail podetail)
        {
            try
            {
                using (client)
                {
                    Task<HttpResponseMessage> responseTask = client.PutAsJsonAsync(url + "?pono=" + pono + "&itcode=" + itcode, podetail);
                    responseTask.Wait();

                    //To store result of web api response.   
                    HttpResponseMessage result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        Task<Podetail> readTask = result.Content.ReadAsAsync<Podetail>();
                        readTask.Wait();

                        podetail = readTask.Result;
                    }
                };

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Podetails/Delete/5
        public ActionResult Delete(string pono, string itcode)
        {
            Podetail podetail = new Podetail();
            using (client)
            {
                Task<HttpResponseMessage> responseTask = client.GetAsync(url + "?pono=" + pono + "&itcode=" + itcode);
                responseTask.Wait();

                //To store result of web api response.   
                HttpResponseMessage result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    Task<Podetail> readTask = result.Content.ReadAsAsync<Podetail>();
                    readTask.Wait();

                    podetail = readTask.Result;
                }
            };
            return View(podetail);
        }

        // POST: Podetails/Delete/5
        [HttpPost]
        public ActionResult Delete(string pono, string itcode, Podetail podetail)
        {
            try
            {
                using (client)
                {
                    Task<HttpResponseMessage> responseTask = client.DeleteAsync(url + "?pono=" + pono + "&itcode=" + itcode);
                    responseTask.Wait();

                    //To store result of web api response.   
                    HttpResponseMessage result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        Task<Podetail> readTask = result.Content.ReadAsAsync<Podetail>();
                        readTask.Wait();

                        podetail = readTask.Result;
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
