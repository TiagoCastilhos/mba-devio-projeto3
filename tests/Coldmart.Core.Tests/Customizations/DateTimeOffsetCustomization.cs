using AutoFixture;

namespace Coldmart.Core.Tests.Customizations;

public sealed class DateTimeOffsetCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<DateTimeOffset>(b => b.FromFactory(() => DateTimeOffset.UtcNow.AddDays(1)));
    }
}
