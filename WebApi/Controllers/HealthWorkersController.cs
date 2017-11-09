using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/HealthWorkers")]
    public class HealthWorkersController : Controller
    {
        private readonly UserDBContext _context;

        public HealthWorkersController(UserDBContext context)
        {
            _context = context;
        }

        // GET: api/HealthWorkers
        [HttpGet]
        public IEnumerable<HealthWorkerUser> GetHealthWorker()
        {
            return _context.HealthWorker;
        }

        // GET: api/HealthWorkers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHealthWorkerUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var healthWorkerUser = await _context.HealthWorker.SingleOrDefaultAsync(m => m.Id == id);

            if (healthWorkerUser == null)
            {
                return NotFound();
            }

            return Ok(healthWorkerUser);
        }

        // PUT: api/HealthWorkers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHealthWorkerUser([FromRoute] string id, [FromBody] HealthWorkerUser healthWorkerUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != healthWorkerUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(healthWorkerUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthWorkerUserExists(id))
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

        // DELETE: api/HealthWorkers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHealthWorkerUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var healthWorkerUser = await _context.HealthWorker.SingleOrDefaultAsync(m => m.Id == id);
            if (healthWorkerUser == null)
            {
                return NotFound();
            }

            _context.HealthWorker.Remove(healthWorkerUser);
            await _context.SaveChangesAsync();

            return Ok(healthWorkerUser);
        }

        private bool HealthWorkerUserExists(string id)
        {
            return _context.HealthWorker.Any(e => e.Id == id);
        }
    }
}