using System.Runtime;

public class Combate
{
    public GeneracionPersonaje IniciarCombate(GeneracionPersonaje jug1, GeneracionPersonaje jug2)
    {
        Console.WriteLine($"Combate entre {jug1.Nombre} y {jug2.Nombre}");

        while (jug1.Durabilidad > 0 && jug2.Durabilidad > 0)
        {
            RealizarTurno(jug1, jug2);

            if (jug2.Durabilidad <= 0)
            {
                Console.WriteLine($"{jug1.Nombre} ha ganado el combate");
                return jug1;
            }

            RealizarTurno(jug2, jug1);
            if (jug1.Durabilidad <= 0)
            {
                Console.WriteLine($"{jug2.Nombre} ha ganado el combate");
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

        Console.WriteLine($"{ataca.Nombre} ataca a {defiende.Nombre} y causa {daño} puntos de daño");
        Console.WriteLine($"{defiende.Nombre} tiene {defiende.Durabilidad} puntos de durabilidad restantes");
    }

    private int CalcularDaño(GeneracionPersonaje jug)
    {
        int daño;

        daño = jug.Fuerza + jug.Poder / 2;

        return daño;
    }








    



}