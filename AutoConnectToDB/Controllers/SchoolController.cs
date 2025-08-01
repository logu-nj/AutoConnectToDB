using AutoConnectToDB.Persistence;
using AutoConnectToDB.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoConnectToDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolController : ControllerBase
    {
        public ContextForDB db;
        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            return await db.Students.ToListAsync();
        }
        [HttpPost]
        public Guid Post(Student obj)
        {
            db.Students.Add(obj);
            db.SaveChanges();
            return obj.Id;
        }
    }
}
