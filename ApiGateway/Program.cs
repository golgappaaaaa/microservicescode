using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. KEY: Exact same string jo Product API mein hai
var secretKey = "Kar@9871_Aapki_Secret_Key_32_Chars!";
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

// 2. AUTHENTICATION: "Bearer" naam hata do yahan se
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => // <--- Yahan se "Bearer" string hata di hai
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Kar@9871_Aapki_Secret_Key_32_Chars!")),
        ValidateIssuer = true,
        ValidIssuer = "karan",
        ValidateAudience = true,
        ValidAudience = "karan",
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero // Isse 5 minute ka wait nahi karna padega
    };
});

builder.Services.AddAuthorization();
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot();

var app = builder.Build();

// Brahmaastra Middleware
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("Gateway is Running!");
        return;
    }
    await next();
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();
app.Run();