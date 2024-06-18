using System.Text.Json;

public class ServicioWeb
{
    public async Task<Root> GetPersonaje()
    {
        Random random = new Random();
        int idPersonajes = random.Next(0,732);
        var url = $"https://www.superheroapi.com/api.php/5248ed3c2a550909d2dd451fe25e718f/{idPersonajes}";

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

}
    