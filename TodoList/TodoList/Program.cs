using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Security.Authentication;
using TodoList.Code;
using TodoList.Components;
using TodoList.Components.Account;
using TodoList.Data;
using TodoList.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<UserWithRole>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<RoleHandler>();
builder.Services.AddScoped<CPRServices>();
builder.Services.AddScoped<ToDoServices>();
builder.Services.AddScoped<HashContent>();
builder.Services.AddScoped<ToDoServices>();


builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var TodoConnectionString = builder.Configuration.GetConnectionString("ToDoConnectionString") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddAuthorization(options => {

    options.AddPolicy("AuthUser", policy => {
        policy.RequireAuthenticatedUser();
    });
    
    options.AddPolicy("AuthAdmin", policy => {
        policy.RequireRole("Admin");
    });

});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
});

string userfolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
userfolder = Path.Combine(userfolder, ".aspnet", "https", "CertifikateNavn.pfx");
builder.Configuration.GetSection("Kestrel:Endpoints:Https:Certificate:Path").Value = userfolder;

string certPW = builder.Configuration.GetValue<string>("kestrelCertificatePassword");
builder.Configuration.GetSection("Kestrel:Endpoints:Https:Certificate:Password").Value = certPW;

//builder.WebHost.UseKestrel((context, serveroptions) =>
//{
//    serveroptions.Configure(context.Configuration.GetSection("Kestrel"))
//    .Endpoint("HTTPS", listenOptions =>
//    {
//        listenOptions.HttpsOptions.SslProtocols = SslProtocols.Tls13;
//    });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
