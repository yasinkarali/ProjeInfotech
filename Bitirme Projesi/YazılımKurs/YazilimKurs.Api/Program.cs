using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using YazilimKurs.Data;
using YazilimKurs.Data.Abstract;
using YazilimKurs.Data.Concrete.EfCore.Repositories;
using YazilimKurs.Service.Abstract;
using YazilimKurs.Service.Concrete;
using YazilimKurs.Shared.Helpers.Abstract;
using YazilimKurs.Shared.Helpers.Concrete;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<YazilimKursDbContext>(options=>options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

builder.Services.AddScoped<ICourseRepository, EfCoreCourseRepository>();
builder.Services.AddScoped<IStudentRepository, EfCoreStudentRepository>();
builder.Services.AddScoped<ITeacherRepository, EfCoreTeacherRepository>();
builder.Services.AddScoped<ICartRepository, EfCoreCartRepository>();
builder.Services.AddScoped<ICartItemRepository, EfCoreCartItemsRepository>();
builder.Services.AddScoped<IOrderRepository, EfCoreOrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, EfCoreOrderItemRepository>();
builder.Services.AddScoped<ICourseStudentRepository, EfCoreCourseStudentRepository>();



builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<ICardItemService, CardItemService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddScoped<IImageHelper, ImageHelper>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
