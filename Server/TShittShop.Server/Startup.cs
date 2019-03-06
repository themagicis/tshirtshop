using App.Common.Config;
using App.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using TShirtShop.DataAccess;
using TShirtShop.Server.Infrastructure;
using TShirtShop.Server.Logging;
using TShirtShop.Server.Security;
using TShirtShop.Services.Attributes;
using TShirtShop.Services.Categories;
using TShirtShop.Services.Departments;
using TShirtShop.Shared;
using Unity;

namespace TShittShop.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IUnityContainer Container { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<ConnectionStringOptions>(Configuration.GetSection(ConnectionStringOptions.Section));
            services.Configure<TokenOptions>(Configuration.GetSection(TokenOptions.Section));
            services.Configure<SendGridOptions>(Configuration.GetSection(SendGridOptions.Section));

            // Allow cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build());
                options.DefaultPolicyName = "AllowAll";
            });

            services.AddHttpContextAccessor();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   var tokenProvider = Container.Resolve<JwtTokenProvider>();
                   options.TokenValidationParameters = tokenProvider.BearerOptions.TokenValidationParameters;

               });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
                config.Filters.Add(typeof(ValidateModelStateAttribute));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddControllersAsServices();
        }

        public void ConfigureContainer(IUnityContainer container)
        {
            Container = container;

            container.RegisterSingleton<ConfigOptions<ConnectionStringOptions>>();
            container.RegisterSingleton<ConfigOptions<TokenOptions>>();
            container.RegisterSingleton<ConfigOptions<SendGridOptions>>();

            container.RegisterSingleton<ISecurityKeyProvider, RsaSecurityKeyProvider>();
            container.RegisterSingleton<JwtTokenProvider>();

            container.RegisterType<IAppContext, TShirtShopContext>(new PerRequestLifetimeManager(container.Resolve<IHttpContextAccessor>()));

            container.RegisterFactory<IAppIdentity>(s => new JwtTokenIdentity(s.Resolve<IHttpContextAccessor>().HttpContext.User.Identity as ClaimsIdentity));

            container.RegisterType<IAttributesService, AttributesService>();
            container.RegisterType<ICategoriesService, CategoriesService>();
            container.RegisterType<IDepartmentsService, DepartmentsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.ConfigureCustomExceptionMiddleware();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}