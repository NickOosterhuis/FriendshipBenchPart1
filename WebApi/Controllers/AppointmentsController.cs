using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Models;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Appointments")]
    public class AppointmentsController : Controller
    {
        private readonly AppointmentDBContext _context;

        public AppointmentsController(AppointmentDBContext context)
        {
            _context = context;
        }

        // GET: api/Appointments
        [HttpGet]
        public IEnumerable<AppointmentGetViewModel> GetAppointments()
        {
            List<AppointmentGetViewModel> appointments = new List<AppointmentGetViewModel>();
            foreach(Appointment appointment in _context.Appointments)
            {
                appointments.Add(new AppointmentGetViewModel
                {
                    Id = appointment.Id,
                    Date = appointment.Date,
                    Time = appointment.Time,
                    Status = _context.AppointmentStatuses.Find(appointment.StatusId),
                    Bench = _context.Benches.Find(appointment.BenchId),
                    ClientId = appointment.ClientId,
                    HealthworkerName = appointment.HealthworkerName
                });
            }

            return appointments;
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appointment = await _context.Appointments.SingleOrDefaultAsync(m => m.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            AppointmentGetViewModel viewModel = new AppointmentGetViewModel
            {
                Id = appointment.Id,
                Date = appointment.Date,
                Time = appointment.Time,
                Status = _context.AppointmentStatuses.Find(appointment.StatusId),
                Bench = _context.Benches.Find(appointment.BenchId),
                ClientId = appointment.ClientId,
                HealthworkerName = appointment.HealthworkerName
            };

            return Ok(viewModel);
        }

        // PUT: api/Appointments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment([FromRoute] int id, [FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appointment.Id)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
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

        // POST: api/Appointments
        [HttpPost]
        public async Task<IActionResult> PostAppointment([FromBody] AppointmentPostViewModel appointmentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appointment = new Appointment()
            {
              Date = appointmentViewModel.Date,
              Time = appointmentViewModel.Time,
              StatusId = 1,
              BenchId = appointmentViewModel.BenchId,
              ClientId = appointmentViewModel.ClientId,
              HealthworkerName = appointmentViewModel.HealthworkerName
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointment", new { id = appointment.Id }, appointment);
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }
    }
}