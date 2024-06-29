using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

//Para comprobar que se estén cargando bien los pjs
foreach (var personaje in personajes)
{
    Console.WriteLine($"Nombre: {personaje.Nombre}, Inteligencia: {personaje.Inteligencia}, Fuerza: {personaje.Fuerza}, Velocidad: {personaje.Velocidad}, Durabilidad: {personaje.Durabilidad}, Poder: {personaje.Poder}, Combate: {personaje.Combate}, Id: {personaje.Id}");
}