using AutoConnectToDB.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AutoConnectToDB.Persistence
{
    public class ContextForDB : DbContext
    {
        public ContextForDB(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }

    }
}
