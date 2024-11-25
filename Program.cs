// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text.Json;


var serializer = new CsvSerializer();
var testObject = F.Get();
const int iterations = 100000;

// Замер времени на кастомную сериализацию
var stopwatch = Stopwatch.StartNew();
for (int i = 0; i < iterations; i++)
{
    var csv = serializer.Serialize(testObject);
}
stopwatch.Stop();
Console.WriteLine($"Custom serialization time: {stopwatch.ElapsedMilliseconds} ms");

// Замер времени на стандартную JSON сериализацию
stopwatch.Restart();
for (int i = 0; i < iterations; i++)
{
    var json = JsonSerializer.Serialize(testObject);
}
stopwatch.Stop();
Console.WriteLine($"JSON serialization time: {stopwatch.ElapsedMilliseconds} ms");

// Замер времени вывода в консоль
var csvString = serializer.Serialize(testObject);
stopwatch.Restart();
Console.WriteLine(csvString);
stopwatch.Stop();
Console.WriteLine($"Console output time: {stopwatch.ElapsedMilliseconds} ms");

// Замер времени на десериализацию
stopwatch.Restart();
for (int i = 0; i < iterations; i++)
{
    var deserializedObject = serializer.Deserialize<F>(csvString);
}
stopwatch.Stop();
Console.WriteLine($"Custom deserialization time: {stopwatch.ElapsedMilliseconds} ms");

// Стандартная JSON десериализация
var jsonString = JsonSerializer.Serialize(testObject);
stopwatch.Restart();
for (int i = 0; i < iterations; i++)
{
    var deserializedObject = JsonSerializer.Deserialize<F>(jsonString);
}
stopwatch.Stop();
Console.WriteLine($"JSON deserialization time: {stopwatch.ElapsedMilliseconds} ms");

