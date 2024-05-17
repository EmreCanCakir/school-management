using FluentValidation;
using Infrastructure.Converter;
using LectureManagement.DataAccess;
using LectureManagement.DataAccess.Abstracts;
using LectureManagement.DataAccess.Concretes;
using LectureManagement.Services.Abstracts;
using LectureManagement.Services.Concretes;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);
// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new TimeSpanJsonConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
ConfigureSwagger();
ConfigureRabbitMQ();
ConfigureRedis();

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

void ConfigureServices(IServiceCollection services) {
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<MainDbContext>(options =>
    {
        options.UseSqlServer(connectionString);
        //options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        options.EnableSensitiveDataLogging();
    });

    services.AddTransient<ILectureService, LectureService>();
    services.AddTransient<ILectureDal, LectureDal>();
    services.AddTransient<ILectureInstructorService, LectureInstructorService>();
    services.AddTransient<ILectureInstructorDal, LectureInstructorDal>();
    services.AddTransient<ILectureStudentService, LectureStudentService>();
    services.AddTransient<ILectureStudentDal, LectureStudentDal>();
    services.AddTransient<ILectureScheduleService, LectureScheduleService>();
    services.AddTransient<ILectureScheduleDal, LectureScheduleDal>();
    services.AddTransient<IAcademicYearService, AcademicYearService>();
    services.AddTransient<IAcademicYearDal, AcademicYearDal>();

    services.AddScoped<ClassroomDetailResponseService>();

    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    services.AddAutoMapper(Assembly.GetExecutingAssembly());

    services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
}

void ConfigureSwagger()
{
    builder.Services.AddSwaggerGen(config =>
    {
        config.MapType<DateOnly>(() => new OpenApiSchema
        {
            Type = "string",
            Format = "date",
            Example = new OpenApiString(DateTime.Today.ToString("yyyy-MM-dd"))
        });
        config.MapType<TimeSpan>(() => new OpenApiSchema
        {
            Type = "string",
            Format = "time",
            Example = new OpenApiString(DateTime.Today.ToString("HH:mm:ss"))
        });
    }
);

    builder.Services.AddAuthentication();
}

void ConfigureRabbitMQ()
{
    builder.Services.AddMassTransit(busConfigurator =>
    {
        busConfigurator.SetKebabCaseEndpointNameFormatter();
        //busConfigurator.AddConsumer<AuthTokenGeneratedConsumer>();

        busConfigurator.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), h =>
            {
                h.Username(builder.Configuration["MessageBroker:Username"]);
                h.Password(builder.Configuration["MessageBroker:Password"]);
            });

            cfg.ConfigureEndpoints(context);
        });
    });
}

void ConfigureRedis()
{
    string connectionString = builder.Configuration.GetConnectionString("Redis");
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = connectionString;
    });
}