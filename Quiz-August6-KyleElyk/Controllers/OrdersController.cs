using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Quiz_August6_KyleElyk.Models;

namespace Quiz_August6_KyleElyk.Controllers
{
    public class OrdersController : Controller
    {


        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.ApplicationUser);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        [Authorize(Roles="Manager")]
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,OrderDate,ApplicationUserId,Paid")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", order.ApplicationUserId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", order.ApplicationUserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,OrderDate,ApplicationUserId,Paid")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", order.ApplicationUserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult CustomerOrders(string userId)
        {
            var user = db.Users.Find(userId);
            var orders = db.Orders.Where(o => o.ApplicationUserId == user.Id);
            ViewBag.User = user;

            return View(orders);
        }

        public ActionResult ReviewOrder(int OrderId)
        {
            var order = db.Orders.Find(OrderId);
            var pizzas = order.Pizzas;
            double total = 0;

            foreach (var pizza in pizzas)
            {
                total += pizza.Price;

                foreach (var topping in pizza.Toppings)
                {
                    total += topping.Price;
                }
            }
            ViewBag.Total = total;
            return View(order);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult OrderPayments()
        {
            var ordersList = db.Orders.Where(o => o.Paid == true);

            return View();
        }

        [Authorize(Roles = "Manager")]
        public ActionResult ReportingPage(DateTime? StartDate, DateTime? EndDate)
        {
            if (StartDate != null && EndDate != null)
            {
                var ordersInPeriod = db.Orders.Where(o => o.OrderDate >= StartDate && o.OrderDate <= EndDate);
                return View(ordersInPeriod);
            }
            return View();
        }


        public ActionResult BestMonth()
        {
            var paidOrders = db.Orders.Where(o => o.Paid == true).Include(o => o.Pizzas);
            var ordersSortedByDate = paidOrders.GroupBy(o => o.OrderDate.Month);
            var myDictionary = new Dictionary<int, double>();

            foreach (var ordersByMonth in ordersSortedByDate)
            {
                double total = 0;
                foreach (var order in ordersByMonth)
                {
                    foreach (var pizza in order.Pizzas)
                    {
                        total += pizza.Price;
                        foreach (var topping in pizza.Toppings)
                        {
                            total += topping.Price;
                        }
                    }
                }
                myDictionary.Add(ordersByMonth.Key, total);
            }

            ViewBag.BestMonth = myDictionary.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            return View();
        }
    }
}

