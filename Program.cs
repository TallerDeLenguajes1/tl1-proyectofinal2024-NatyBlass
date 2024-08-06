using System;
using System.Collections.Generic;
using System.Threading.Tasks;

ComplementoGrafico.MostrarTitulo();


PersonajesJson personajesJson = new PersonajesJson();
List<GeneracionPersonaje> personajes;

if (personajesJson.Existe("personajes.json"))
{
    personajes = personajesJson.LeerPjs("personajes.Json");
}
else
{
    FabricaDePersonajes fabrica = new FabricaDePersonajes();
    personajes = new List<GeneracionPersonaje>();
    for (int i = 0; i < 10; i++)
    {
        personajes.Add(await fabrica.CrearPersonaje());
    }

    personajesJson.GuardarPjs(personajes, "personajes.json");
}

//Para comprobar que se estén cargando bien los pjs.
/*foreach (var personaje in personajes)
{
    Console.WriteLine($"Nombre: {personaje.Nombre}, Inteligencia: {personaje.Inteligencia}, Fuerza: {personaje.Fuerza}, Velocidad: {personaje.Velocidad}, Durabilidad: {personaje.Durabilidad}, Poder: {personaje.Poder}, Combate: {personaje.Combate}, Id: {personaje.Id}");
}*/

//Mejora para eleccion de un personaje principal

Random random = new Random();
GeneracionPersonaje jugPrincipal = personajes[random.Next(personajes.Count)];
personajes.Remove(jugPrincipal); // Saco de la lista de personajes al jugador principal para que este no luche contra si mismo

int durabilidad = jugPrincipal.Durabilidad;

int vidas = 3;

//Ahora voy a empezar a aplicar la logica de combate

Combate combate = new Combate();

foreach (var enemigo in personajes) 
{
    if (vidas > 0)
    {
        Console.WriteLine($"\n{jugPrincipal.Nombre} vs {enemigo.Nombre}");
        GeneracionPersonaje jugGanador = combate.IniciarCombate(jugPrincipal, enemigo);

        if(jugGanador == jugPrincipal)
        {
            ComplementoGrafico.HasGanado();
        }
        else
        {
            vidas--;
            jugPrincipal.Durabilidad = durabilidad;
            ComplementoGrafico.HasPerdido();
        }
    }

}

if(vidas > 0)
{
    ComplementoGrafico.GanadorFinal();
}
else
{
    ComplementoGrafico.DerrotaFinal();
}