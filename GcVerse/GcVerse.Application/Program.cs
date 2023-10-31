using GcVerse.Infrastructure.Repositories;
using GcVerse.Infrastructure.Repositories.Category.Implementation;
using GcVerse.Infrastructure.Repositories.Content;
using GcVerse.Infrastructure.Repositories.Content.Implementation;
using GcVerse.Infrastructure.Repositories.User;
using GcVerse.Infrastructure.Repositories.User.Implementation;
using GcVerse.Infrastructure.Services.Category;
using GcVerse.Infrastructure.Services.Category.Implementation;
using GcVerse.Infrastructure.Services.Content;
using GcVerse.Infrastructure.Services.Content.Implementation;
using GcVerse.Infrastructure.Services.Security;
using GcVerse.Infrastructure.Services.Security.Implementation;
using GcVerse.Infrastructure.Services.User;
using GcVerse.Infrastructure.Services.User.Implementation;
using GcVerse.Models.Category;
using GcVerse.Models.Content;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;

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

builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("AuthSecret"));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddCors();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(o => o.AllowAnyOrigin());
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();