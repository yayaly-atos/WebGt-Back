using G2T.Data;
using G2T.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;
using webapiG2T.Services.Implementations;
using webapiG2T.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<Utilisateur, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),

        // Custom validation
        LifetimeValidator = (notBefore, expires, token, param) =>
        {
            var dbContext = builder.Services.BuildServiceProvider().GetRequiredService<DataContext>();
            if (expires != null)
            {
                if (DateTime.UtcNow > expires)
                {
                    var exist = dbContext.RevoquerTokens.FirstOrDefault(t => t.Id == token.Id && t.IsRevoquer == false);
                    if (exist != null)
                    {
                        exist.IsRevoquer = true;
                        exist.DateRevoquer = expires.Value;
                        dbContext.RevoquerTokens.Update(exist);
                        dbContext.SaveChanges();
                    }
                    
                    return false;
                }
            }

            var jti = token.Id;
            var exists = dbContext.RevoquerTokens.Any(t => t.Id == jti && t.IsRevoquer == true);
            return !exists;
        }

    };
});

// builder.Services.ConfigureApplicationCookie(op => op.LoginPath = "/UserAuthentication/Login");

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IRevoquerTokenService, RevoquerTokenService>();
builder.Services.AddScoped<IIncidentService, IncidentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
