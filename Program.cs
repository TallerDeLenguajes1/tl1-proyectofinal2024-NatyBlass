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

//Ahora voy a empezar a aplicar la logica de combate

Combate combate = new Combate();
for (int i = 0; i < personajes.Count -1 ; i += 2) 
{
    var ganador = combate.IniciarCombate(personajes[i], personajes[i + 1]);
    
    if (ganador != null)
    {
        Console.WriteLine($"Ganador del combate entre {personajes[i].Nombre} y {personajes[i + 1].Nombre}: {ganador.Nombre}");
    }
    else
    {
        Console.WriteLine($"Empate entre {personajes[i].Nombre} y {personajes[i + 1].Nombre}");
    }
}