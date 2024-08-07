public class Menu
{
    private HistorialJson historialJson = new HistorialJson();

    public void MostrarMenu()
    {
        string opc;

        Console.WriteLine("                     ================================");
        Console.WriteLine("                         1 - INICIAR JUEGO NUEVO");
        Console.WriteLine("                         2 -  CONTINUAR JUEGO");
        Console.WriteLine("                     ================================");
        
        opc = Console.ReadLine();

    }

    private void IniciarJuegoNuevo()
    {
        string nickname;

        Console.WriteLine("                     ================================");
        Console.WriteLine("                           INGRESE SU NICKNAME");
        
        nickname = Console.ReadLine();


    }



}