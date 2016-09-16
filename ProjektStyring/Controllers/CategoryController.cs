using ProjektStyring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjektStyring.Controllers
{
    public class CategoryController : ApiController
    {
        private DataContext _db = new DataContext();


        [HttpGet]
        public List<Category> GetAll()
        {
            return _db.Categorys.OrderBy(c => c.Id).ToList();

        }

        [HttpGet]
        public IHttpActionResult GetById (int Id)
        {

            Category category = _db.Categorys.Where(c => c.Id == Id).FirstOrDefault();

            if(category == null)
            {
                return NotFound();

            }
            return Ok(category);
        }

        [HttpPost]
    
        public IHttpActionResult CreateCategory(Category model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Categorys.Add(model);
            _db.SaveChanges();

            return Ok(model);
        }


        [HttpDelete]
        public IHttpActionResult DeleteCategory(int Id)
        {

            Category category = _db.Categorys.Where(c => c.Id == Id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }

            _db.Categorys.Remove(category);
            _db.SaveChanges();

            return Ok(category);
        }
    }
}
