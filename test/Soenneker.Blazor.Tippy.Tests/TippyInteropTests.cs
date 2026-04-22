using Soenneker.Blazor.Tippy.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blazor.Tippy.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class TippyInteropTests : HostedUnitTest
{
    private readonly ITippyInterop _util;

    public TippyInteropTests(Host host) : base(host)
    {
        _util = Resolve<ITippyInterop>(true);
    }

    [Test]
    public void Default()
    {

    }
}
