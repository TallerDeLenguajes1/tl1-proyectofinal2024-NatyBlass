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
    private int rondasJugadas;

    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
    public DateTime Fecha { get => fecha; set => fecha = value; }
    public List<GeneracionPersonaje> Personajes { get => personajes; set => personajes = value; }
    public GeneracionPersonaje PjPrincipal { get => pjPrincipal; set => pjPrincipal = value; }
    public int Vidas { get => vidas; set => vidas = value; }
    public int RondasJugadas { get => rondasJugadas; set => rondasJugadas = value; }
    
    }


public class HistorialJson
{
    private readonly string ArchivoPartidas = "partidas.json";
    
    public HistorialJson(string nombreArchivo)
    {
        ArchivoPartidas = nombreArchivo;
    }

    public void GuardarGanador(GeneracionPersonaje ganador)
    {
        List<GeneracionPersonaje> ganadores;

        if (File.Exists(ArchivoPartidas))
        {
            string jsonString = File.ReadAllText(ArchivoPartidas);
            ganadores = JsonSerializer.Deserialize<List<GeneracionPersonaje>>(jsonString);
        }
        else
        {
            ganadores = new List<GeneracionPersonaje>();
        }
        
        ganadores.Add(ganador);

        File.WriteAllText(ArchivoPartidas, JsonSerializer.Serialize(ganadores));
    }

    public List<GeneracionPersonaje> LeerGanadores(string ArchivoPartidas)
    {
        if (File.Exists(ArchivoPartidas))
        {
            string jsonString = File.ReadAllText(ArchivoPartidas);
            return JsonSerializer.Deserialize<List<GeneracionPersonaje>>(jsonString);
        }

        return new List<GeneracionPersonaje>();
    }

    public bool Existe(string ArchivoPartidas)
    {
        return File.Exists(ArchivoPartidas) && new FileInfo(ArchivoPartidas).Length > 0;
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

    public void GuardarHistorial(List<Partida> historial)
    {
        string jsonString = JsonSerializer.Serialize(historial, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(ArchivoPartidas, jsonString);
    }

    public void GuardarPartida(Partida partida)
    {
        var historial = LeerHistorial();
        historial.Add(partida);
        GuardarHistorial(historial);
    }

    public List<Partida> LeerHistorial()
    {
        if (File.Exists(ArchivoPartidas))
        {
            string jsonString = File.ReadAllText(ArchivoPartidas);
            return JsonSerializer.Deserialize<List<Partida>>(jsonString);
        }

        return new List<Partida>();
    }

}

}