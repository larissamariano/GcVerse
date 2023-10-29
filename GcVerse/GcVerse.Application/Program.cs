using GcVerse.Infrastructure.Mapping;
using GcVerse.Infrastructure.Repositories;
using GcVerse.Infrastructure.Repositories.Category;
using GcVerse.Infrastructure.Repositories.Category.Implementation;
using GcVerse.Infrastructure.Repositories.Content;
using GcVerse.Infrastructure.Repositories.Content.Implementation;
using GcVerse.Infrastructure.Services.Category;
using GcVerse.Infrastructure.Services.Category.Implementation;
using GcVerse.Infrastructure.Services.Content;
using GcVerse.Infrastructure.Services.Content.Implementation;
using GcVerse.Models.Category;
using GcVerse.Models.Content;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            .Enrich.
            FromLogContext().
            WriteTo.Console().
            CreateBootstrapLogger());

System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
Log.Information("Starting up!");

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBaseRepository<BaseCategory>, CategoryRepository>();

builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
builder.Services.AddScoped<IBaseRepository<SubCategory>, SubCategoryRepository>();

builder.Services.AddScoped<IBaseContentRepository, BaseContentRepository>();

builder.Services.AddScoped<IListContentService, ListContentService>();
builder.Services.AddScoped<IBaseRepository<ListContent>, ListContentRepository>();

builder.Services.AddScoped<INewsContentService, NewsContentService>();
builder.Services.AddScoped<IBaseRepository<NewsContent>, NewsContentRepository>();

builder.Services.AddScoped<IQuizzContentService, QuizzContentService>();
builder.Services.AddScoped<IBaseRepository<QuizzContent>, QuizzContentRepository>();

MappingData.Mapping();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();