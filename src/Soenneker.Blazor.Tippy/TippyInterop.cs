using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Tippy.Abstract;
using Soenneker.Blazor.Tippy.Configuration;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Blazor.Tippy;

///<inheritdoc cref="ITippyInterop"/>
public sealed class TippyInterop : ITippyInterop
{
    private const string _modulePath = "_content/Soenneker.Blazor.Tippy/js/tippyinterop.js";

    private readonly IResourceLoader _resourceLoader;
    private readonly IModuleImportUtil _moduleImportUtil;

    private readonly AsyncInitializer<TippyConfiguration> _scriptInitializer;

    private readonly CancellationScope _cancellationScope = new();

    public TippyInterop(IResourceLoader resourceLoader, IModuleImportUtil moduleImportUtil)
    {
        _resourceLoader = resourceLoader;
        _moduleImportUtil = moduleImportUtil;

        _scriptInitializer = new AsyncInitializer<TippyConfiguration>(Initialize);
    }

    private static string NormalizeContentUri(string uri)
    {
        if (string.IsNullOrEmpty(uri) || uri.Contains("://", StringComparison.Ordinal))
            return uri;

        return uri[0] == '/' ? uri : "/" + uri;
    }

    private async ValueTask Initialize(TippyConfiguration config, CancellationToken token)
    {
        if (config.UseCdn)
        {
            await _resourceLoader.LoadStyle("https://cdn.jsdelivr.net/npm/tippy.js@6.3.7/dist/tippy.css",
                "sha256-WWn0l9kVjXaC+CGcbxP6Zyac31v1Cjkx2VMnFR3uVng=", cancellationToken: token);
            await _resourceLoader.LoadScriptAndWaitForVariable("https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js", "Popper",
                "sha256-whL0tQWoY1Ku1iskqPFvmZ+CHsvmRWx/PIoEvIeWh4I=", cancellationToken: token);
            await _resourceLoader.LoadScriptAndWaitForVariable("https://cdn.jsdelivr.net/npm/tippy.js@6.3.7/dist/tippy.umd.js", "tippy",
                "sha256-AMOLcfKm4CGkIKi5aMXSELz3F7Q0SS0HKWOiTLtte1U=", cancellationToken: token);
        }
        else
        {
            await _resourceLoader.LoadStyle(NormalizeContentUri("_content/Soenneker.Blazor.Tippy/css/tippy.css"), cancellationToken: token);
            await _resourceLoader.LoadScriptAndWaitForVariable(NormalizeContentUri("_content/Soenneker.Blazor.Tippy/js/popper.min.js"), "Popper",
                cancellationToken: token);
            await _resourceLoader.LoadScriptAndWaitForVariable(NormalizeContentUri("_content/Soenneker.Blazor.Tippy/js/tippy.umd.js"), "tippy",
                cancellationToken: token);
        }

        _ = await _moduleImportUtil.GetContentModuleReference(_modulePath, token);
    }

    public async ValueTask Initialize(string elementId, TippyConfiguration tippyConfiguration, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            await _scriptInitializer.Init(tippyConfiguration, linked);
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("initialize", linked, elementId, tippyConfiguration);
        }
    }

    public async ValueTask Hide(string elementId, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("hide", linked, elementId);
        }
    }

    public async ValueTask Show(string elementId, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("show", linked, elementId);
        }
    }

    public async ValueTask Destroy(string elementId, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("destroy", linked, elementId);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _moduleImportUtil.DisposeContentModule(_modulePath);

        await _scriptInitializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
