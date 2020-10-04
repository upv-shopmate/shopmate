using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace ShopMate.Persistence
{
    public static class ValueConversionExtensions
    {
        public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> property)
        {
            var converter = new ValueConverter<T, string>(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<T>(v));

            var comparer = new ValueComparer<T>(
                (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
                v => v == null ? 0 : JsonConvert.SerializeObject(v).GetHashCode(),
                v => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(v)));

            property.HasConversion(converter);
            property.Metadata.SetValueConverter(converter);
            property.Metadata.SetValueComparer(comparer);

            return property;
        }
    }
}
