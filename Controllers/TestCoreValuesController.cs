﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CentricTeam4.DAL;
using CentricTeam4.Models;

namespace CentricTeam4.Controllers
{
    public class TestCoreValuesController : Controller
    {
        private MIS4200Context db = new MIS4200Context();

        // GET: TestCoreValues
        public ActionResult Index()
        {
            return View(db.TestCoreValues.ToList());
        }

        // GET: TestCoreValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestCoreValues testCoreValues = db.TestCoreValues.Find(id);
            if (testCoreValues == null)
            {
                return HttpNotFound();
            }
            return View(testCoreValues);
        }

        // GET: TestCoreValues/Create
        public ActionResult Create()
        {
            var recognizor = db.userData.OrderBy(c => c.lastName).ThenBy(c => c.firstName);
            ViewBag.recognizor = new SelectList(recognizor, "ID", "fullName");

            ViewBag.recognized = new SelectList(recognizor, "ID", "fullName");
            return View();
        }

        // POST: TestCoreValues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,award,recognizor,recognized,recognitionDate,phone,title,description")] TestCoreValues testCoreValues)
        {
            if (ModelState.IsValid)
            {
                db.TestCoreValues.Add(testCoreValues);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var recognizor = db.userData.OrderBy(c => c.lastName).ThenBy(c => c.firstName);
            ViewBag.recognizor = new SelectList(recognizor, "ID", "fullName");

            ViewBag.recognized = new SelectList(recognizor, "ID", "fullName");
            return View(testCoreValues);
        }

        // GET: TestCoreValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestCoreValues testCoreValues = db.TestCoreValues.Find(id);
            if (testCoreValues == null)
            {
                return HttpNotFound();
            }
            return View(testCoreValues);
        }

        // POST: TestCoreValues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,award,recognizor,recognized,recognitionDate,phone,title,description")] TestCoreValues testCoreValues)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testCoreValues).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testCoreValues);
        }

        // GET: TestCoreValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestCoreValues testCoreValues = db.TestCoreValues.Find(id);
            if (testCoreValues == null)
            {
                return HttpNotFound();
            }
            return View(testCoreValues);
        }

        // POST: TestCoreValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestCoreValues testCoreValues = db.TestCoreValues.Find(id);
            db.TestCoreValues.Remove(testCoreValues);
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