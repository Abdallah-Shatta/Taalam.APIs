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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Learning.DAL.ServicesExtension
{
    public static class DALServicesExtensions
    {
        public static void AddDALServices(this IServiceCollection services, IConfiguration configuration)
        {
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
            /*------------------------------------------------------------------------*/
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            /*------------------------------------------------------------------------*/

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<User, Role, AppDbContext, int>>()
            .AddRoleStore<RoleStore<Role, AppDbContext, int>>();

        }
    }
}
