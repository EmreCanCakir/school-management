using MassTransit;
using Microsoft.AspNetCore.Identity;
using UserManagement.Models;
using UserManagement.DataAccess;
using Microsoft.EntityFrameworkCore;
using UserManagement;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.DocumentFilter<SwaggerDocumentFilter>();

    config.DocInclusionPredicate((docName, apiDesc) =>
    {
        var routeTemplate = apiDesc.RelativePath;
        if (routeTemplate == "/manage/2fa")
            return false;
        return true;
    });
}
);

ConfigureServices();

ConfigureRabbitMQ();

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

app.MapIdentityApi<User>();

app.Run();

void ConfigureServices()
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddMvc();
    builder.Services.AddDbContext<MainDbContext>(options => options.UseSqlServer(connectionString));

    builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
    builder.Services.AddAuthorization();

    builder.Services.AddIdentityCore<User>(options =>
    {
        // Password settings
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredUniqueChars = 1;

        // User settings.
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;
    }).AddEntityFrameworkStores<MainDbContext>().AddApiEndpoints();
}

void ConfigureRabbitMQ()
{
    builder.Services.AddMassTransit(busConfigurator =>
    {
        busConfigurator.SetKebabCaseEndpointNameFormatter();
        //busConfigurator.AddConsumer<AuthTokenCreateConsumer>();

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

public class SwaggerDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Paths.Remove("/manage/2fa");
    }
}