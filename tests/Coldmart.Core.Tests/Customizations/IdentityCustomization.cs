using AutoFixture;
using Coldmart.Core.Services;
using Coldmart.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;

namespace Coldmart.Core.Tests.Customizations;

public sealed class IdentityCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<LogarViewModel>(b => b.With(v => v.Senha, "Senha@123"));

        fixture.Customize<IOptions<IdentityOptions>>(b => b.FromFactory(() =>
        {
            var storeOptions = fixture.Build<StoreOptions>().With(s => s.ProtectPersonalData, false).Create();

            var identityOptions = fixture.Build<IdentityOptions>()
                .With(o => o.Stores, storeOptions)
                .Create();

            var mock = new Mock<IOptions<IdentityOptions>>();
            mock.Setup(o => o.Value).Returns(identityOptions);

            return mock.Object;
        }));
    }
}