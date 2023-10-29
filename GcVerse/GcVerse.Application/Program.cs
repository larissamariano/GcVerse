using Dapper;
using GcVerse.Infrastructure.Mapping;
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
using GcVerse.Models.Shared;
using Serilog;
using System.ComponentModel;
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
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();

builder.Services.AddScoped<IBaseContentRepository, BaseContentRepository>();

builder.Services.AddScoped<IListContentService, ListContentService>();
builder.Services.AddScoped<IListContentRepository, ListContentRepository>();

builder.Services.AddScoped<INewsContentService, NewsContentService>();
builder.Services.AddScoped<INewsContentRepository, NewsContentRepository>();

var map = new CustomPropertyTypeMap(typeof(BaseCategory), (type, columnName)
   => type.GetProperties().FirstOrDefault(prop => MappingData.GetDescriptionFromAttribute(prop) == columnName.ToLower()));
Dapper.SqlMapper.SetTypeMap(typeof(BaseCategory), map);

map = new CustomPropertyTypeMap(typeof(SubCategory), (type, columnName)
  => type.GetProperties().FirstOrDefault(prop => MappingData.GetDescriptionFromAttribute(prop) == columnName.ToLower()));
Dapper.SqlMapper.SetTypeMap(typeof(SubCategory), map);

map = new CustomPropertyTypeMap(typeof(BaseImage),
(type, columnName) => type.GetProperties().
                           FirstOrDefault(prop => MappingData.GetDescriptionFromAttribute(prop) == columnName.ToLower()));
Dapper.SqlMapper.SetTypeMap(typeof(BaseImage), map);

map = new CustomPropertyTypeMap(typeof(BaseContent),
(type, columnName) => type.GetProperties().
                           FirstOrDefault(prop => MappingData.GetDescriptionFromAttribute(prop) == columnName.ToLower()));
Dapper.SqlMapper.SetTypeMap(typeof(BaseContent), map);

map = new CustomPropertyTypeMap(typeof(ListTopic),
(type, columnName) => type.GetProperties().
                           FirstOrDefault(prop => MappingData.GetDescriptionFromAttribute(prop) == columnName.ToLower()));
Dapper.SqlMapper.SetTypeMap(typeof(ListTopic), map);

map = new CustomPropertyTypeMap(typeof(NewsContent),
(type, columnName) => type.GetProperties().
                           FirstOrDefault(prop => MappingData.GetDescriptionFromAttribute(prop) == columnName.ToLower()));
Dapper.SqlMapper.SetTypeMap(typeof(NewsContent), map);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

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
