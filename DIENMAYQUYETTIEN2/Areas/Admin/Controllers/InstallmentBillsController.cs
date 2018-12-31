using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DIENMAYQUYETTIEN2.Models;
using System.Transactions;

namespace DIENMAYQUYETTIEN2.Areas.Admin.Controllers
{
    public class InstallmentBillsController : Controller
    {
        private DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities();

        // GET: Admin/InstallmentBills
        public ActionResult Index()
        {
            var inscashbill = db.InstallmentBills.Include(b => b.InstallmentBillDetails).ToList();

            if (Session["Username"] != null)
            {
                return View(inscashbill);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: Admin/InstallmentBills/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(Session["InsCashBill"]);
        }

        // POST: Admin/InstallmentBills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InstallmentBill model)
        {
            if (ModelState.IsValid)
            {
                Session["InsCashBill"] = model;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2()
        {
            using (var scope = new TransactionScope())
                try
                {
                    var inscashBill = Session["InsCashBill"] as InstallmentBill;
                    var insctcashBill = Session["insctcashBill"] as List<InstallmentBill>;

                   // db.CashBills.Add(inscashBill);
                    db.SaveChanges();

                    foreach (var chiTiet in insctcashBill)
                    {
                     //   chiTiet. = cashBill.ID;
                       // chiTiet.Product = null;
                        //db.CashBillDetails.Add(chiTiet);
                        //cashBill.GrandTotal += (chiTiet.Quantity * chiTiet.SalePrice);
                    }

                    db.SaveChanges();
                    scope.Complete();

                    Session["CashBill"] = null;
                    Session["ctcashBill"] = null;
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            return View("Create");
        }

        // GET: Admin/InstallmentBills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallmentBill installmentBill = db.InstallmentBills.Find(id);
            if (installmentBill == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "CustomerCode", installmentBill.CustomerID);
            return View(installmentBill);
        }

        // POST: Admin/InstallmentBills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BillCode,CustomerID,Date,Shipper,Note,Method,Period,GrandTotal,Taken,Remain")] InstallmentBill installmentBill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(installmentBill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "CustomerCode", installmentBill.CustomerID);
            return View(installmentBill);
        }

        // GET: Admin/InstallmentBills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallmentBill installmentBill = db.InstallmentBills.Find(id);
            if (installmentBill == null)
            {
                return HttpNotFound();
            }
            return View(installmentBill);
        }

        // POST: Admin/InstallmentBills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InstallmentBill installmentBill = db.InstallmentBills.Find(id);
            db.InstallmentBills.Remove(installmentBill);
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
