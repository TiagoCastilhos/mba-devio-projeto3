using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Coldmart.Core.Tests.Extensions;

public static class MockDbContextExtensions
{
    public static void SetupDbSet<T, TDbSet>(this Mock<DbSet<T>> dbSet, Expression<Func<T, TDbSet>> expression, List<T> values) where T : IdentityDbContext
    {
        var data = values.AsQueryable();
        dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
        dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);
    }
}
