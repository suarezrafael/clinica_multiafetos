using ClinicaMultiAfetos.Context;
using ClinicaMultiAfetos.Repositories;
using ClinicaMultiAfetos.Repositories.Interfaces;
using ClinicaMultiAfetos.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace ClinicaMultiAfetos;


public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddTransient<IDocumentoClinicaRepository, DocumentoClinicaRepository>();
        services.AddTransient<IPacienteRepository, PacienteRepository>();
        services.AddTransient<IDocumentoPacienteRepository, DocumentoPacienteRepository>();
        services.AddTransient<IPlanoPacienteRepository, PlanoPacienteRepository>();
        services.AddTransient<IReciboPacienteRepository, ReciboPacienteRepository>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin",
                politica =>
                {
                    politica.RequireRole("Admin");
                });
        });

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddControllersWithViews();

        //services.AddMemoryCache();

        services.AddSession(options =>
        {
            // Set a short timeout for easy testing.
            options.IdleTimeout = TimeSpan.FromMinutes(60);
            // You might want to only set the application cookies over a secure connection:
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.HttpOnly = true;
            // Make the session cookie essential
            options.Cookie.IsEssential = true;
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserRoleInitial seedUserRoleInitial)
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        seedUserRoleInitial.SeedRoles();
        seedUserRoleInitial.SeedUsers();

        app.UseCookiePolicy();

        app.UseSession();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
            );
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
