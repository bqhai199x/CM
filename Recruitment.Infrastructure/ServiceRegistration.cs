using Microsoft.Extensions.DependencyInjection;
using Recruitment.Infrastructure.Interfaces;
using Recruitment.Infrastructure.Repositories;

namespace Recruitment.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ICandidateRepository, CandidateRepository>();
            services.AddTransient<ILevelRepository, LevelRepository>();
            services.AddTransient<IPositionRepository, PositionRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
