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
    [Route("api/AppointmentStatus")]
    public class AppointmentStatusController : Controller
    {
        private readonly AppointmentDBContext _context;

        public AppointmentStatusController(AppointmentDBContext context)
        {
            _context = context;
        }

        // GET: api/AppointmentStatus
        [HttpGet]
        public IEnumerable<AppointmentStatus> GetAppointmentStatuses()
        {
            return _context.AppointmentStatuses;
        }

        // GET: api/AppointmentStatus/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appointmentStatus = await _context.AppointmentStatuses.SingleOrDefaultAsync(m => m.Id == id);

            if (appointmentStatus == null)
            {
                return NotFound();
            }

            return Ok(appointmentStatus);
        }

        // PUT: api/AppointmentStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointmentStatus([FromRoute] int id, [FromBody] AppointmentStatus appointmentStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appointmentStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(appointmentStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentStatusExists(id))
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

        // POST: api/AppointmentStatus
        [HttpPost]
        public async Task<IActionResult> PostAppointmentStatus([FromBody] AppointmentStatus appointmentStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AppointmentStatuses.Add(appointmentStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointmentStatus", new { id = appointmentStatus.Id }, appointmentStatus);
        }

        // DELETE: api/AppointmentStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointmentStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appointmentStatus = await _context.AppointmentStatuses.SingleOrDefaultAsync(m => m.Id == id);
            if (appointmentStatus == null)
            {
                return NotFound();
            }

            _context.AppointmentStatuses.Remove(appointmentStatus);
            await _context.SaveChangesAsync();

            return Ok(appointmentStatus);
        }

        private bool AppointmentStatusExists(int id)
        {
            return _context.AppointmentStatuses.Any(e => e.Id == id);
        }
    }
}