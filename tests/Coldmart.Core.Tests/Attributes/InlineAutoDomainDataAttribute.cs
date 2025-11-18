using System.Diagnostics.CodeAnalysis;
using AutoFixture.Xunit2;

namespace Coldmart.Core.Tests.Attributes;

[ExcludeFromCodeCoverage]
public sealed class InlineAutoDomainDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoDomainDataAttribute(params object[] values)
        : base(new AutoDomainDataAttribute(), values)
    { }
}