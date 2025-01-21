using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using MVCProject.Controllers;
using MVCProject.Data;
using MVCProject.Data.UnitOfWork;
using MVCProject.Models;
using System.Data;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using MVCProject.Controllers.API.Base;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity.UI.Services;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;

var builder = WebApplication.CreateBuilder(args);

Configuration.Default.ApiKey.Add("api-key", builder.Configuration["BrevoApi:MailApikey"]);

// Add services to the container.
builder.Services.AddControllersWithViews();
//NewtonJSonSoft registreren
builder.Services.AddControllersWithViews().AddNewtonsoftJson(Options => Options
    .SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
//builder.Services.AddControllersWithViews(options =>
//{
//    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
//}
//    );
builder.Services.AddDbContext<AzureDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDBConnection")));
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDefaultIdentity<Gebruiker>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AzureDbContext>();



//API
//https://muratsuzen.medium.com/using-api-key-authorization-with-middleware-and-attribute-on-asp-net-core-web-api-543a4955a0ef
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("X-API-KEY", new OpenApiSecurityScheme
    {
        Name = "X-API-KEY",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme",
        In = ParameterLocation.Header,
        Description = "ApiKey must appear in header"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "X-API-KEY"
                },
                In = ParameterLocation.Header
            },
            new string[]{}
        }
    });
});

var supportedCultures = new[] { new CultureInfo("nl-BE") };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("nl-BE");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

//Build
var app = builder.Build();

//API
// Swagger middleware koppelen aan app
// Configure the HTTP request pipeline.
// zorgt ervoor dat swagger ui en testomgeving enkel in development beschikbaar is
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // SwaggerUI instellen met juiste JSON endpoint
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "MVCProjectAPI");
    });
}
//vereist apiKey voor alle pagina's => niet uncommenten
//app.UseMiddleware<APIKeyValidation>();

//Rollen aanmaken en toewijzen
using (var scope = app.Services.CreateScope())
{
    //definieer alle services die ik ga gebruiken in deze scope
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var usermanager = scope.ServiceProvider.GetRequiredService<UserManager<Gebruiker>>();
    var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

    //Definieer alle rollen
    var roleList = new List<string>()
    {
        "Beheerder",
        "Directie",
        "Coördinator",
        "Secretariaat",
        "Leerkracht",
    };

    //Maakt alle rollen aan bij opstart van applicatie als ze nog niet bestaan.
    foreach (string role in roleList)
    {
        if (!(await roleManager.RoleExistsAsync(role)))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    };


    var user = new Gebruiker();

    //De eerste keer is er nog geen beheerder, dan gaan we hier de rollen verdelen
    var usersInRole = await usermanager.GetUsersInRoleAsync("Beheerder");

    if (usersInRole.Count() == 0 && (await uow.GebruikerRepository.GetAllAsync()).ToList().Count > 0)
    {
        user = await uow.GetUserByEmail("ArneVanLooyen@School.com");
        if (user != null)
            await usermanager.AddToRoleAsync(user, "Beheerder");

        user = await uow.GetUserByEmail("InneTijlen@School.com");
        if (user != null)
        {
            await usermanager.AddToRoleAsync(user, "Directie");
        }

        user = await uow.GetUserByEmail("FikBakkers@School.com");
        if (user != null)
        {
            await usermanager.AddToRoleAsync(user, "Coördinator");
        }

        user = await uow.GetUserByEmail("KathelijnAchterHuze@School.com");
        if (user != null)
        {
            await usermanager.AddToRoleAsync(user, "Secretariaat");
        }

        var users = await uow.GebruikerRepository.GetAllAsync();
        foreach (Gebruiker usr in users)
        {
            if ((await usermanager.GetRolesAsync(usr)).ToList().Count == 0)
            {
                await usermanager.AddToRoleAsync(usr, "Leerkracht");
            }
        }

    }


}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Plaats UseAuthentication v��r UseAuthorization
app.UseAuthentication();
app.UseAuthorization();


// Middleware om niet-ingelogde gebruikers naar de loginpagina te sturen
// Kijkt of de user in ingelogd en als er niet is ingelogd: kijkt of je op de loginpagina zit. Hiermee voorkom je een infinityloop.
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/api"))
    {
    }
    else if (context.User.Identity != null && !context.User.Identity.IsAuthenticated && !context.Request.Path.StartsWithSegments("/Home/Login"))
    {
        context.Response.Redirect("/Home/Login");
        return;
    }

    await next();
});


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Dashboard}/{id?}");

    endpoints.MapRazorPages();
});

app.Run();