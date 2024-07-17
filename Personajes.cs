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

    //Agregado de funcion para mejorar atributos

    public void MejorarAtributos(int atributo, double porcentaje)
    {
        switch (atributo)
        {
            case 1: 
                Inteligencia = (int)(Inteligencia * (1 + porcentaje /100));
                break;
            
            case 2:
                Fuerza = (int)(Fuerza *(1 + porcentaje /100));
                break;
            
            case 3:
                Velocidad = (int)(Velocidad * (1 + porcentaje / 100));
                break;

            case 4:
                Durabilidad = (int)(Durabilidad * (1 + porcentaje / 100));
                break;

            case 5:
                Poder = (int)(Poder * (1 + porcentaje / 100));
                break;
            
            case 6:
                Combate = (int)(Combate * (1 + porcentaje / 100));
                break;
        }
    }
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
            Random random = new Random();
            int randomStats = random.Next(0,101); //Para que no retorne 0 especificamente, le pido un aleatorio
            return randomStats; // Valor predeterminado si la conversión falla
        }
    }


}