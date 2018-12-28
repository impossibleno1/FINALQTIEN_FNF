using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DIENMAYQUYETTIEN2.Models;

namespace DIENMAYQUYETTIEN2.Areas.Admin.Controllers
{
    public class CashBillDetailsController : Controller
    {
        private DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities();

        // GET: Admin/CashBillDetails
        public ActionResult Index()
        {
            if (Session["ctcashBill"]==null)
            {
                Session["ctcashBill"] = new List<CashBillDetail>();   
            }
            return PartialView(Session["ctcashBill"]);
        }

        // GET: /Admin/CashBillDetails/Details/5
        public int DonGiaBan(int ProductID)
        {
            return db.Products.Find(ProductID).SalePrice;
        }

        // GET: /Admin/CashBillDetails/Create
        public PartialViewResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductName");
            var model = new CashBillDetail();
            model.ID = 0;
            model.Quantity = 1;
            return PartialView(model);
        }

        // POST: Admin/CashBillDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2(CashBillDetail model)
        {
            if (ModelState.IsValid)
            {
                model.ID = Environment.TickCount;
                model.Product = db.Products.Find(model.ProductID);
                var ctcashBill = Session["ctcashBill"] as List<CashBillDetail>;
                if (ctcashBill == null)
                    ctcashBill = new List<CashBillDetail>();
                ctcashBill.Add(model);
                Session["ctcashBill"] = ctcashBill;
                return RedirectToAction("Create", "CashBills");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductName", model.ProductID);
            return View("Create", model);
        }

        // GET: Admin/CashBillDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBillDetail cashBillDetail = db.CashBillDetails.Find(id);
            if (cashBillDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.BillID = new SelectList(db.CashBills, "ID", "BillCode", cashBillDetail.BillID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductCode", cashBillDetail.ProductID);
            return View(cashBillDetail);
        }

        // POST: Admin/CashBillDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BillID,ProductID,Quantity,SalePrice")] CashBillDetail cashBillDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cashBillDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BillID = new SelectList(db.CashBills, "ID", "BillCode", cashBillDetail.BillID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductCode", cashBillDetail.ProductID);
            return View(cashBillDetail);
        }

        // GET: Admin/CashBillDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBillDetail cashBillDetail = db.CashBillDetails.Find(id);
            if (cashBillDetail == null)
            {
                return HttpNotFound();
            }
            return View(cashBillDetail);
        }

        // POST: Admin/CashBillDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CashBillDetail cashBillDetail = db.CashBillDetails.Find(id);
            db.CashBillDetails.Remove(cashBillDetail);
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
