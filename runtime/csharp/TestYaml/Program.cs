using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

// Try dynamic
var deserializer = new DeserializerBuilder()
    .WithNamingConvention(CamelCaseNamingConvention.Instance)
    .Build();

Console.WriteLine("Testing:");
Console.WriteLine($"\"4\" to object -> {deserializer.Deserialize<object>("4")?.GetType().Name}");
Console.WriteLine($"\"4\" to int -> {deserializer.Deserialize<int>("4")}");
Console.WriteLine($"\"4\" to Dictionary -> fails because it's scalar");

