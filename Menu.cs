using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using EspacioPartidaEHistorial;
public class MenuYJuego
{

    private readonly HistorialJson historialJson_; //Tuve que aplicar un constructor ya que ahora hay uno dentro de HistorialJson
    
    public MenuYJuego (HistorialJson historialJson)
    {
        historialJson_ = historialJson;
    }

    public void MostrarMenu()
    {
        string opc;

        Console.WriteLine("                                                     ================================");
        Console.WriteLine("                                                         1 - INICIAR JUEGO NUEVO");
        Console.WriteLine("                                                         2 -  CONTINUAR JUEGO");
        Console.WriteLine("                                                     ================================");
        
        opc = Console.ReadLine();

        switch (opc)
        {
            case "1":
                IniciarJuegoNuevo();
                break;
            case "2":
                ContinuarJuego();
                break;
            default:
                Console.WriteLine("                                             LA OPCION QUE INGRESO NO ES VALIDA");
                MostrarMenu();
                break;
        }
    }

    //Para mejorar la organización, creé un método específico llamado "JugarPartida"

    private void JugarPartida(Partida partida)
    {
        Combate combate = new Combate();
        int durabilidad = partida.PjPrincipal.Durabilidad;
        int vidas = partida.Vidas;
        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine($"Enemigos {partida.Personajes.Count}");
            if (partida.Personajes.Count < 9)
            {
                Console.WriteLine("La lista de personajes no contiene suficientes enemigos para el combate.");
                return;
            }

            for(int i = 0; i < 9; i++)
            {
                var enemigo = partida.Personajes[i];
                Console.WriteLine($"\n                                                        {partida.PjPrincipal.Nombre} vs {enemigo.Nombre}");
                GeneracionPersonaje jugGanador = combate.IniciarCombate(partida.PjPrincipal, enemigo);
        
                if(jugGanador == partida.PjPrincipal)
                {
                    Console.WriteLine($"TE QUEDAN {partida.Vidas} VIDAS");
                    partida.PjPrincipal.Durabilidad = durabilidad; //Restauro la durabilidad principal de mi jugador
                }
                else
                {
                    partida.Vidas--;
                    Console.WriteLine($"TE QUEDAN {partida.Vidas} VIDAS");
                    partida.PjPrincipal.Durabilidad = durabilidad; //Restauro la durabilidad principal de mi jugador.

                    if (partida.Vidas == 0)
                    {
                        Console.WriteLine("                                         TE HAS QUEDADO SIN VIDAS");
                        break; //Este condicionante me ayuda a salir del bucle si el personaje perdió todas las vidas
                    }
                }
                
                ComplementoGrafico.MostrarLineasDivisorias();
                Console.WriteLine("Presione 'Enter' para ir al siguiente combate...");
                Console.ReadKey();
            }
                
            if (partida.Vidas == 0)
            {
                Console.WriteLine("                           PERDISTE LA OPORTUNIDAD DE SER EL DIOS DE LA GUERRA DE LOS MUNDOS");
                ComplementoGrafico.DerrotaFinal();
                continuar = false;
            }
            else
            {
                ComplementoGrafico.GanadorFinal();
                ComplementoGrafico.MostrarLineasDivisorias();

                Console.WriteLine("Si desea continuar jugando presione 'Enter'...");
                Console.WriteLine("Si no desea continuar jugando presione cualquier tecla...");
                var key = Console.ReadKey().Key;

                if (key != ConsoleKey.Enter)
                {
                    // Guardar la partida al finalizar
                    continuar = false;
                    Console.WriteLine("El juego ha terminado");
                    historialJson_.GuardarPartida(partida);
                    Console.WriteLine("PARTIDA GUARDADA");
                }
                else
                {
                    continuar = true;
                }
            }
        }
    }

    private async void IniciarJuegoNuevo() //async para poder usar await
    {
        string nickname;

        Console.WriteLine("                                                     ================================");
        Console.WriteLine("                                                            INGRESE SU NICKNAME");
        
        nickname = Console.ReadLine();

        Console.WriteLine("Nickname ingresado: " + nickname);

        //Console.ReadKey();

        FabricaDePersonajes fabricaDePersonajes = new FabricaDePersonajes();
        
        try
        {
            await fabricaDePersonajes.ObtenerYGuardarPersonajes();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener personajes: {ex.Message}");
            return;
        }

        List<GeneracionPersonaje> personajes = fabricaDePersonajes.LeerPjsJSON();

        if (personajes.Count == 0)
        {
            Console.WriteLine("No se pudieron cargar personajes. El juego no continuará.");
            return;
        }

        Random random = new Random();
        GeneracionPersonaje jugPrincipal = personajes[random.Next(personajes.Count)];
        personajes.Remove(jugPrincipal); // Saco de la lista de personajes al jugador principal para que este no luche contra si mismo

        //Para comprobar que se estén cargando bien los pjs.
        /*foreach (var personaje in personajes)
        {
            Console.WriteLine($"Nombre: {personaje.Nombre}, Inteligencia: {personaje.Inteligencia}, Fuerza: {personaje.Fuerza}, Velocidad: {personaje.Velocidad}, Durabilidad: {personaje.Durabilidad}, Poder: {personaje.Poder}, Combate: {personaje.Combate}, Id: {personaje.Id}");
        }*/

        jugPrincipal.MostrarAtributos();

        int vidas = 3;

        EspacioPartidaEHistorial.Partida nuevaPartida = new EspacioPartidaEHistorial.Partida
        {
            NombreUsuario = nickname,
            Fecha = DateTime.Now,
            Personajes = personajes,
            PjPrincipal = jugPrincipal,
            Vidas = vidas
        };

        Console.WriteLine("Llamando a JugarPartida...");
        JugarPartida(nuevaPartida);
    }

    private void ContinuarJuego()
    {
        //Voy a leer las partidas guardadas
        List<Partida> partidasGuardadas = historialJson_.LeerPartidasGuardadas();
        
        if (partidasGuardadas.Count == 0)
        {
            Console.WriteLine("                     NO HAY PARTIDAS GUARDADAS PARA CONTINUAR");
            return; //para salir del método
        }

        Console.WriteLine("                         INGRESE SU NICKNAME PARA BUSCAR PARTIDAS");
        string nickname = Console.ReadLine();

        Partida partidaParaContinuar = null;

        foreach (var partida in partidasGuardadas)
        {
            if (partida.NombreUsuario == nickname)
            {
                partidaParaContinuar = partida;
                break; //Salgo del bucle al encontrar la partida
            }
        }

        if (partidaParaContinuar == null)
        {
            Console.WriteLine($"                  NO SE ENCONTRARON PARTIDAS DEL JUGADOR {nickname}");
            return;
        }

        //con esto el usuario puede continuar su partida
        Console.WriteLine($"                     PARTIDA ENCONTRADA PARA {nickname}. CONTINUA JUGANDO");

        const string nombreArchivo = "personajes.json";

        PersonajesJson personajesJson = new PersonajesJson();
        List<GeneracionPersonaje> personajes = personajesJson.LeerPjs(nombreArchivo);

        GeneracionPersonaje jugPrincipal = partidaParaContinuar.PjPrincipal;

        personajes.Remove(jugPrincipal);

        partidaParaContinuar.Personajes = personajes;
        JugarPartida(partidaParaContinuar);
    }


}