using Soenneker.Blazor.Tippy.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;
using Xunit.Abstractions;

namespace Soenneker.Blazor.Tippy.Tests;

[Collection("Collection")]
public class TippyInteropTests : FixturedUnitTest
{
    private readonly ITippyInterop _interop;

    public TippyInteropTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _interop = Resolve<ITippyInterop>(true);
    }
}
