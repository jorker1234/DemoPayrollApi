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
    public class StudentController : Controller
    {
        private PayrollContext _context;

        public StudentController()
        {
            _context = new PayrollContext();
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult> Get(string? keyword, int limit = 100, int skip = 0)
        {
            var students = (IQueryable<Student>)_context.Students;
            if (keyword != null)
            {
                students = students.Where(o => o.Firstname.Contains(keyword) || o.Lastname.Contains(keyword));
            }
            students = students.Skip(skip).Take(limit);
            var results = students.ToList();
            return Json(results);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var student = await _context.Students.Select(o => new Student
            {
                Id = o.Id,
                Firstname = o.Firstname,
                Lastname = o.Lastname,
                Subjects = o.Subjects.Select(x => new Subject
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Unit = x.Unit,
                }).ToList()
            }).FirstOrDefaultAsync(o => o.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return Json(student);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Student value)
        {
            _context.Students.Add(value);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Student value)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            student.Firstname = value.Firstname;
            student.Lastname = value.Lastname;
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
