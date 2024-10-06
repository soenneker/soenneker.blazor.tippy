[![](https://img.shields.io/nuget/v/soenneker.blazor.tippy.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.tippy/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.tippy/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.tippy/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.tippy.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.tippy/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Blazor.Tippy
### A Blazor interop library for Tippy.js

## Installation

```
dotnet add package Soenneker.Blazor.Tippy
```

## Usage

1. Register the interop within DI (`Program.cs`)

```csharp
public static async Task Main(string[] args)
{
    ...
    builder.Services.AddTippy();
}
```

Scripts and styles will get dynamically and lazily loaded for you.

2. Inject `ITippyInterop` within your `App.Razor` file


```razor
@using Soenneker.Blazor.Masonry.Abstract
@inject ITippyInterop TippyInterop

<div id="tooltipElement">Hover me!</div>

@code {
    protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var options = new TippyOptions
                {
                    Content = "This is a tooltip!",
                    Theme = "light",
                    Interactive = true,
                    Trigger = "click",
                    Placement = "bottom"
                };

                // Initialize the Tippy tooltip on the element
                await TippyInterop.Init("tooltipElement", options);
            }
        }
}
```