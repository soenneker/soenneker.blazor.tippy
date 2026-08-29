[![](https://img.shields.io/nuget/v/soenneker.blazor.tippy.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.tippy/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.tippy/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.tippy/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.tippy.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.tippy/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.tippy/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.tippy/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Blazor.Tippy

A small Blazor component and JS interop wrapper for text tooltips powered by Tippy.js and Popper.

## Installation

```bash
dotnet add package Soenneker.Blazor.Tippy
```

Register the interop service in `Program.cs`:

```csharp
using Soenneker.Blazor.Tippy.Registrars;

builder.Services.AddTippyInteropAsScoped();
```

Add the component namespace to `_Imports.razor`:

```razor
@using Soenneker.Blazor.Tippy
```

## Component usage

`Tippy` renders a wrapper element, initializes after the first interactive render, and cleans up its browser instance when removed:

```razor
@using Soenneker.Blazor.Tippy.Configuration

<Tippy @ref="_tooltip"
       Configuration="_configuration"
       OnReady="HandleReady"
       tabindex="0"
       aria-label="Shipping information">
    Shipping details
</Tippy>

<button type="button" disabled="@(!_ready)" @onclick="ShowAsync">Show tooltip</button>
<button type="button" disabled="@(!_ready)" @onclick="HideAsync">Hide tooltip</button>

@code {
    private Tippy? _tooltip;
    private bool _ready;

    private readonly TippyConfiguration _configuration = new()
    {
        Content = "Orders usually leave the warehouse within two business days.",
        Placement = "bottom",
        Trigger = "mouseenter focus"
    };

    private void HandleReady() => _ready = true;
    private async Task ShowAsync() => await _tooltip!.Show();
    private async Task HideAsync() => await _tooltip!.Hide();
}
```

Use a focusable control or add `tabindex="0"` when keyboard users must be able to trigger the tooltip. Do not put essential instructions only in hover content; keep critical information available in normal page content as well.

Configuration changes are applied after a subsequent render by recreating the browser instance. `Destroy()` permanently destroys that component instance; use conditional rendering to create a new one later.

## Attaching to an existing element

Use `ITippyInterop` when the reference element must remain your own button, link, or other DOM node:

```razor
@using Soenneker.Blazor.Tippy.Abstract
@using Soenneker.Blazor.Tippy.Configuration
@inject ITippyInterop TippyInterop

<button id="delete-help" type="button">Delete</button>

@code {
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
            return;

        await TippyInterop.Initialize("delete-help", new TippyConfiguration
        {
            Content = "This permanently deletes the draft.",
            Placement = "right",
            Trigger = "mouseenter focus"
        });
    }
}
```

Direct interop instances are scoped by element ID. Call `Destroy(elementId)` if the target is removed outside normal Blazor disposal; automatic DOM removal observation also cleans up detached targets.

## Configuration

| Property | Purpose |
| --- | --- |
| `Content` | Tooltip text. HTML rendering is not enabled by this wrapper. |
| `Theme` | Tippy theme name; custom themes require matching application CSS. |
| `Interactive` | Allows pointer interaction with the tooltip popper. |
| `Trigger` | Tippy trigger string, such as `mouseenter focus`, `click`, or `manual`. |
| `Placement` | Preferred placement, such as `top`, `bottom`, `left`, or `right`. |
| `UseCdn` | Loads pinned Tippy/Popper assets from jsDelivr when `true`; uses bundled package assets when `false`. |

`Content` is passed as a string with Tippy's default HTML handling disabled, so markup is displayed as text rather than inserted into the DOM. This package does not expose arbitrary Tippy plugins, callbacks, DOM nodes, or raw HTML content.

When `UseCdn` is enabled, your Content Security Policy and network controls must permit jsDelivr. Set it to `false` when third-party asset loading is not allowed.
