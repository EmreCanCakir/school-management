using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrganisationManagement.DataAccess;
using OrganisationManagement.DataAccess.Abstracts;
using OrganisationManagement.DataAccess.Concretes;
using OrganisationManagement.Services.Abstracts;
using OrganisationManagement.Services.Concretes;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigureServices(builder);

void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<MainDbContext>(options => options.UseSqlServer(connectionString));

    builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    builder.Services.AddTransient<IDepartmentDal, DepartmentDal>();
    builder.Services.AddTransient<IFacultyDal, FacultyDal>();
    builder.Services.AddTransient<IClassroomDal, ClassroomDal>();

    builder.Services.AddTransient<IDepartmentService, DepartmentService>();
    builder.Services.AddTransient<IFacultyService, FacultyService>();
    builder.Services.AddTransient<IClassroomService, ClassroomService>();

    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
}

var app = builder.Build();
app.Services.CreateScope().ServiceProvider.GetRequiredService<MainDbContext>().Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();