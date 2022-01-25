using GoldShop.Infrastructure;
using GoldShop.Infrastructure.IShopServices;
using GoldShop.Infrastructure.ShopServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldShop
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
            services.AddControllers();
            services.AddDbContext<GoldDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ShopConnection")));
            // configure DI for application services
            services.AddScoped<IUsersServices, UsersServices>();
            // Add JWT Token settings
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 var serverSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:key"]));
                 options.TokenValidationParameters = new
                     TokenValidationParameters
                 {
                     IssuerSigningKey = serverSecret,
                     ValidIssuer = Configuration["JWT:Issuer"],
                     ValidAudience = Configuration["JWT:Audience"]
                 };
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
