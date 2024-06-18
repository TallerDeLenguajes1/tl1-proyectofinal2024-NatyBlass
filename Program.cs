//using ServicioWeb;
using System.Text.Json;

var salida = await GetPersonaje();

static async Task<Root> GetPersonaje()
    {
        Random idAleatorio = new Random();
        int idPersonaje = idAleatorio.Next(0,732);

        var url = $"https://www.superheroapi.com/api.php/5248ed3c2a550909d2dd451fe25e718f/{idPersonaje}";

        try
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Root infoPersonaje = JsonSerializer.Deserialize<Root>(responseBody);
            return infoPersonaje;
        }
        catch (HttpRequestException err)
        {
            Console.WriteLine("Problemas de Acceso \n Mensaje: " + err.Message);
            return null;
        }
    }


var nombre = salida.infoPersonaje.name;
Console.WriteLine("Nombre del personaje: " +nombre);