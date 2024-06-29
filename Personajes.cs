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
            Inteligencia = int.Parse(personaje.powerstats.intelligence),
            Fuerza = int.Parse(personaje.powerstats.strength),
            Velocidad = int.Parse(personaje.powerstats.speed),
            Durabilidad = int.Parse(personaje.powerstats.durability),
            Poder = int.Parse(personaje.powerstats.power),
            Combate = int.Parse(personaje.powerstats.combat),
            Id = int.Parse(personaje.id)
        };

        return nuevoPersonaje;
    }


}