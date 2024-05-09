using Microsoft.AspNetCore.Identity;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using OpenApiService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var httpClientHandler = new HttpClientHandler();
httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

var httpClient = new HttpClient(httpClientHandler);

builder.Services.AddSingleton<UserManagementClient>(services =>
    new UserManagementClient(builder.Configuration.GetValue<string>("USERMANAGEMENT_URL"), httpClient));

//builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);

//builder.Services.AddSession();
//builder.Services.AddHttpContextAccessor();

//var app = builder.Build();

//app.UseAuthentication();
//app.UseAuthorization();

// Add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer("Bearer", options =>
{
    options.Authority = builder.Configuration.GetValue<string>("USERMANAGEMENT_URL");
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateAudience = false
    };
    options.RequireHttpsMetadata = false;
});

// Add authorization
builder.Services.AddAuthorization();

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
//app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();
//app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await GenerateCSharpClient("./OpenApi/UserManagement.json", "./OpenApiServices/UserManagementClient.cs", "UserManagementClient");

app.Run();

async static Task GenerateCSharpClient(string filePath, string generatePath, string className) =>
    await GenerateClient(
        document: await OpenApiDocument.FromFileAsync(filePath),
        generatePath: generatePath,
        generateCode: (OpenApiDocument document) =>
        {
            var settings = new CSharpClientGeneratorSettings
            {
                UseBaseUrl = true,
                ClassName = className,
                CSharpGeneratorSettings =
                {
                    Namespace = "OpenApiService",
                },
                GenerateClientInterfaces = true,
            };

            var generator = new CSharpClientGenerator(document, settings);
            var code = generator.GenerateFile();
            return code;
        }
    );

async static Task GenerateClient(OpenApiDocument document, string generatePath, Func<OpenApiDocument, string> generateCode)
{
    Console.WriteLine($"Generating {generatePath}...");

    var code = generateCode(document);

    await System.IO.File.WriteAllTextAsync(generatePath, code);
}