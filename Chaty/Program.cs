using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AspNetCore.Identity.MongoDbCore;
using Chaty.Components;
using Chaty.Services;
using DAL;
using DAL.Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbContext = DAL.MongoDbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();

builder.Services.AddHttpClient<HttpService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("ServerURI").Value!);
});

builder.Services.Configure<RefreshTokenSettings>(
    builder.Configuration.GetSection("RefreshToken"));

builder.Services.AddSingleton<RefreshTokenFactory>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<RefreshTokenSettings>>();
    return new RefreshTokenFactory(settings);
});

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddScoped<IMongoDbContext, MongoDbContext>();

// builder.Services.AddBlazoredSessionStorage();
builder.Services.AddAuthorization();
// builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorizationCore();
// builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthStateProvider>();

builder.Services.AddScoped<Uow>();

builder.Services
    .AddIdentity<User, Role>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 4;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
    })
    // .AddRoles<Role>()
    .AddUserStore<MongoUserStore<User>>()
    .AddRoleStore<MongoRoleStore<Role>>()
    .AddSignInManager<SignInManager<User>>()
    .AddDefaultTokenProviders();     

JwtSecurityTokenHandler.DefaultInboundClaimFilter.Clear();
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.SlidingExpiration = true;
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = builder.Configuration.GetValue<string>("JWT:issuer"),
            ValidAudience = builder.Configuration.GetValue<string>("JWT:audience"),
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration.GetValue<string>("JWT:key")!
                    )
                ),
            ClockSkew = TimeSpan.Zero,
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsAllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<Redirector>();

//-------------------------------------------------------------------
var app = builder.Build();
//-------------------------------------------------------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (ctx, next) =>
{
    var logger = ctx.RequestServices.GetRequiredService<ILogger<Program>>();
    await next.Invoke();
});

app.MapControllers();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseCors("CorsAllowAll");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();