using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListUPD.Models;

namespace ToDoListUPD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly GoalsDBContext _context;

        public GoalsController(GoalsDBContext context)
        {
            _context = context;
        }

        // GET: api/Goals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Goals>>> GetGoals()
        {
            return await _context.Goals.ToListAsync();
        }

        // GET: api/Goals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Goals>> GetGoals(int id)
        {
            var goals = await _context.Goals.FindAsync(id);

            if (goals == null)
            {
                return NotFound();
            }

            return goals;
        }

        // PUT: api/Goals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoals(int id, Goals goals)
        {
            if (id != goals.GoalId)
            {
                return BadRequest();
            }

            _context.Entry(goals).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoalsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Goals
        [HttpPost]
        public async Task<ActionResult<Goals>> PostGoals(Goals goals)
        {
            _context.Goals.Add(goals);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGoals", new { id = goals.GoalId }, goals);
        }

        // DELETE: api/Goals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Goals>> DeleteGoals(int id)
        {
            var goals = await _context.Goals.FindAsync(id);
            if (goals == null)
            {
                return NotFound();
            }

            _context.Goals.Remove(goals);
            await _context.SaveChangesAsync();

            return goals;
        }

        private bool GoalsExists(int id)
        {
            return _context.Goals.Any(e => e.GoalId == id);
        }
    }
}
