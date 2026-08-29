using System.Threading.Tasks;
using System.Threading;
using System;
using Soenneker.Blazor.Tippy.Configuration;

namespace Soenneker.Blazor.Tippy.Abstract;

/// <summary>
/// A Blazor interop library for Tippy.js
/// </summary>
public interface ITippyInterop : IAsyncDisposable
{
    /// <summary>
    /// Initializes the tippy so it is ready for use.
    /// </summary>
    /// <param name="elementId">ID of the DOM element to target.</param>
    /// <param name="tippyConfiguration">tippy Configuration that supplies runtime settings.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the tippy is ready for use.</returns>
    ValueTask Initialize(string elementId, TippyConfiguration tippyConfiguration, CancellationToken cancellationToken = default);

    /// <summary>
    /// Hides tippy for the tippy.
    /// </summary>
    /// <param name="elementId">ID of the DOM element to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the hide operation is complete.</returns>
    ValueTask Hide(string elementId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Shows tippy for the tippy.
    /// </summary>
    /// <param name="elementId">ID of the DOM element to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the show operation is complete.</returns>
    ValueTask Show(string elementId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Releases the resources held by the tippy.
    /// </summary>
    /// <param name="elementId">ID of the DOM element to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the destroy operation is complete.</returns>
    ValueTask Destroy(string elementId, CancellationToken cancellationToken = default);
}
