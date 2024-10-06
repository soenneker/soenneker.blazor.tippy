using System.Threading.Tasks;
using System.Threading;
using Soenneker.Blazor.Tippy.Options;
using System;

namespace Soenneker.Blazor.Tippy.Abstract;

/// <summary>
/// A Blazor interop library for Tippy.js
/// </summary>
public interface ITippyInterop : IAsyncDisposable
{
    ValueTask Init(string elementId, TippyOptions tippyOptions, CancellationToken cancellationToken = default);

    ValueTask Hide(string elementId, CancellationToken cancellationToken = default);
}
