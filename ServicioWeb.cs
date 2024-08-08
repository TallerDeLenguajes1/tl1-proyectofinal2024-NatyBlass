using System.Text.Json;

public class ServicioWeb
{
    private static readonly Random random = new Random(); // Instanciar fuera del método fue una sugerencia para que siempre genere randoms.

    public async Task<Root> GetPersonaje()
    {
        
        int idPersonajes = random.Next(0,732);
        var url = $"https://www.superheroapi.com/api.php/5248ed3c2a550909d2dd451fe25e718f/{idPersonajes}";

        try
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            //client.Timeout = TimeSpan.FromSeconds(5);
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

}
    