using NSwag;
using NSwag.CodeGeneration.CSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
                UseBaseUrl = false,
                ClassName = className,
                CSharpGeneratorSettings =
                {
                    Namespace = "OpenApiService",
                }
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