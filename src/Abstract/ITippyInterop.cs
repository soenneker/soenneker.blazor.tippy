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
    ValueTask Initialize(string elementId, TippyConfiguration tippyConfiguration, CancellationToken cancellationToken = default);

    ValueTask Hide(string elementId, CancellationToken cancellationToken = default);

    ValueTask Show(string elementId, CancellationToken cancellationToken = default);

    ValueTask Destroy(string elementId, CancellationToken cancellationToken = default);
}
