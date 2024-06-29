using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class PersonajesJson
{
    public void GuardarPjs(List<GeneracionPersonaje> personajes, string nombreArchivo)
    {
        string jsonString = JsonSerializer.Serialize(personajes);
        File.WriteAllText(nombreArchivo, jsonString);
    }

    public List<GeneracionPersonaje> LeerPjs(string nombreArchivo)
    {
        if(File.Exists(nombreArchivo))
        {
            string jsonString = File.ReadAllText(nombreArchivo);
            return JsonSerializer.Deserialize<List<GeneracionPersonaje>>(jsonString);
        }
        return new List<GeneracionPersonaje>();
    }

    public bool Existe(string nombreArchivo)
    {
        return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0;
    }

}