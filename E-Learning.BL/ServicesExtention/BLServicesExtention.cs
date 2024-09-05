using E_Learning.BL.Managers.AccountManager;
using E_Learning.BL.Managers.AuthenticationManager;
using E_Learning.BL.Managers.CategoryManager;

using E_Learning.BL.Managers.Mailmanager;

using E_Learning.BL.Managers.CourseManager;

using Microsoft.Extensions.DependencyInjection;

namespace E_Learning.BL.ServicesExtention
{
    public static class BLServicesExtention
    {
        public static void AddBLServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<ICourseManager, CourseManager>();

            services.AddScoped<IUserManager, UserManager>();
            services.AddTransient<IJwtManager, JwtManager>();
            services.AddTransient<IAccountManager,AccountManager>();
            services.AddTransient<IMailManager,MailManager>();
        }
    }
}
