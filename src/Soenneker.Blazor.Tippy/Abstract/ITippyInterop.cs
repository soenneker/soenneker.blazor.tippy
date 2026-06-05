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
    /// Executes the initialize operation.
    /// </summary>
    /// <param name="elementId">The element id.</param>
    /// <param name="tippyConfiguration">The tippy configuration.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Initialize(string elementId, TippyConfiguration tippyConfiguration, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the hide operation.
    /// </summary>
    /// <param name="elementId">The element id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Hide(string elementId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the show operation.
    /// </summary>
    /// <param name="elementId">The element id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Show(string elementId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the destroy operation.
    /// </summary>
    /// <param name="elementId">The element id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Destroy(string elementId, CancellationToken cancellationToken = default);
}