using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Tippy.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Blazor.Tippy.Registrars;

/// <summary>
/// A Blazor interop library for Tippy.js
/// </summary>
public static class TippyInteropRegistrar
{
    /// <summary>
    /// Adds <see cref="ITippyInterop"/> as a scoped service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddTippyInteropAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped().TryAddScoped<ITippyInterop, TippyInterop>();

        return services;
    }
}
