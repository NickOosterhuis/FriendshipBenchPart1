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
using Microsoft.AspNetCore.Authorization;
using WebApi.ViewModels.HealthWorkers;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/HealthWorkers")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HealthWorkersController : Controller
    {
        private readonly UserDBContext _context;

        public HealthWorkersController(UserDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetHealthWorkers()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            List<HealthWorkerViewModel> healthWorkers = new List<HealthWorkerViewModel>();
            foreach (HealthWorkerUser healthWorker in _context.HealthWorker)
            {
                healthWorkers.Add(new HealthWorkerViewModel
                {
                    Id = healthWorker.Id,
                    Firstname = healthWorker.Firstname,
                    Lastname = healthWorker.Lastname,
                    Gender = healthWorker.Gender,
                    Birthday = healthWorker.Birthday,
                    Email = healthWorker.Email,
                    PhoneNumber = healthWorker.PhoneNumber,
                });
            }
            return Ok(healthWorkers); 
        }

        // GET: api/HealthWorkers/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

            HealthWorkerViewModel vm = new HealthWorkerViewModel();

            vm.Id = healthWorkerUser.Id;
            vm.Firstname = healthWorkerUser.Firstname;
            vm.Lastname = healthWorkerUser.Lastname;
            vm.Email = healthWorkerUser.Email;
            vm.Birthday = healthWorkerUser.Birthday;
            vm.Gender = healthWorkerUser.Gender;
            vm.PhoneNumber = healthWorkerUser.PhoneNumber;
            
            return Ok(vm);
        }

        // PUT: api/HealthWorkers/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutHealthWorkerUser([FromRoute] string id, [FromBody] HealthWorkerViewModel healthWorkerUser)
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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