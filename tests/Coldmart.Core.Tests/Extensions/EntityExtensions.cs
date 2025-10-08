namespace Coldmart.Core.Tests.Extensions;

public static class EntityExtensions
{
    public static void SetProperty<T, TProperty>(this T entity, string propertyName, TProperty value) where T : class
    {
        var propertyInfo = typeof(T).GetProperty(propertyName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
        propertyInfo!.SetValue(entity, value);
    }
}