using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PgClientApplication.Models;

namespace PgClientApplication.Controllers
{
    public class PgListController : Controller
    {
      
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PgListController));
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                _log4net.Info("token not found");

                return RedirectToAction("Login");

            }
            else
            {
                _log4net.Info("Productlist getting Displayed");

                List<Pg> ItemList = new List<Pg>();
                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44345/api/Pg"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ItemList = JsonConvert.DeserializeObject<List<Pg>>(apiResponse);
                    }
                }
                return View(ItemList);

            }
        }
        public async Task<IActionResult> Book(int id)
        {
            _log4net.Info("Booking in progess");
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {

                Pg Item = new Pg();
                Book b = new Book();
                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44345/api/Pg/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Item = JsonConvert.DeserializeObject<Pg>(apiResponse);
                    }
                    b.pg_Id = Item.pg_id;
                    b.UserId = Convert.ToInt32(HttpContext.Session.GetInt32("Userid"));
                    b.BookingDate = DateTime.Now;
                    b.No_ofMonth = 0;
                    b.TotalCost = 0;
                }
                return View(b);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(Book b)
        {
            _log4net.Info("Booking Done");
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {

                Pg p = new Pg();

                using (var client = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    
                    using (var response = await client.GetAsync("https://localhost:44345/api/Pg/" + b.pg_Id))
                    {

                        string apiResponse = await response.Content.ReadAsStringAsync();
                        p = JsonConvert.DeserializeObject<Pg>(apiResponse);
                    }
                 
                    b.TotalCost = b.No_ofMonth * p.Rent_per_month;
                    b.UserId= Convert.ToInt32(HttpContext.Session.GetInt32("Userid"));




                    StringContent content = new StringContent(JsonConvert.SerializeObject(b), Encoding.UTF8, "application/json");

                    using (var response = await client.PostAsync("https://localhost:44372/api/Booking/", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        b = JsonConvert.DeserializeObject<Book>(apiResponse);       
                    }
                }
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> GetBookingItems(int id)
        {
            _log4net.Info("Getting Booking details");
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {
                List<Book> item = new List<Book>();
                ViewBag.Username = HttpContext.Session.GetString("Username");


                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    if (HttpContext.Session.GetInt32("Userid") != null)
                    { id = Convert.ToInt32(HttpContext.Session.GetInt32("Userid")); }

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("https://localhost:44372/api/Booking/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        item = JsonConvert.DeserializeObject<List<Book>>(apiResponse);
                    }
                }
                return View(item);
            }


        }
    }
    
    }
