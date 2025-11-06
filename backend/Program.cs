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
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Midterm_EquipmentRental_Team2.Models;

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
builder.Services.AddScoped<JWTService>();


builder.Services.AddAuthentication(options => 
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/auth/login";
    options.AccessDeniedPath = "/auth/denied";
    options.LogoutPath = "/auth/logout";
})
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? "";
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? "";
    options.CallbackPath = "/signin-oidc"; // required
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("email");

    // Map claims & assign role on successful login
    options.Events.OnCreatingTicket = ticket =>
    {
        var email = ticket.Principal.FindFirstValue(ClaimTypes.Email);
        if (!string.IsNullOrEmpty(email))
        {
            using var scope = builder.Services.BuildServiceProvider().CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var user = db.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                // create user
                user = new User
                {
                    Email = email,
                    Role = "User",
                    ExternalProvider = "Google",
                    ExternalId = ticket.Principal.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                db.Users.Add(user);
                db.SaveChanges();
            }

            // Add Role claim for [Authorize(Roles="Admin")]
            var claimsIdentity = ticket.Principal.Identity as ClaimsIdentity;
            claimsIdentity?.AddClaim(new Claim(ClaimTypes.Role, user.Role));
        }
        return Task.CompletedTask;
    };
});


//var jwtSection = builder.Configuration.GetSection("jwt");
//var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["key"]));

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = jwtSection["Issuer"],
//        ValidAudience = jwtSection["Audience"],
//        IssuerSigningKey = signingKey,

//        NameClaimType = ClaimTypes.Email
//    };
//})
//.AddCookie(options =>
//{
//    options.LoginPath = "/auth/login";
//    options.AccessDeniedPath = "/auth/denied";
//    options.LogoutPath = "/auth/logout";
//})
//.AddGoogle(options =>
//{
//    options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? "";
//    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? "";
//    options.Scope.Add("profile");
//    options.Scope.Add("email");
//});


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
