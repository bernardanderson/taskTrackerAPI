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
    public class UserTasksController : Controller
    {
        private taskTrackerContext context;

        public UserTasksController(taskTrackerContext ctx)
        {
            context = ctx;
        }
        
        // GET /userTasks
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> userTasks = from userTask in context.UserTask select userTask;

            if (userTasks == null)
            {
                return NotFound();
            }

            return Ok(userTasks);

        }

        // GET /userTasks/5
        [HttpGet("{id}", Name = "GetUserTask")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                UserTask userTasks = context.UserTask.Single(m => m.TaskId == id);

                if (userTasks == null)
                {
                    return NotFound();
                }
                
                return Ok(userTasks);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }


        }

        // POST /userTasks
        [HttpPost]
        public IActionResult Post([FromBody] UserTask userTasks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.UserTask.Add(userTasks);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserTaskExists(userTasks.TaskId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetUserTask", new { id = userTasks.TaskId }, userTasks);
        }

        // PUT userTasks/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserTask userTasks)
        {
            if (userTasks == null || userTasks.TaskId != id)
            {
                return BadRequest();
            }

            context.Entry(userTasks).State = EntityState.Modified;
            if (userTasks == null)
            {
                return NotFound();
            }
            context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE userTasks/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            UserTask userTasks = context.UserTask.Single(m => m.TaskId == id);
            if (userTasks == null)
            {
                return NotFound();
            }

            context.UserTask.Remove(userTasks);
            context.SaveChanges();
            return new NoContentResult();
        }

        private bool UserTaskExists(int id)
        {
            return context.UserTask.Count(e => e.TaskId == id) > 0;
        }
    }
}
