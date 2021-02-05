using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrollApi.Data;
using PayrollApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PayrollApi.Controllers
{
    [Route("api/[controller]")]
    public class PayrollController : Controller
    {
        private PayrollContext _context;

        public PayrollController()
        {
            _context = new PayrollContext();
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post(int studentId, int subjectId)
        {
            var student = await _context.Students.Include(x => x.Subjects).FirstOrDefaultAsync(o => o.Id == studentId);
            if (student == null)
            {
                return NotFound();
            }
            if(student.Subjects.Any(o => o.Id == subjectId))
            {
                return BadRequest();
            }
            var subject = await _context.Subjects.FirstOrDefaultAsync(o => o.Id == subjectId);
            if (subject == null)
            {
                return NotFound();
            }
            student.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{studentId}/{subjectId}")]
        public async Task<ActionResult> Delete(int studentId, int subjectId)
        {
            var student = await _context.Students.Include(x => x.Subjects).FirstOrDefaultAsync(o => o.Id == studentId);
            if (student == null)
            {
                return NotFound();
            }
            if (!student.Subjects.Any(o => o.Id == subjectId))
            {
                return BadRequest();
            }
            var subject = await _context.Subjects.FindAsync(subjectId);
            if (subject == null)
            {
                return NotFound();
            }
            student.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
