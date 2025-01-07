using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application.Modules;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddHandlers();
        
        return services;
    }
    
    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<InsertProjectCommand>());
        services.AddTransient<IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>, InsertProjectCommandBehavior>();
        
        return services;
    }
}