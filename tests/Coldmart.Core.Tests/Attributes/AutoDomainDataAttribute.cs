using AutoFixture;
using AutoFixture.Xunit2;
using Coldmart.Core.Tests.Customizations;

namespace Coldmart.Core.Tests.Attributes;

public sealed class AutoDomainDataAttribute : AutoDataAttribute
{
    public AutoDomainDataAttribute()
        : base(() =>
        {
            var fixture = new Fixture()
                .Customize(new CommonCompositionCustomization());

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        })
    { }
}