using API.Data;
using API.Data.Repositories;
using API.Helpers;
using API.Interfaces;
using API.Interfaces.Repositories;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions 
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMessageRepository, MessageRepository>();

            services.AddScoped<ITokenService, TokenService>();

            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<IPhotoService, PhotoService>();
            

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            
            //  services.AddDbContext<DataContext>(options =>
            //    options.UseSqlite("Data Source=Database.db"));

            services.AddDbContext<DataContext>(options =>
            {
                string connStr = config.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connStr);
            });

            return services;
        }
    }
}