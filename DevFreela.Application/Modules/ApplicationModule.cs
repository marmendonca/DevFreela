using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Models;
using DevFreela.Application.Services;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application.Modules;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddServices();
        services.AddHandlers();
        
        return services;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISkillService, SkillService>();
        
        return services;
    }
    
    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<InsertProjectCommand>());
        services.AddTransient<IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>, InsertProjectCommandBehavior>();
        
        return services;
    }
}