using System.Runtime;
using System.Security;

public class Combate
{
    public GeneracionPersonaje IniciarCombate(GeneracionPersonaje jug1, GeneracionPersonaje jug2)
    {
        ComplementoGrafico.MostrarCombateComenzo();

        Console.WriteLine($"Combate entre {jug1.Nombre} y {jug2.Nombre}");

        while (jug1.Durabilidad > 0 && jug2.Durabilidad > 0)
        {
            RealizarTurno(jug1, jug2);

            if (jug2.Durabilidad <= 0)
            {
                Console.WriteLine($"{jug2.Nombre} ha sido derrotado");
                MejorarAtributo(jug1);
                return jug1;
            }

            RealizarTurno(jug2, jug1);
            if (jug1.Durabilidad <= 0)
            {
                Console.WriteLine($"{jug1.Nombre} ha sido derrotado");
                return jug2;
            }

        }

        return null; // en caso de empates, aunque es poco probable que suceda xd
    }

    private void RealizarTurno(GeneracionPersonaje ataca, GeneracionPersonaje defiende)
    {
        int daño;

        daño = CalcularDaño(ataca);
        defiende.Durabilidad -= daño;

        if (defiende.Durabilidad <= 0)
        {
            defiende.Durabilidad = 0; // la vida no debería mostrarse como <0
        }

        Console.WriteLine($"{ataca.Nombre} ataca a {defiende.Nombre} y causa {daño} puntos de daño");
        Console.WriteLine($"{defiende.Nombre} tiene {defiende.Durabilidad} puntos de durabilidad restantes");
    }

    private int CalcularDaño(GeneracionPersonaje jug)
    {
        int daño;

        daño = jug.Fuerza + jug.Poder / 2;

        return daño;
    }

    private void MejorarAtributo(GeneracionPersonaje personaje)
    {
        ComplementoGrafico.MejoraPersonaje();
        Console.WriteLine("Seleccione un Atributo a Mejorar");
        Console.WriteLine("1 - Inteligencia");
        Console.WriteLine("2 - Fuerza");
        Console.WriteLine("3 - Velocidad");
        Console.WriteLine("4 - Durabilidad");
        Console.WriteLine("5 - Poder");
        Console.WriteLine("6 - Combate");

        string opc = Console.ReadLine();
        int atributo;

        if(int.TryParse(opc, out atributo) && atributo >= 1 && atributo <= 6)
        {
            double porcMejora = 10.0; //Valor predeterminado de porcentaje
            personaje.MejorarAtributos(atributo, porcMejora);
            Console.WriteLine("¡Atributo Mejorado Correctamente!");

        }
        else
        {
            Console.WriteLine("Opcion no valida");
        }

    }








    



}