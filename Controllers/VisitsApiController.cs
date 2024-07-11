using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentDashboard.Data;
using StudentDashboard.Models;

namespace StudentDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitsApiController : ControllerBase
    {
        private readonly StudentDashboardContext _context;

        public VisitsApiController(StudentDashboardContext context)
        {
            _context = context;
        }

        // GET: api/VisitsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Visits>>> GetVisits()
        {
            return await _context.Visits.ToListAsync();
        }

        // GET: api/VisitsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Visits>> GetVisits(int id)
        {
            var visits = await _context.Visits.FindAsync(id);

            if (visits == null)
            {
                return NotFound();
            }

            return visits;
        }

        // PUT: api/VisitsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisits(int id, Visits visits)
        {
            if (id != visits.Id)
            {
                return BadRequest();
            }

            _context.Entry(visits).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitsExists(id))
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

        // POST: api/VisitsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Visits>> PostVisits(Visits visits)
        {
            _context.Visits.Add(visits);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVisits", new { id = visits.Id }, visits);
        }

        // DELETE: api/VisitsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisits(int id)
        {
            var visits = await _context.Visits.FindAsync(id);
            if (visits == null)
            {
                return NotFound();
            }

            _context.Visits.Remove(visits);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VisitsExists(int id)
        {
            return _context.Visits.Any(e => e.Id == id);
        }
    }
}
