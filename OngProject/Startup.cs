
using Amazon.S3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OngProject.Core.Business;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.DataAccess;
using OngProject.Middleware;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;

using System.Text;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;

namespace OngProject
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OngProject", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Token Bearer",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            services.AddDbContext<OngContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("Challenge")));

            services.AddAWSService<IAmazonS3>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            });

            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<ICategoryBussines, CategoryBussines>();
            services.AddScoped<INewsBusiness, NewsBusiness>();
            services.AddTransient<IEmailBusiness, EmailBusiness>();
            services.AddScoped<IEncryptHelper, EncryptHelper>();
            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddScoped<EntityMapper>();
            services.AddScoped<IContact, ContactBusiness>();
            services.AddScoped<IMembers, MemberBusiness>();
            services.AddScoped<ImagesBusiness>();
          
            services.AddScoped<OrganizationBusiness>();
            services.AddTransient<ICommentBusiness, CommentBusiness>();
            services.AddScoped<ISlideBusiness, SlideBusiness>();
            services.AddScoped<IActivityBusiness, ActivityBusiness>();
            services.AddScoped<ITestimonialsBussines, TestimonialsBusiness>();
           
            services.AddControllers();
            services.AddDbContext<OngContext>();
            services.Configure<JwtConfig>(Configuration.GetSection("JWT"));
            var key = Encoding.ASCII.GetBytes(Configuration["JWT:Secret"]);
            services
            .AddAuthentication(x =>
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
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false
                };
            });

         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "OngProject v1"); c.RoutePrefix = "api/docs"; });
            }
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<RouteProtection>();
            app.UseOwnershipMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
