using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

public class GeneracionPersonaje
{
    private int inteligencia;
    private int fuerza;
    private int velocidad;
    private int durabilidad;
    private int poder;
    private int combate;
    private int id;
    private string nombre;

    public int Inteligencia { get => inteligencia; set => inteligencia = value; }
    public int Fuerza { get => fuerza; set => fuerza = value; }
    public int Velocidad { get => velocidad; set => velocidad = value; }
    public int Durabilidad { get => durabilidad; set => durabilidad = value; }
    public int Poder { get => poder; set => poder = value; }
    public int Combate { get => combate; set => combate = value; }
    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
}

public class FabricaDePersonajes
{
    ServicioWeb servicioWeb = new ServicioWeb();

    //Task me permite ejecutar una operacion de forma asincronica sin "bloquear" el hilo principal
    public async Task<GeneracionPersonaje> CrearPersonaje() //Debo ponerlo como un método asincronico para que el await no de problema
    {
        var personaje = await servicioWeb.GetPersonaje();

        GeneracionPersonaje nuevoPersonaje = new GeneracionPersonaje
        {
            Nombre = personaje.name,
            Inteligencia = TryParseInt(personaje.powerstats.intelligence),
            Fuerza = TryParseInt(personaje.powerstats.strength),
            Velocidad = TryParseInt(personaje.powerstats.speed),
            Durabilidad = TryParseInt(personaje.powerstats.durability),
            Poder = TryParseInt(personaje.powerstats.power),
            Combate = TryParseInt(personaje.powerstats.combat),
            Id = TryParseInt(personaje.id)
        };

        return nuevoPersonaje;
    }


    //Para Verificar antes de convertir string a enteros : - recomendación del chat gpt debido a que me dio un error de la nada - 
    private int TryParseInt(string value)
    {
        if (int.TryParse(value, out int result))
        {
            return result;
        }
        else
        {
            return 0; // Valor predeterminado si la conversión falla
        }
    }


}