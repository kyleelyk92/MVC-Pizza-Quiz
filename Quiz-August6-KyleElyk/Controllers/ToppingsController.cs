﻿using System;
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
    public class ToppingsController : Controller
    {

        private RoleManager<IdentityRole> rolesManager;
        private UserManager<IdentityUser> usersManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Toppings
        public ActionResult Index()
        {
            return View(db.Toppings.ToList());
        }

        // GET: Toppings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topping topping = db.Toppings.Find(id);
            if (topping == null)
            {
                return HttpNotFound();
            }
            return View(topping);
        }

        // GET: Toppings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Toppings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price")] Topping topping)
        {
            if (ModelState.IsValid)
            {
                db.Toppings.Add(topping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(topping);
        }

        // GET: Toppings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topping topping = db.Toppings.Find(id);
            if (topping == null)
            {
                return HttpNotFound();
            }
            return View(topping);
        }

        // POST: Toppings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price")] Topping topping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(topping);
        }

        // GET: Toppings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topping topping = db.Toppings.Find(id);
            if (topping == null)
            {
                return HttpNotFound();
            }
            return View(topping);
        }

        // POST: Toppings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Topping topping = db.Toppings.Find(id);
            db.Toppings.Remove(topping);
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
    }
}
