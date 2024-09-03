using E_Learning.DAL.Data.Context;
using E_Learning.DAL.Models;



using E_Learning.DAL.Repositories.AnswerRepository;
using E_Learning.DAL.Repositories.CartRepository;
using E_Learning.DAL.Repositories.CategoryRepository;
using E_Learning.DAL.Repositories.CourseRepository;
using E_Learning.DAL.Repositories.EnrollmentRepository;
using E_Learning.DAL.Repositories.LessonRepository;
using E_Learning.DAL.Repositories.QuestionRepository;
using E_Learning.DAL.Repositories.QuizRepository;
using E_Learning.DAL.Repositories.SectionRepository;
using E_Learning.DAL.Repositories.UserRepository;
using E_Learning.DAL.UnitOfWorkDP;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace E_Learning.DAL.ServicesExtension
{
    public static class DALServicesExtensions
    {
        public static void AddDALServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });
            /*------------------------------------------------------------------------*/
            var connectionString = configuration.GetConnectionString("Yasser");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            /*------------------------------------------------------------------------*/
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // services.AddTransient<I>();
            /*------------------------------------------------------------------------*/
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            /*------------------------------------------------------------------------*/


        


  
      


            }
            }
  
    }

