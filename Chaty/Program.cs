using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Chaty.Components;
using Chaty.Helpers.Services;
using DAL;
using DAL.Domain;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();

builder.Services.AddHttpClient<HttpService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("ServerURI").Value!);
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

builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthStateProvider>();

builder.Services.AddScoped<UOW>();

builder.Services
    .AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = false)
    ;

JwtSecurityTokenHandler.DefaultInboundClaimFilter.Clear();
builder.Services
    .AddAuthentication()
    .AddCookie(options => options.SlidingExpiration = true)
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

//-------------------------------------------------------------------
var app = builder.Build();
//-------------------------------------------------------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapControllers();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseCors("CorsAllowAll");


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();