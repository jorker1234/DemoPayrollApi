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
    public class SubjectController : Controller
    {
        private PayrollContext _context;
        public SubjectController()
        {
            _context = new PayrollContext();
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult> Get(string? keyword, int limit = 100, int skip = 0)
        {
            var subjects = (IQueryable<Subject>) _context.Subjects;
            if(keyword != null)
            {
                subjects = subjects.Where(o => o.Name.Contains(keyword));
            }
            subjects = subjects.Skip(skip).Take(limit);
            var results = subjects.ToList();

            return Json(results);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var subject = await _context.Subjects.Select(o => new Subject
            {
                Id = o.Id,
                Name = o.Name,
                Price = o.Price,
                Unit = o.Unit,
                Students = o.Students.Select(x => new Student
                {
                    Id = x.Id,
                    Firstname = x.Firstname,
                    Lastname = x.Lastname,
                }).ToList()
            }).FirstOrDefaultAsync(o => o.Id == id);
            if (subject == null)
            {
                return NotFound();
            }
            return Json(subject);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Subject value)
        {
            _context.Subjects.Add(value);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Subject value)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            subject.Name = value.Name;
            subject.Price = value.Price;
            subject.Unit = value.Unit;
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
