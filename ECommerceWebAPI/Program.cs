using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using ECommerceWebAPI.Context;
using ECommerceWebAPI.Filters;
using ECommerceWebAPI.Interfaces;
using ECommerceWebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ECommerceWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<IProductService, ProductService>();

            builder.Services.AddScoped<ValidationFilterAttribute>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
                        ValidAudience = builder.Configuration["Jwt:ValidAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:IssuerSigningKey"]))
                    };
                });

            //builder.Services.AddControllers().AddNewtonsoftJson(option => { option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                //API description
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecom API", Version = "v1" });

                //Auth Definition
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            builder.Services.AddDbContext<DataContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"));
            });


            //services cors
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
            builder =>
            {
                builder.WithOrigins("*")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
            });
            });

            builder.Services.AddMvc();
            // File Attachments 
            builder.Services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            //services cors


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action}/{id?}");
            //To run Automatic migartion
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DataContext>();
                db.Database.Migrate();
            }

            app.Run();
        }
    }
}