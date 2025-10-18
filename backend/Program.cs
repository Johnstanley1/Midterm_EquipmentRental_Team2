using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Midterm_EquipmentRental_Team2.Data;
using Microsoft.OpenApi.Models;
using System.Text;
using Midterm_EquipmentRental_Team2.Repositories;
using Midterm_EquipmentRental_Team2.Services;
using Midterm_EquipmentRental_Team2.UnitOfWork;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// to prevent circular reference issues during JSON serialization
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


// Configure Entity Framework with In-Memory Database for simplicity
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("EquipmentRentalDb"));
builder.Services.AddScoped<IEquipementRepository, EquipementRepository>();
builder.Services.AddScoped<IEquipementService, EquipementService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IRentalService, RentalService>();

// Configure Authentication and Authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key-goes-here0123456789")),
        };
    });



// add jwt authentication to swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    {
        Title = $".NET API v1",
        Version = "v1",
        Description = "Equipement API v1"
    });


    // This creates the "Authorize button in swagger interface
    // which allows you to enter a JWT token to authorize requests
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });


    // This adds a lock icons to protected endpoints in swagger
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{ }
        }
    });

});


/// This enables CORS (cross-origin resource sharing)
/// Angular runs on a different port so its origin needs to be allowed in .NET
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            //.AllowCredentials()
            .AllowAnyMethod();
        }
    );
});


builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// initialize the database to ensure its created
using (var scope = app.Services.CreateScope()) 
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

//app.UseHttpsRedirection();

// Enable Authentication and Authorization middleware
app.UseAuthentication();

app.UseCors("AllowAngular"); 

app.UseAuthorization();

app.MapControllers();

app.Run();
