using Microsoft.EntityFrameworkCore;
using ServicesCentral.Data;
using ServicesCentral.Repositories.Abstract;
using ServicesCentral.Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IMarketService, MarketService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

//set เชื่อมกับฐานข้อมูล
builder.Services.AddDbContext<ApplicationDBContext>( //จะบอกฐานข้อมูลต้องสร้างอะไรบ้าง ก็ส่งไฟล์ ApplicationDBContext จาก Data ไป
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaulConnection")) //ConnectionString อยู่ใน appsetings.json
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

 app.Run();

