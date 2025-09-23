using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace Coldmart.Core.Tests.Extensions;

public static class DbSetHelper
{
    public static Mock<DbSet<T>> CreateMockedDbSet<T>(List<T> objects) where T : class
    {
        var mock = objects.BuildMockDbSet();

        return mock;
    }
}