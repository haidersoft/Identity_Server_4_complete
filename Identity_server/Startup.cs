using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using identity_server_4.Data;
using Microsoft.AspNetCore.Identity;
using identity_server_4.Auth;
using IdentityServer4.Validation;
using identity_server_4.OwnerPasswordValidator;

namespace identity_server_4
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment Environment;

        
        public Startup(IWebHostEnvironment environment,IConfiguration configuration) {
            Environment = environment;
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
              options.EnableEndpointRouting = false);
            services.AddControllersWithViews();

            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;


            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(this.configuration.GetConnectionString("Default")));



            //services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>()
            //     .AddTransient<IAuthRepository, AuthRepository>();

            services.AddIdentity<IdentityUser, IdentityRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddResourceOwnerValidator<UserValidator>()
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddTestUsers(Config.Users)


                //.AddAspNetIdentity<IdentityUser>()
                //.AddConfigurationStore(options =>
                //{
                //    options.ConfigureDbContext = builder => builder.UseSqlServer(this.configuration.GetConnectionString("Default"), options => options.MigrationsAssembly(migrationAssembly));
                //})
                //.AddOperationalStore(options =>
                //{
                //    options.ConfigureDbContext = builder => builder.UseSqlServer(this.configuration.GetConnectionString("Default"), options => options.MigrationsAssembly(migrationAssembly));
                //})
                //.AddResourceOwnerValidator<UserValidator>()
                .AddDeveloperSigningCredential();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseMvcWithDefaultRoute();

            //app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
