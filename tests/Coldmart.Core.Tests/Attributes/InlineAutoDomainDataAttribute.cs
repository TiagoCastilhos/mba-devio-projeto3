using AutoFixture.Xunit2;

namespace Coldmart.Core.Tests.Attributes;

public sealed class InlineAutoDomainDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoDomainDataAttribute(params object[] values)
        : base(new AutoDomainDataAttribute(), values)
    { }
}