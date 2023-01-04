using Microsoft.EntityFrameworkCore;
using MovieProject.Repository.Entities;

namespace MovieProject.Repository
{
    //เชื่อมต่อกับฐานข้อมูล
    public class MyProjectContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source=localhost\\SQLEXPRESS;Initial Catalog=moviedb;Trusted_Connection=True;TrustServerCertificate=True");
        }
        public DbSet<MovieModel> MovieTB { get; set; } //ตัวแปร Table ชื่อ MovieTB ไว้รับค่าส่งค่าให้กับ ฐานข้อมูล
    }
}