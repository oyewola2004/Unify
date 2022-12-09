using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unify.UNIFY.Implementation.Repository;
using Unify.UNIFY.Implementation.Services;
using UNIFY.Implementation.Services;
using Unify.UNIFY.Interfaces.Repository;
using Unify.UNIFY.Interfaces.Services;
using UNIFY.Authentication;
using UNIFY.Context;
using UNIFY.Email;
using UNIFY.Implementation.Repository;
using UNIFY.Implementation.Services;
using UNIFY.Interfaces.Repository;
using UNIFY.Interfaces.Services;
using Interfaces.Repository;
using Interfaces.Services;
using Implementation.Services;
using Implementation;

namespace UNIFY
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
            services.AddDbContext<ApplicationContext>(options => options.UseMySQL("server=localhost;user=root;database=UNIFY;port=3306;password=oyeyemialabi22311"));


            services.AddCors(cor => cor.AddPolicy("UNIFY", builder=>
            {
               builder.AllowAnyHeader();
               builder.AllowAnyMethod();
               builder.AllowAnyOrigin();
            }));

             services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService, MessageService>();
             services.AddScoped<IMarketPlaceRepository, MarketPlaceRepository>();
            services.AddScoped<IMarketPlaceService, MarketPlaceService>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<ICommunityService, CommunityService>();
            services.AddScoped<ICommunityRepository, CommunityRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ISecurityRepository, SecurityRepository>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<ISecurityAgencyRepository, SecurityAgencyRepository>();
            services.AddScoped<ISecurityAgencyService, SecurityAgencyService>();



            var key = "This is an authorization key";
            services.AddSingleton<IJWTAuthentication>(new JWTAuthentication(key));




            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
               };

           });

           services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = int.MaxValue; // if don't set default value is: 30 MB
            });
            services.Configure<FormOptions>(x =>
           {
             x.ValueLengthLimit = int.MaxValue;
                 x.MultipartBodyLengthLimit = int.MaxValue; // if don't set default value is: 128 MB
               x.MultipartHeadersLengthLimit = int.MaxValue;
            });    



            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UNIFY", Version = "v1" });
            });

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UNIFY v1"));
            }
           
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseCors("UNIFY");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
