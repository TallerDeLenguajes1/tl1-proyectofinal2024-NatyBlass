using EspacioPartidaEHistorial;
public class MenuYJuego
{
    private EspacioPartidaEHistorial.HistorialJson historialJson = new EspacioPartidaEHistorial.HistorialJson();

    public void MostrarMenu()
    {
        string opc;

        Console.WriteLine("                     ================================");
        Console.WriteLine("                         1 - INICIAR JUEGO NUEVO");
        Console.WriteLine("                         2 -  CONTINUAR JUEGO");
        Console.WriteLine("                     ================================");
        
        opc = Console.ReadLine();

        switch (opc)
        {
            case "1":
                IniciarJuegoNuevo();
                break;
            case "2":
                break;
            default:
                Console.WriteLine("LA OPCION QUE INGRESO NO ES VALIDA");
                MostrarMenu();
                break;
        }




    }

    private async void IniciarJuegoNuevo() //async para poder usar await
    {
        string nickname;

        Console.WriteLine("                     ================================");
        Console.WriteLine("                           INGRESE SU NICKNAME");
        
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

        int durabilidad = jugPrincipal.Durabilidad;
        int vidas = 3;

        Combate combate = new Combate();

        EspacioPartidaEHistorial.Partida nuevaPartida = new EspacioPartidaEHistorial.Partida
        {
            NombreUsuario = nickname,
            Fecha = DateTime.Now,
            Personajes = personajes,
            PjPrincipal = jugPrincipal,
            Vidas = vidas
        };

        //Ahora voy a empezar a aplicar la logica de combate



        foreach (var enemigo in personajes) 
        {
            if (vidas > 0)
            {
                Console.WriteLine($"\n                                                        {jugPrincipal.Nombre} vs {enemigo.Nombre}");
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
            ComplementoGrafico.MostrarLineasDivisorias();
        }

        if(vidas > 0)
        {
            ComplementoGrafico.GanadorFinal();
        }
        else
        {
            ComplementoGrafico.DerrotaFinal();
        }



    }



}