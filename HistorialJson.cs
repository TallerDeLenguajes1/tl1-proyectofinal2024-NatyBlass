using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class HistorialJson
{
    public void GuardarGanador(GeneracionPersonaje ganador, string nombreArchivo)
    {
        List<GeneracionPersonaje> ganadores;

        if (File.Exists(nombreArchivo))
        {
            string jsonString = File.ReadAllText(nombreArchivo);
            ganadores = JsonSerializer.Deserialize<List<GeneracionPersonaje>>(jsonString);
        }
        else
        {
            ganadores = new List<GeneracionPersonaje>();
        }
        
        ganadores.Add(ganador);

        File.WriteAllText(nombreArchivo, JsonSerializer.Serialize(ganadores));
    }

    public List<GeneracionPersonaje> LeerGanadores(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
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
