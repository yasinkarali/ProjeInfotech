using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YazilimKurs.Entity.Concrete;

namespace YazilimKurs.Data
{
    public class YazilimKursDbContext : DbContext
    {
        public YazilimKursDbContext(DbContextOptions  options) : base(options)
        {
            
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<CourseStudent> CoursesStudents { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardItem> CardItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseStudent>().HasKey(cs=> new {cs.StudentId, cs.CourseId});
            base.OnModelCreating(modelBuilder);
        }
    }

}
