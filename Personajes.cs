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
    var personaje = await servicioWeb.GetPersonaje();

    GeneracionPersonaje nuevoPersonaje = new GeneracionPersonaje();

    nuevoPersonaje.Nombre = personaje.name;
    nuevoPersonaje.Inteligencia = int.Parse(personaje.powerstats.intelligence);
    nuevoPersonaje.Fuerza = int.Parse(personaje.powerstats.strength);
    nuevoPersonaje.Velocidad = int.Parse(personaje.powerstats.speed);
    nuevoPersonaje.Durabilidad = int.Parse(personaje.powerstats.durability);
    nuevoPersonaje.Poder = int.Parse(personaje.powerstats.power);
    nuevoPersonaje.Combate = int.Parse(personaje.powerstats.combat);
    nuevoPersonaje.Id = int.Parse(personaje.id);

}