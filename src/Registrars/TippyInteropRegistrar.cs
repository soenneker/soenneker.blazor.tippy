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
    public static IServiceCollection AddTippyInteropAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoader();
        services.TryAddScoped<ITippyInterop, TippyInterop>();

        return services;
    }
}
