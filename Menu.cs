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
            for(int i = 0; i < 9; i++)
            {
                var enemigo = partida.Personajes[i];
                Console.WriteLine($"\n                                                        {partida.PjPrincipal.Nombre} vs {enemigo.Nombre}");
                GeneracionPersonaje jugGanador = combate.IniciarCombate(partida.PjPrincipal, enemigo);
        
                if(jugGanador == partida.PjPrincipal)
                {
                    Console.WriteLine($"TE QUEDAN {partida.Vidas} VIDAS");
                    ComplementoGrafico.HasGanado();
                }
                else
                {
                    partida.Vidas--;
                    Console.WriteLine($"TE QUEDAN {partida.Vidas} VIDAS");
                    partida.PjPrincipal.Durabilidad = durabilidad; //Restauro la durabilidad principal de mi jugador.
                    ComplementoGrafico.HasPerdido();

                    if (partida.Vidas == 0)
                    {
                        Console.WriteLine("                                         TE HAS QUEDADO SIN VIDAS");
                        break; //Este condicionante me ayuda a salir del bucle si el personaje perdió todas las vidas
                    }
                }
                
                ComplementoGrafico.MostrarLineasDivisorias();
                Console.WriteLine("Presione 'Enter' para ir al siguiente combate...");
                while(Console.ReadKey(true).Key != ConsoleKey.Enter);
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

        //Traje esto de Program para que directamente desde esta clase pueda realizar un método de Juego o ContinuarJuego

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

        Random random = new Random();
        GeneracionPersonaje jugPrincipal = personajes[random.Next(personajes.Count)];
        personajes.Remove(jugPrincipal); // Saco de la lista de personajes al jugador principal para que este no luche contra si mismo

        //Para comprobar que se estén cargando bien los pjs.
        /*foreach (var personaje in personajes)
        {
            Console.WriteLine($"Nombre: {personaje.Nombre}, Inteligencia: {personaje.Inteligencia}, Fuerza: {personaje.Fuerza}, Velocidad: {personaje.Velocidad}, Durabilidad: {personaje.Durabilidad}, Poder: {personaje.Poder}, Combate: {personaje.Combate}, Id: {personaje.Id}");
        }*/
        int vidas = 3;

        EspacioPartidaEHistorial.Partida nuevaPartida = new EspacioPartidaEHistorial.Partida
        {
            NombreUsuario = nickname,
            Fecha = DateTime.Now,
            Personajes = personajes,
            PjPrincipal = jugPrincipal,
            Vidas = vidas
        };
        JugarPartida(nuevaPartida);
    }

}