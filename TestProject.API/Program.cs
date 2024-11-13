using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using TestProject.Core;
using TestProject.Infrustrucure;
using TestProject.Infrustrucure.Data;
using TestProject.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"),
         sqlOptions => sqlOptions.EnableRetryOnFailure());
});

builder.Services.AddInfrustuctureDependencies()
                .AddServiceDependency()
                .AddCoreDependency();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("https://localhost:7017")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

/*builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOriginal",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});
*/

// »‰«¡ «· ÿ»Ìﬁ
var app = builder.Build();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")),
    RequestPath = "/uploads"
});

app.UseCors("AllowSpecificOrigin");


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
