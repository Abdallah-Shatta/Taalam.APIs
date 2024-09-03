using E_Learning.BL.Managers.AuthenticationManager;
using E_Learning.BL.Managers.CategoryManager;
using Microsoft.Extensions.DependencyInjection;

namespace E_Learning.BL.ServicesExtention
{
    public static class BLServicesExtention
    {
        public static void AddBLServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddTransient<IJwtManager, JwtManager>();
        }
    }
}
