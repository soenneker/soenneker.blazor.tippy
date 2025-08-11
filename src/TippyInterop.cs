using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Soenneker.Blazor.Tippy.Abstract;
using Soenneker.Blazor.Tippy.Configuration;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.ValueTask;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Blazor.Tippy;

///<inheritdoc cref="ITippyInterop"/>
public sealed class TippyInterop : ITippyInterop
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IResourceLoader _resourceLoader;

    private readonly AsyncSingleton _scriptInitializer;

    private const string _module = "Soenneker.Blazor.Tippy/js/tippyinterop.js";
    private const string _moduleVariable = "TippyInterop";

    public TippyInterop(IJSRuntime jSRuntime, IResourceLoader resourceLoader)
    {
        _jsRuntime = jSRuntime;
        _resourceLoader = resourceLoader;

        _scriptInitializer = new AsyncSingleton(async (token, arr) =>
        {
            var useCdn = true;

            if (arr.Length > 0)
                useCdn = (bool) arr[0];

            if (useCdn)
            {
                await _resourceLoader.LoadStyle("https://cdn.jsdelivr.net/npm/tippy.js@6.3.7/dist/tippy.css",
                                         "sha256-WWn0l9kVjXaC+CGcbxP6Zyac31v1Cjkx2VMnFR3uVng=", cancellationToken: token)
                                     .NoSync();
                await _resourceLoader.LoadScriptAndWaitForVariable("https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js", "Popper",
                                         "sha256-whL0tQWoY1Ku1iskqPFvmZ+CHsvmRWx/PIoEvIeWh4I=", cancellationToken: token)
                                     .NoSync();
                await _resourceLoader.LoadScriptAndWaitForVariable("https://cdn.jsdelivr.net/npm/tippy.js@6.3.7/dist/tippy.umd.js", "tippy",
                                         "sha256-AMOLcfKm4CGkIKi5aMXSELz3F7Q0SS0HKWOiTLtte1U=", cancellationToken: token)
                                     .NoSync();
            }
            else
            {
                await _resourceLoader.LoadStyle("_content/Soenneker.Blazor.Tippy/css/tippy.css", cancellationToken: token).NoSync();

                await _resourceLoader.LoadScriptAndWaitForVariable("_content/Soenneker.Blazor.Tippy/js/popper.min.js", "Popper", cancellationToken: token)
                                     .NoSync();

                await _resourceLoader.LoadScriptAndWaitForVariable("_content/Soenneker.Blazor.Tippy/js/tippy.umd.js", "tippy", cancellationToken: token)
                                     .NoSync();
            }

            await _resourceLoader.ImportModuleAndWaitUntilAvailable(_module, _moduleVariable, 100, token).NoSync();

            return new object();
        });
    }

    public async ValueTask Initialize(string elementId, TippyConfiguration tippyConfiguration, CancellationToken cancellationToken = default)
    {
        await _scriptInitializer.Init(cancellationToken, tippyConfiguration.UseCdn).NoSync();

        await _jsRuntime.InvokeVoidAsync($"{_moduleVariable}.initialize", cancellationToken, elementId, tippyConfiguration).NoSync();
    }

    public ValueTask Hide(string elementId, CancellationToken cancellationToken = default)
    {
        return _jsRuntime.InvokeVoidAsync($"{_moduleVariable}.hide", cancellationToken, elementId);
    }

    public ValueTask Show(string elementId, CancellationToken cancellationToken = default)
    {
        return _jsRuntime.InvokeVoidAsync($"{_moduleVariable}.show", cancellationToken, elementId);
    }

    public ValueTask Destroy(string elementId, CancellationToken cancellationToken = default)
    {
        return _jsRuntime.InvokeVoidAsync($"{_moduleVariable}.destroy", cancellationToken, elementId);
    }

    public async ValueTask DisposeAsync()
    {
        await _resourceLoader.DisposeModule(_module).NoSync();

        await _scriptInitializer.DisposeAsync().NoSync();
    }
}