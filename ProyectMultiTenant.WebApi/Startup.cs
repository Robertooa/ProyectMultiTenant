using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ProyectMultiTenant.Application.Contracts;
using ProyectMultiTenant.Application.Services;
using ProyectMultiTenant.Domain.Contracts;
using ProyectMultiTenant.Domain.IRepository;
using ProyectMultiTenant.Domain.Models;
using ProyectMultiTenant.Domain.Security;
using ProyectMultiTenant.Repository.MSSql;
using ProyectMultiTenant.Repository.MSSql.Repository;
using ProyectMultiTenant.Security;
using ProyectMultiTenant.WebApi.Extensions;
using ProyectMultiTenant.WebApi.Middleware;

namespace ProyectMultiTenant.WebApi
{
    public class Startup
    {
        private readonly string MyCors = "AllowCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
            });
            services.AddControllers();
            services.AddMultitenancy();
            services.AddCors(option =>
            {
                option.AddPolicy(name: MyCors, builder =>
                {
                    builder.WithOrigins(Configuration["AllowedHosts"])
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            var tokenOptionSection = Configuration.GetSection("TokenOption");
            var tokenOption = tokenOptionSection.Get<TokenOption>();
            var key = Encoding.ASCII.GetBytes(tokenOption.Secret);
            services.AddAuthentication(setup =>
            {
                setup.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                setup.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }); ;

            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();

            services.AddTransient<ICryptography, Cryptography>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthenticationUserService, AuthenticationUserService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ITokenService, TokenService>();

            services.AddScoped<IBaseRepository<Organization>, BaseRepository<Organization>>();
            services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
            services.AddScoped<IBaseRepository<Token>, BaseRepository<Token>>();
            services.AddScoped<IBaseRepositoryTenant2<Product>, BaseRepositoryTenant2<Product>>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IGeneratorTenantRepository, GeneratorTenantRepository>();

            services.Configure<TokenOption>(option =>
            {
                Configuration.GetSection("TokenOption")
                .Bind(option);
            });

            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(
                Configuration.GetConnectionString("ConnectionStringSqlServerTenant1")));

            services.AddDbContext<MultiTenantDbContext>(option => option.UseSqlServer(
                Configuration.GetConnectionString("ConnectionStringSqlServerTenant2")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            using (var scope = app.ApplicationServices.CreateScope())
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
                context.Database.Migrate();
            }

            app.UseMiddleware<TenantMiddleware>();
            app.UseRouting();
            app.UseSession();
            app.UseCors(MyCors);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    ); ;
            });
        }
    }
}
