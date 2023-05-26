using System;
using ServicesCentral.Models;
using Microsoft.EntityFrameworkCore;
using ServicesCentral.Models.Domain;

namespace ServicesCentral.Data
{
	public class ApplicationDBContext :DbContext
	{
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) //คอนสตั้กเตอร์ ,option จะเชื่อมต่อกับฐานข้อมูลแบบใด ;พารามิเตอร์ชื่อว่า options ; base คือโยนไปทำงานในคลาสแม่
        {

        }

        public DbSet<Market> Market { get; set; } //ระบุชื่อ entity หรือก็คือชื่อ model ;เอา model student นำมาสร้างเป็นตารางเก็บในฐานข้อมูลที่มีชื่อว่า students; ต้องอ้างอิงผ่าน DbSet
        public DbSet<MarketGenre> MarketGenre { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
    }
}

