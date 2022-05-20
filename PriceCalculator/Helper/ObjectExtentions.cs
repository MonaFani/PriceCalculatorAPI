using System.Dynamic;
using System.Reflection;
using System.Text.Json;

namespace PriceCalculatorAPI.Helper
{
    public static class ObjectExtentions
    {
        public static ExpandoObject ShapeData<TSource>(
            this TSource source, string fields
            )
        {
            if (source == null)
            {
                throw new ArgumentException("source");
            }
            var expandObject = new ExpandoObject();
            var propertyInfoList = new List<PropertyInfo>();
            if (string.IsNullOrWhiteSpace(fields))
            {
                var propertyInfos = typeof(TSource).GetProperties();
                propertyInfoList.AddRange(propertyInfos);
            }
            else
            {
                var fieldsAfterSplit = fields.Split(',');
                foreach (var field in fieldsAfterSplit)
                {
                    var propetyName = field.Trim();
                    var propertyInfo = typeof(TSource).GetProperty(propetyName);

                    if (propertyInfo == null)
                    {
                        throw new Exception($"Property {propetyName} wasn't found on {typeof(TSource)}");
                    }

                    propertyInfoList.Add(propertyInfo);
                }

            }

            foreach (var propertyInfo in propertyInfoList)
            {
                var propertyValue = propertyInfo.GetValue(source);

                ((IDictionary<string, object>)expandObject).Add(propertyInfo.Name, propertyValue);

            }

            return expandObject;
        }
        public static ExpandoObject ShapeData<TSource>(
            this TSource source, object? valueNotEqual
            )
        {
            if (source == null)
            {
                throw new ArgumentException("source");
            }
            var expandObject = new ExpandoObject();
            var propertyInfoList = typeof(TSource).GetProperties();


            foreach (var propertyInfo in propertyInfoList)
            {
                var propertyValue = propertyInfo.GetValue(source);
                if (!propertyValue.Equals(valueNotEqual))
                    ((IDictionary<string, object>)expandObject).Add(propertyInfo.Name, propertyValue);

            }

            return expandObject;
        }

        public static string ToJsonCamelCase<TSource>(
            this TSource source)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            };

            return JsonSerializer.Serialize(source, serializeOptions);
        }

    }
}
