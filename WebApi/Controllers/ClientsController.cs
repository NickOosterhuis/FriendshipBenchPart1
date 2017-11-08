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
    [Route("api/Clients")]
    public class ClientsController : Controller
    {
        private readonly UserDBContext _context;

        public ClientsController(UserDBContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public IEnumerable<ClientUser> GetClient()
        {
            return _context.Client;
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientUser = await _context.Client.SingleOrDefaultAsync(m => m.Id == id);

            if (clientUser == null)
            {
                return NotFound();
            }

            return Ok(clientUser);
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientUser([FromRoute] string id, [FromBody] ClientUser clientUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clientUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientUserExists(id))
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

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientUser = await _context.Client.SingleOrDefaultAsync(m => m.Id == id);
            if (clientUser == null)
            {
                return NotFound();
            }

            _context.Client.Remove(clientUser);
            await _context.SaveChangesAsync();

            return Ok(clientUser);
        }

        private bool ClientUserExists(string id)
        {
            return _context.Client.Any(e => e.Id == id);
        }
    }
}