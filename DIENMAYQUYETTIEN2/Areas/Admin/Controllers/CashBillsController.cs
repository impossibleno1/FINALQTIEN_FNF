﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DIENMAYQUYETTIEN2.Models;
using System.Transactions;
using DIENMAYQUYETTIEN2.Areas.Admin.Models;

namespace DIENMAYQUYETTIEN2.Areas.Admin.Controllers
{
    public class CashBillsController : Controller
    {
        private DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities();

        // GET: Admin/CashBills
        public ActionResult Index()
        {
            var cashbill = db.CashBills.Include(b => b.CashBillDetails).ToList();

            //if (Session["Username"] != null)
            //{
            //    return View(cashbill);
            //}
            //else
            //{
            //    return RedirectToAction("Login");
            //}
            return View(cashbill);
        }


        // GET: Admin/CashBills/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(Session["CashBill"]);
        }

        // POST: Admin/CashBills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CashBill model)
        {
            if (ModelState.IsValid)
            {
                Session["CashBill"] = model;
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
                var cashBill = Session["CashBill"] as CashBill;
                var ctcashBill = Session["ctcashBill"] as List<CashBillDetail>;

                db.CashBills.Add(cashBill);
                db.SaveChanges();

                foreach (var chiTiet in ctcashBill)
                {
                    chiTiet.BillID = cashBill.ID;
                    chiTiet.Product = null;
                    db.CashBillDetails.Add(chiTiet);
                    cashBill.GrandTotal += (chiTiet.Quantity * chiTiet.SalePrice);
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

        // GET: Admin/CashBills/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBill cashBill = db.CashBills.Find(id);
            if (cashBill == null)
            {
                return HttpNotFound();
            }
            return View(cashBill);
        }

        // POST: Admin/CashBills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BillCode,CustomerName,PhoneNumber,Address,Date,Shipper,Note,GrandTotal")] CashBill cashBill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cashBill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cashBill);
        }

        // GET: Admin/CashBills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBill cashBill = db.CashBills.Find(id);
            if (cashBill == null)
            {
                return HttpNotFound();
            }
            return View(cashBill);
        }

        // POST: Admin/CashBills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CashBill cashBill = db.CashBills.Find(id);
            db.CashBills.Remove(cashBill);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Print(int id)
        {
            var order = db.CashBills.FirstOrDefault(o => o.ID == id);
            if (order != null)
            {
                ReceiptModel rp = new ReceiptModel();
                rp.Address = order.Address;
                rp.BillCode = order.BillCode;
                rp.CustomerName = order.CustomerName;
                rp.Date = order.Date;
                rp.GrandTotal = order.GrandTotal;
                rp.Note = order.Note;
                rp.PhoneNumber = order.PhoneNumber;
                rp.Shipper = order.Shipper;
                rp.CashBillDetail = order.CashBillDetails.ToList();
                return View(rp);
            }
            else
            {
                return View();
            }
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
