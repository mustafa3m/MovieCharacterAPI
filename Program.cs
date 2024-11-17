using MovieCharacterAPI.Data;
using Microsoft.EntityFrameworkCore;
using MovieCharacterAPI.Services;


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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

