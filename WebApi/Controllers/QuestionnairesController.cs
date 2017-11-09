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
    [Route("api/Questionnaires")]
    public class QuestionnairesController : Controller
    {
        private readonly QuestionnaireDBContext _context;

        public QuestionnairesController(QuestionnaireDBContext context)
        {
            _context = context;
        }

        // GET: api/Questionnaires
        [HttpGet]
        public IEnumerable<Questionnaire> GetQuestionnaire()
        {
            return _context.Questionnaire;
        }

        // GET: api/Questionnaires/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionnaire([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionnaire = await _context.Questionnaire.SingleOrDefaultAsync(m => m.Id == id);

            if (questionnaire == null)
            {
                return NotFound();
            }

            return Ok(questionnaire);
        }

        // PUT: api/Questionnaires/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionnaire([FromRoute] int id, [FromBody] Questionnaire questionnaire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != questionnaire.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionnaire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionnaireExists(id))
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

        // POST: api/Questionnaires
        [HttpPost]
        public async Task<IActionResult> PostQuestionnaire([FromBody] QuestionnairePostViewModel questionnaireViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Questionnaire questionnaire = new Questionnaire
            {
                Date = questionnaireViewModel.Time,
                Time = questionnaireViewModel.Time,
                Client_id = questionnaireViewModel.Client_id
            };

            _context.Questionnaire.Add(questionnaire);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionnaire", new { id = questionnaire.Id }, questionnaire);
        }

        // DELETE: api/Questionnaires/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionnaire([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionnaire = await _context.Questionnaire.SingleOrDefaultAsync(m => m.Id == id);
            if (questionnaire == null)
            {
                return NotFound();
            }

            _context.Questionnaire.Remove(questionnaire);
            await _context.SaveChangesAsync();

            return Ok(questionnaire);
        }

        private bool QuestionnaireExists(int id)
        {
            return _context.Questionnaire.Any(e => e.Id == id);
        }
    }
}