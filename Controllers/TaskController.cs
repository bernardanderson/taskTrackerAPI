using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using taskTracker.Data;
using taskTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace taskTrackerAPI.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private taskTrackerContext context;

        public ProductsController(taskTrackerContext ctx)
        {
            context = ctx;
        }
        
        // GET /tasks
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> tasks = from task in context.Task select task;

            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);

        }

        // GET /tasks/5
        [HttpGet("{id}", Name = "GetTask")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Task task = context.Task.Single(m => m.TaskId == id);

                if (task == null)
                {
                    return NotFound();
                }
                
                return Ok(task);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }


        }

        // POST /tasks
        [HttpPost]
        public IActionResult Post([FromBody] Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Task.Add(task);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TaskExists(task.TaskId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetTask", new { id = task.TaskId }, task);
        }

        // PUT tasks/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Task task)
        {
            if (task == null || task.TaskId != id)
            {
                return BadRequest();
            }

            context.Entry(task).State = EntityState.Modified;
            if (task == null)
            {
                return NotFound();
            }
            context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE tasks/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Task task = context.Task.Single(m => m.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            context.Task.Remove(task);
            context.SaveChanges();
            return new NoContentResult();
        }

        private bool TaskExists(int id)
        {
            return context.Task.Count(e => e.TaskId == id) > 0;
        }
    }
}
