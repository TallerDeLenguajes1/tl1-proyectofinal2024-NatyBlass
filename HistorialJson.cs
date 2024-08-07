using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EspacioPartidaEHistorial //me daba un error al compilar sin un namespace
{
    public class Partida
    {
    private string nombreUsuario;
    private DateTime fecha;
    private List<GeneracionPersonaje> personajes;
    private GeneracionPersonaje pjPrincipal;
    private int vidas;

    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
    public DateTime Fecha { get => fecha; set => fecha = value; }
    public List<GeneracionPersonaje> Personajes { get => personajes; set => personajes = value; }
    public GeneracionPersonaje PjPrincipal { get => pjPrincipal; set => pjPrincipal = value; }
    public int Vidas { get => vidas; set => vidas = value; }
    }


public class HistorialJson
{
    private const string ArchivoPartidas = "partidas.json";

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

    public List<Partida> CargarHistorial()
    {
        if(!File.Exists(ArchivoPartidas))
        {
            return new List<Partida>();
        }

        string jsonString = File.ReadAllText(ArchivoPartidas);
        return JsonSerializer.Deserialize<List<Partida>>(jsonString);
    }

}

}