using E_Learning.BL.ServicesExtention;
using E_Learning.DAL.Data.Context;
using E_Learning.DAL.ServicesExtension;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_Learning.APIs.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using E_Learning.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Learning.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(options =>
            {
                //Authorization policy
                //var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                //options.Filters.Add(new AuthorizeFilter(policy));

            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            /*------------------------------------------------------------------------*/
            builder.Services.AddBLServices();
            builder.Services.AddDALServices(builder.Configuration);


            //CORS: localhost:4200, localhost:4100
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policyBuilder =>
                {
                    policyBuilder
                    .WithOrigins("http://localhost:4200")
                    .WithHeaders("Authorization", "origin", "accept", "content-type")
                    .WithMethods("GET", "POST", "PUT", "DELETE");
                });
            });

            /*------------------------------------------------------------------------*/

            //identity
            builder.Services.AddIdentity<User, Role>()
                            .AddEntityFrameworkStores<AppDbContext>()
                            .AddDefaultTokenProviders()
                            .AddUserStore<UserStore<User, Role, AppDbContext, int>>()
                            .AddRoleStore<RoleStore<Role, AppDbContext, int>>();

            /* builder.Services.AddIdentityApiEndpoints<User>()
            .AddEntityFrameworkStores<AppDbContext>();*/

            //de 3ashan el forget password
            /* builder.Services.Configure<IdentityOptions>(
                opts => opts.SignIn.RequireConfirmedEmail = true
                );*/

            /*------------------------------------------------------------------------*/
            builder.Services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromHours(10));


            //var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            //var key = Encoding.ASCII.GetBytes(jwtSettings.GetSection("Secret").Value);

            /*------------------------------------------------------------------------*/
            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;//bearer  ---

            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters()//this what property you want to valid
            //    {
            //        ValidateIssuer = true,
            //        ValidateLifetime = true,
            //        ValidateAudience = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = jwtSettings.GetSection("Issuer").Value,
            //        ValidAudience = jwtSettings.GetSection("Audience").Value,
            //        IssuerSigningKey = new SymmetricSecurityKey(key)
            //    };
            //});

            /*------------------------------------------------------------------------*/
            //builder.Services.AddAuthorization(options =>
            //{
            //});
            /*------------------------------------------------------------------------*/
            var app = builder.Build();
            /*------------------------------------------------------------------------*/

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // Serve static files
            app.UseStaticFiles();
            /* app.UseCors("MyPolicy");*/
            app.UseCors();
            
            // app.MapIdentityApi<User>();

            //app.UseAuthentication();

            //app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}
