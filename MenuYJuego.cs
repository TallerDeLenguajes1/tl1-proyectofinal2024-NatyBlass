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

        Console.WriteLine("                                                     =====================================");
        Console.WriteLine("                                                         1 - INICIAR JUEGO NUEVO");
        Console.WriteLine("                                                         2 -  CONTINUAR JUEGO");
        Console.WriteLine("                                                         3 - VER HISTORIAL DE PARTIDAS");
        Console.WriteLine("                                                     =====================================");
        
        opc = Console.ReadLine();

        switch (opc)
        {
            case "1":
                IniciarJuegoNuevo();
                break;
            case "2":
                ContinuarJuego();
                break;
            case "3":
                VerHistorialDePartidas();
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

        Console.WriteLine("                                                     ======================================");
        Console.WriteLine("                                                               INGRESE SU NICKNAME");
        
        nickname = Console.ReadLine();

        Console.WriteLine("Nickname ingresado: " + nickname);

        Console.ReadKey();

        // FabricaDePersonajes fabricaDePersonajes = new FabricaDePersonajes();
        // try
        // {
        //     await fabricaDePersonajes.ObtenerYGuardarPersonajes();
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine($"Error al obtener personajes: {ex.Message}");
        //     return;
        // }

        PersonajesJson personajesJson = new PersonajesJson();
        List<GeneracionPersonaje> personajes = new List<GeneracionPersonaje>();
        List<GeneracionPersonaje> personajesFilt;
        
        if (personajesJson.Existe("personajes.json"))
        {
            personajesFilt = personajesJson.LeerPjs("personajes.Json");

            Random rand1 = new Random();

            for (int i = 0; i < 10; i++)
            {
                personajes.Add(personajesFilt[rand1.Next(0,51)]);
                
            }
        }
        else
        {
            FabricaDePersonajes fabrica = new FabricaDePersonajes();
            personajesFilt = new List<GeneracionPersonaje>();
            for (int i = 0; i < 10; i++)
            {
                personajesFilt.Add(await fabrica.CrearPersonaje());
            }

            personajesJson.GuardarPjs(personajesFilt, "personajes.json");

            Random rand1 = new Random();

            for (int i = 0; i < 10; i++)
            {
                personajes.Add(personajesFilt[rand1.Next(0,51)]);
                
            }
        }


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

        //Console.WriteLine("Llamando a JugarPartida...");
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

    private void VerHistorialDePartidas()
    {
        List<Partida> partidasGuardadas = historialJson_.LeerPartidasGuardadas();

        if (partidasGuardadas.Count == 0)
        {
            Console.WriteLine("                     NO HAY PARTIDAS GUARDADAS");
            return;
        }

        Console.WriteLine("                                                     =====================================");
        Console.WriteLine("                                                            HISTORIAL DE PARTIDAS");
        Console.WriteLine("                                                     =====================================");
        
        foreach (var partida in partidasGuardadas)
        {
            Console.WriteLine("                                       ==============================================================");
            Console.WriteLine($"                                                      NICKNAME: {partida.NombreUsuario}");
            Console.WriteLine($"                                                         FECHA: {partida.Fecha}");
            Console.WriteLine($"                                           PERSONAJE PRINCIPAL: {partida.PjPrincipal.Nombre}");
            Console.WriteLine($"                                                     VICTORIAS: {partida.PjPrincipal.Victorias}");
            Console.WriteLine("                                                   =====================================");
            Console.WriteLine($"                                                              ESTADISTICAS");
            Console.WriteLine($"                                                    - Inteligencia: {partida.PjPrincipal.Inteligencia}");
            Console.WriteLine($"                                                    - Fuerza: {partida.PjPrincipal.Fuerza}");
            Console.WriteLine($"                                                    - Velocidad: {partida.PjPrincipal.Velocidad}");
            Console.WriteLine($"                                                    - Durabilidad: {partida.PjPrincipal.Durabilidad}");
            Console.WriteLine($"                                                    - Poder: {partida.PjPrincipal.Poder}");
            Console.WriteLine($"                                                    - Combate: {partida.PjPrincipal.Combate}");
            Console.WriteLine();
        }

        Console.WriteLine("Presione cualquier tecla para volver al menú...");
        Console.ReadKey();
        MostrarMenu();
    }

}
public class IntroduccionDelJuego
{
    public void MostrarIntroduccion()
    {
        Console.WriteLine("                                  ========================================================================");
        Console.WriteLine("                                                    BIENVENIDO A LA GUERRA DE LOS MUNDOS");
        Console.WriteLine("                                  ========================================================================");
        Console.WriteLine();
        Console.WriteLine("                                    ¡Prepárate para una emocionante batalla entre héroes y villanos de");
        Console.WriteLine("                                    los universos más épicos! En este juego, tendrás la oportunidad de");
        Console.WriteLine("                                    luchar con personajes icónicos de Marvel, DC, Harry Potter y muchos");
        Console.WriteLine("                                    otros mundos fascinantes.");
        Console.WriteLine();
        Console.WriteLine("                                    Cada personaje tiene habilidades únicas, poderes impresionantes,");
        Console.WriteLine("                                    y una historia fascinante.");
        Console.WriteLine();
        Console.WriteLine("                                    ¡Que comiencen las batallas y que el mejor luchador se alce con la");
        Console.WriteLine("                                    victoria!");
        Console.WriteLine();
        Console.WriteLine("                                  ========================================================================");
        Console.WriteLine("                                                     ¡Buena suerte y que gane el mejor!");
        Console.WriteLine("                                  ========================================================================");
        Console.WriteLine();
        //Console.WriteLine("Presione cualquier tecla para continuar...");
        //Console.ReadKey();
    }
}

