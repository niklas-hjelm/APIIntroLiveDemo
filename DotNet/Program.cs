var builder = WebApplication.CreateBuilder(args);

Dictionary<int, Person> people = [];
var id = 1;

builder.Services.AddCors(a => a.AddDefaultPolicy(b => b.AllowAnyOrigin()));

builder.Services.AddHttpClient("NodeApi", http =>
{
    http.BaseAddress = new Uri("http://localhost:3000");
});

var app = builder.Build();

app.MapGet("/api/dotnet", () =>
{
    return people;
});

app.MapPost("/api/dotnet", async (IHttpClientFactory clientFactory, Person person) =>
{
    people.Add(id++, person);
    System.Console.WriteLine(person);
    using var client = clientFactory.CreateClient("NodeApi");
    await client.PostAsJsonAsync<Person>("/api/node", person);

    return "OK";
});

app.MapGet("/api/dotnet/get-from-nodeApi", async (IHttpClientFactory clientFactory) =>
{
    using var client = clientFactory.CreateClient("NodeApi");
    var result = await client.GetFromJsonAsync<List<Person>>("/api/node");
    return result;
});

app.Run();

record Person(string Name, int Age);