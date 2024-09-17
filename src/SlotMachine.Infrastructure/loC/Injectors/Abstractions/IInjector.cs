namespace SlotMachine.Infrastructure.loC.Injectors.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

public interface IInjector
{
   abstract static void Inject ( IServiceCollection serviceCollection, IWebHostEnvironment webHostEnvironment );

   abstract static void RemoveInjection ( IServiceCollection serviceCollection );
}