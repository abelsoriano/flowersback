using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using FlowersBack.Models;
using System.IO;
using System;
using FlowersBack.Classes;

namespace FlowersBack.Controllers.API
{
    public class FlowersController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Flowers
        public IQueryable<Flower> GetFlowers()
        {
            return db.Flowers;
        }

        // GET: api/Flowers/5
        [ResponseType(typeof(Flower))]
        public IHttpActionResult GetFlower(int id)
        {
            Flower flower = db.Flowers.Find(id);
            if (flower == null)
            {
                return NotFound();
            }

            return Ok(flower);
        }

        // PUT: api/Flowers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFlower(int id, FlowerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.FlowerId)
            {
                return BadRequest();
            }

            if (request.ImageArray != null && request.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(request.ImageArray);
                var guid = Guid.NewGuid().ToString(); //codigo irrepetible, se usa para ids
                var file = string.Format("{0}.jpg", guid);
                var folder = "~/Content/Images";
                var fullPath = string.Format("{0},{1}", folder, file);
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    request.Image = fullPath;
                }
            }

            var flower = ToFlower(request);
            db.Entry(flower).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlowerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Flowers
        [ResponseType(typeof(Flower))]
        public IHttpActionResult PostFlower(FlowerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (request.ImageArray != null && request.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(request.ImageArray);
                var guid = Guid.NewGuid().ToString(); //codigo irrepetible, se usa para ids
                var file = string.Format("{0}.jpg", guid);
                var folder = "~/Content/Images";
                var fullPath = string.Format("{0},{1}", folder, file);
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    request.Image = fullPath;
                }
            }
            var flower = ToFlower(request);
            db.Flowers.Add(flower);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = flower.FlowerId }, flower);
        }

        private Flower ToFlower(FlowerRequest request)
        {
            return new Flower
            {
                Description = request.Description,
                FlowerId = request.FlowerId,
                Image = request.Image,
                IsActive = request.IsActive,
                LastPurchase = request.LastPurchase,
                Observation = request.Observation,
                Price = request.Price,
            };
        }

        // DELETE: api/Flowers/5
        [ResponseType(typeof(Flower))]
        public IHttpActionResult DeleteFlower(int id)
        {
            Flower flower = db.Flowers.Find(id);
            if (flower == null)
            {
                return NotFound();
            }

            db.Flowers.Remove(flower);
            db.SaveChanges();

            return Ok(flower);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FlowerExists(int id)
        {
            return db.Flowers.Count(e => e.FlowerId == id) > 0;
        }
    }
}