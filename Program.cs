using MovieCharacterAPI.Data;
using Microsoft.EntityFrameworkCore;
using MovieCharacterAPI.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Dependency Injection for DbContext
builder.Services.AddDbContext<MovieCharacterDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SecretConnection")));

// Add AutoMapper for mapping models
builder.Services.AddAutoMapper(typeof(MovieProfile).Assembly,
                               typeof(CharacterProfile).Assembly,
                               typeof(FranchiseProfile).Assembly);

// Configure Swagger for API documentation
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MovieCharacter API",
        Description = "An ASP.NET Core Web API for managing movies, characters, and franchises",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Mustafa Muse",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "3M",
            Url = new Uri("https://example.com/license")
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Dependency Injection for Services
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IFranchiseService, FranchiseService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Enable CORS to allow any origin, header, and method
app.UseCors(policyBuilder => policyBuilder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
);

app.MapControllers();

app.Run();

