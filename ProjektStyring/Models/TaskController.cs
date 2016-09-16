using ProjektStyring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjektStyring.Controllers
{       //hvor vores URL starter
    [RoutePrefix("api/Task")]
    public class TaskController : ApiController
    {
        private DataContext _db = new DataContext();

        //standard er vil en 'public action' altid gå ud fra at det er en GET methode, med mindre man skriver andet

        [HttpGet]
        public List<Task> GetAll()
        {
            return _db.Tasks.OrderBy(t => t.Id).ToList();

        }

        [HttpGet]
        public IHttpActionResult GetById(int Id)
        {

            Task task = _db.Tasks.Where(t => t.Id == Id).FirstOrDefault();

            if (task == null)
            {
                return NotFound();

            }
            return Ok(task);
        }

        [HttpPost]
        public IHttpActionResult CreateTask(Task model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            model.Created = DateTime.Now;

            _db.Tasks.Add(model);
            _db.SaveChanges();

            return Ok(model);
        }


        [HttpDelete]
        public IHttpActionResult DeleteTask(int Id)
        {

            Task task = _db.Tasks.Where(t => t.Id == Id).FirstOrDefault();
            if (task == null)
            {
                return NotFound();
            }

            _db.Tasks.Remove(task);
            _db.SaveChanges();

            return Ok(task);
        }


        [HttpPut]
        public IHttpActionResult UpdateTask(int Id )
        {
            Task task = _db.Tasks.Where(t => t.Id == Id).FirstOrDefault();

            if (task == null)
            {
                return NotFound();
            }

            task.Finished = !task.Finished;
           

            _db.Entry(task).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return Ok(task);

        }
        [HttpPut]
        public IHttpActionResult ChangeCategory(int Id, int CategoryId)
        {
            Task task = _db.Tasks.Where(t => t.Id == Id).FirstOrDefault();

            if (task == null)
            {
                return NotFound();
            }

            task.CategoryId = CategoryId;


            _db.Entry(task).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            return Ok(task);

        }


        [Route("search")]
        public List<Task> GetTaskByName (string name)
        {

            return _db.Tasks.Where(p => name != null ? p.Name.StartsWith(name) : true).ToList();


        }

    }


}
