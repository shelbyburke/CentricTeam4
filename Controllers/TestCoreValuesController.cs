using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using CentricTeam4.DAL;
using CentricTeam4.Models;
using Microsoft.AspNet.Identity;

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
            var recognizor = db.userData.OrderBy(c => c.lastName).ThenBy(c => c.firstName);
            ViewBag.recognizor = new SelectList(recognizor, "ID", "fullName");

            string recId = User.Identity.GetUserId();
            SelectList recognized = new SelectList(db.userData, "ID", "fullName");
            recognized = new SelectList(recognized.Where(x => x.Value != recId).ToList(), "Value", "Text");
            ViewBag.recognized = recognized;


            var rec = db.TestCoreValues.Where(r => r.ID == id);
            var recList = rec.ToList();
            ViewBag.rec = recList;

            var totalCount = recList.Count(); //counts all the recognitions for that person

            var rec1Cnt = recList.Where(r => r.award == TestCoreValues.CoreValue.Excellence).Count();
            //counts all the excellance recongitions
            //notice how the Enum values are references, class.enum.value
            //the next two lines show another way to do the same counting
            var rec2Cnt = recList.Count(r => r.award == TestCoreValues.CoreValue.Integrity);
            var rec3Cnt = recList.Count(r => r.award == TestCoreValues.CoreValue.Stewardship);
            var rec4Cnt = recList.Count(r => r.award == TestCoreValues.CoreValue.Culture);
            var rec5Cnt = recList.Count(r => r.award == TestCoreValues.CoreValue.Commitment);
            var rec6Cnt = recList.Count(r => r.award == TestCoreValues.CoreValue.Innovation);
            var rec7Cnt = recList.Count(r => r.award == TestCoreValues.CoreValue.Balance);
            //copy the values into the ViewBag
            ViewBag.total = totalCount;
            ViewBag.Excellance = rec1Cnt;
            ViewBag.Integrity = rec2Cnt;
            ViewBag.Stewardship = rec3Cnt;
            ViewBag.Culture = rec4Cnt;
            ViewBag.Commitment = rec5Cnt;
            ViewBag.Innovation = rec6Cnt;
            ViewBag.Balance = rec7Cnt;
            
            return View(testCoreValues);
        }

        // GET: TestCoreValues/Create
        [Authorize]
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

                {
                    string notification = "Nomination for a core value award sent to :<br/>";
                    var personRecognized = db.userData.Find(testCoreValues.recognized);
                    var fullName = testCoreValues.personRecognized;
                    var recognition = testCoreValues.award;
                    var email = personRecognized.Email;
                    var date = testCoreValues.recognitionDate;
                    var msg = "Hi " + fullName + ",\n\nWe wanted to inform you that you have been recognized for " + recognition;
                    msg += " on " + date.ToShortDateString() + ".";
                    msg += "\n\nCongratulations on being nominated for an award.";
                    msg += "\n\nSincerely\nCentric Consulting Team";
                    notification += "<br/>" + fullName + " has been nominated for " + recognition + " on " + date.ToShortDateString() + ".";

                    MailMessage myMessage = new MailMessage();
                    MailAddress from = new MailAddress("CentricTeam4@gmail.com", "SysAdmin");
                    myMessage.From = from;
                    myMessage.To.Add(email);
                    myMessage.Subject = "Congratulations on being nominated!";
                    myMessage.Body = msg;
                    try
                    {
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential("CentricTeam4@gmail.com", "CentricTe@m4!");
                        smtp.EnableSsl = true;
                        smtp.Send(myMessage);
                        TempData["mailError"] = "";
                    }
                    catch  (Exception ex)
                    {
                        // this captures an exception and allows you to display the message in the View
                        TempData["mailError"] = ex.Message;
                        return View("mailError");
                    }
                    ViewBag.notification = notification;
                }
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
                var recognizor = db.userData.OrderBy(c => c.lastName).ThenBy(c => c.firstName);
                ViewBag.recognizor = new SelectList(recognizor, "ID", "fullName");

                ViewBag.recognized = new SelectList(recognizor, "ID", "fullName");

 //           if (id == null)
 //           {
 //               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
 //           }
 ////           User user = db.userData.Find(id);
 //           if (user == null)
 //           {
 //               return HttpNotFound();
 //           }
 //           Guid userId;
 //           Guid.TryParse(User.Identity.GetUserId(), out userId);
 //           if (user.ID == userId)
 //           {
 //               return View(user);
 //           }
 //           else
 //           {
 //               return View("Not Authenicated");
 //           }
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
            var recognizor = db.userData.OrderBy(c => c.lastName).ThenBy(c => c.firstName);
            ViewBag.recognizor = new SelectList(recognizor, "ID", "fullName");

            ViewBag.recognized = new SelectList(recognizor, "ID", "fullName");


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
