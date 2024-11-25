
using System.Reflection;

public class CsvSerializer
{
    // Сериализация объекта в строку CSV
    public string Serialize<T>(T obj)
    {
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var values = properties.Select(p => p.GetValue(obj)?.ToString() ?? string.Empty);
        return string.Join(",", values);
    }

    // Десериализация строки CSV в объект
    public T Deserialize<T>(string csv) where T : new()
    {
        var obj = new T();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var values = csv.Split(',');

        for (int i = 0; i < properties.Length && i < values.Length; i++)
        {
            var property = properties[i];
            if (property.CanWrite)
            {
                var value = Convert.ChangeType(values[i], property.PropertyType);
                property.SetValue(obj, value);
            }
        }
        return obj;
    }
}


