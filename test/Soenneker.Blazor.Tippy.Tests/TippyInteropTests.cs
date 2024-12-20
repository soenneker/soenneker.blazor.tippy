using Soenneker.Blazor.Tippy.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Blazor.Tippy.Tests;

[Collection("Collection")]
public class TippyInteropTests : FixturedUnitTest
{
    private readonly ITippyInterop _util;

    public TippyInteropTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<ITippyInterop>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
