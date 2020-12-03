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
using Microsoft.AspNet.Identity;

namespace CentricTeam4.Controllers
{
    public class LeaderboardController : Controller
    {
        private MIS4200Context db = new MIS4200Context();

        // GET: Leaderboard
        public ActionResult Index()
        {
          // var recognizedID = 
         //   var leaderboardRec = db.TestCoreValues.Where(t => t.recognized == recognizedID);
         //   var leaderboardRecList = leaderboardRec.ToList();
          //  ViewBag.leaderboardRec = leaderboardRecList;
          //  var totalCnt = leaderboardRecList.Count();

            return View(db.userData.ToList());
        }

        // GET: Leaderboard/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userData userData = db.userData.Find(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            return View(userData);
        }

        // GET: Leaderboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Leaderboard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,lastName,firstName,PhoneNumber,Position,Location,BusinessUnit,hireDate,bio")] userData userData)
        {
            if (ModelState.IsValid)
            {
                userData.ID = Guid.NewGuid();
                db.userData.Add(userData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userData);
        }

        // GET: Leaderboard/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userData userData = db.userData.Find(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            return View(userData);
        }

        // POST: Leaderboard/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,lastName,firstName,PhoneNumber,Position,Location,BusinessUnit,hireDate,bio")] userData userData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userData);
        }

        // GET: Leaderboard/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userData userData = db.userData.Find(id);
            if (userData == null)
            {
                return HttpNotFound();
            }
            return View(userData);
        }

        // POST: Leaderboard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            userData userData = db.userData.Find(id);
            db.userData.Remove(userData);
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
