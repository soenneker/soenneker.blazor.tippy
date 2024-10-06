using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Soenneker.Blazor.Tippy.Abstract;
using Soenneker.Blazor.Tippy.Options;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.ValueTask;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Blazor.Tippy;

///<inheritdoc cref="ITippyInterop"/>
public class TippyInterop : ITippyInterop
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IResourceLoader _resourceLoader;

    private readonly AsyncSingleton<object> _scriptInitializer;

    public TippyInterop(IJSRuntime jSRuntime, IResourceLoader resourceLoader)
    {
        _jsRuntime = jSRuntime;
        _resourceLoader = resourceLoader;

        _scriptInitializer = new AsyncSingleton<object>(async (token, _) => {

            await _resourceLoader.ImportModuleAndWaitUntilAvailable("Soenneker.Blazor.Tippy/tippyinterop.js", "TippyInterop", 100, token).NoSync();
            await _resourceLoader.LoadStyle("https://cdn.jsdelivr.net/npm/tippy.js@6.3.7/dist/tippy.css", "sha256-WWn0l9kVjXaC+CGcbxP6Zyac31v1Cjkx2VMnFR3uVng=", cancellationToken: token).NoSync();
            await _resourceLoader.LoadScriptAndWaitForVariable("https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js", "Popper", "sha256-whL0tQWoY1Ku1iskqPFvmZ+CHsvmRWx/PIoEvIeWh4I=", cancellationToken: token).NoSync();
            await _resourceLoader.LoadScriptAndWaitForVariable("https://cdn.jsdelivr.net/npm/tippy-layout@4.2.2/dist/tippy.pkgd.min.js", "tippy", "sha256-qx7gQMlSzXvTJCl8PBcHyzDQLGvX7NaFbiFY44WpsW4=", cancellationToken: token).NoSync();

            return new object();
        });
    }

    public async ValueTask Init(string elementId, string templateId, TippyOptions tippyOptions, CancellationToken cancellationToken = default)
    {
        await _scriptInitializer.Get(cancellationToken).NoSync();

        await _jsRuntime.InvokeVoidAsync("TippyInterop.init", cancellationToken, elementId, templateId, tippyOptions);
    }

    public async ValueTask Hide(string elementId, CancellationToken cancellationToken = default)
    {
        await _jsRuntime.InvokeVoidAsync("TippyInterop.hide", cancellationToken, elementId);
    }

    public ValueTask DisposeAsync()
    {
        return _resourceLoader.DisposeModule("Soenneker.Blazor.Tippy/tippyinterop.js");
    }
}