using System.Text.Json;
var salida = await GetPersonaje();

static async Task<Root> GetPersonaje()
{
    var url = "https://www.superheroapi.com/api.php/5248ed3c2a550909d2dd451fe25e718f/2";

    try
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        Root personaje = JsonSerializer.Deserialize<Root>(responseBody);
        return personaje;
    }
    catch (HttpRequestException err)
    {
        Console.WriteLine(err.Message);
        return null;
    }
}

var nombre = salida.powerstats.intelligence;
Console.WriteLine(nombre);