using CloudinaryDotNet;
using Coudinary.Services;
using FluentValidation;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using HotelManagement.Infrastructure.Context;
using HotelManagement.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HotelManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //For Entity Framework

            builder.Services.AddDbContext<HotelDbContext>(options => options.UseSqlServer
            (builder.Configuration.GetConnectionString("ConnStr")));

            //for identity

            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<HotelDbContext>()
                .AddDefaultTokenProviders();

            //EmailService
            var emailConfig = builder.Configuration
               .GetSection("EmailConfiguration")
               .Get<EmailConfiguration>();
            builder.Services.AddSingleton(emailConfig);
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddSingleton<ICloudinaryService, CloudinaryService>();

            //Cloudinary
            var cloudName = builder.Configuration.GetValue<string>("AccountSettings:CloudName");
            var apiKey = builder.Configuration.GetValue<string>("AccountSettings:ApiKey");
            var apiSecret = builder.Configuration.GetValue<string>("AccountSettings:ApiSecret");

            if (new[] { cloudName, apiKey, apiSecret }.Any(string.IsNullOrWhiteSpace))
            {
                throw new ArgumentException("Please specify Cloudinary account details!");
            }
            builder.Services.AddSingleton(new Cloudinary(new Account(cloudName, apiKey, apiSecret)));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}