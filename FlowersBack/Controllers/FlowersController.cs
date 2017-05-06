using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FlowersBack.Models;
using FlowersBack.Classes;

namespace FlowersBack.Controllers
{
    public class FlowersController : Controller
    {
        private DataContext db;

        public FlowersController()
        {
            db = new DataContext();
        }

        // GET: Flowers
        public ActionResult Index()
        {
            return View(db.Flowers.OrderBy(f=> f.Description).ToList());
        }

        // GET: Flowers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var flower = db.Flowers.Find(id);

            if (flower == null)
            {
                return HttpNotFound();
            }

            return View(flower);
        }

        // GET: Flowers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Flowers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "FlowerId,Description,Price,LastPurchase,Image,IsActive,Observation")] Flower flower)
        public ActionResult Create(FlowerView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                var flower = ToFlower(view);
                flower.Image = pic;
                //db.Flowers.Add(flower);
                db.Flowers.Add(flower);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view);
           // return View(flower);
        }

        private Flower ToFlower(FlowerView view)
        {
            return new Models.Flower
            {
                Description = view.Description,
                FlowerId = view.FlowerId,
                Image = view.Image,
                IsActive = view.IsActive,
                LastPurchase = view.LastPurchase,
                Observation = view.Observation,
                Price = view.Price,
            };
        }

        // GET: Flowers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Flower flower = db.Flowers.Find(id);
            var flower = db.Flowers.Find(id);

            if (flower == null)
            {
                return HttpNotFound();
            }

            //return View(flower);
            return View(ToView(flower));
        }

        private FlowerView ToView(Flower flower)
        {
            return new FlowerView
            {
                Description = flower.Description,
                FlowerId = flower.FlowerId,
                Image = flower.Image,
                IsActive = flower.IsActive,
                LastPurchase = flower.LastPurchase,
                Observation = flower.Observation,
                Price = flower.Price,
            };
        }

        // POST: Flowers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
     //   public ActionResult Edit([Bind(Include = "FlowerId,Description,Price,LastPurchase,Image,IsActive,Observation")] Flower flower)
      [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( FlowerView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.Image; //la inicializo en la imagen vieja por si el no la cambio no la dañe
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var flower = ToFlower(view); //convertimos a vista
                flower.Image = pic;
                db.Entry(flower).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    ex.ToString();
                }
              
            }

            return View(view);
        }

        // GET: Flowers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flower flower = db.Flowers.Find(id);
            if (flower == null)
            {
                return HttpNotFound();
            }
            return View(flower);
        }

        // POST: Flowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flower flower = db.Flowers.Find(id);
            db.Flowers.Remove(flower);
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
