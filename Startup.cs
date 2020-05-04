 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_CounsellingWebApplication.Hubs;
using E_CounsellingWebApplication.Models;
using E_CounsellingWebApplication.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace E_CounsellingWebApplication
{
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContextPool<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("EmployeeDbConnection")));
            
            services.AddSignalR();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10; //to edit identity validations
                options.Password.RequiredUniqueChars = 3; //to edit identity validations
                options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders(); 


            //services.AddMvc(config =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //                     .RequireAuthenticatedUser()
            //                     .Build();
            //    config.Filters.Add(new AuthorizeFilter(policy));
            //}).AddXmlDataContractSerializerFormatters();   //for global authorization but it has an error

            //for Administration/accessdenied (For administration controller)
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
            });

            
            

            services.AddAuthorization(options =>
            {
             options.AddPolicy("DeleteRolePolicy",
              policy => policy.RequireClaim("Delete Role", "true"));

                options.AddPolicy("EditRolePolicy", policy =>
              policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));
                options.InvokeHandlersAfterFailure = false;

                options.AddPolicy("AdminRolePolicy",
           policy => policy.RequireRole("Admin"));

                options.AddPolicy("CounselorRolePolicy",
           policy => policy.RequireRole("Admin")
                             .RequireRole("Counselor"));

            });
  
            //dependency Injection
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<IBroadcastRepository, SqlBroadcastRepository>();
            services.AddSingleton<IAuthorizationHandler,CanEditOnlyOtherAdminRolesAndClaimsHandler>();
            services.AddScoped<ICategoryRepository, MockCategoryRepository>();
            services.AddScoped<IUserCounselorRepository, SqlUserCounselorRepository>();
            services.AddScoped<IAppointmentRepository, SqlAppointmentRepository>();
            services.AddScoped<IBookRepository, SqlBookRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                               UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
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

            app.UseAuthentication();
            DataInitializer.SeedData(userManager, roleManager); 

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Welcome}/{id?}");
            });
        }
    }
}
