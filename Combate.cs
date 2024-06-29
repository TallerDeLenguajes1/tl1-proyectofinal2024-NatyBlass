using System.Runtime;

public class Combate
{
    public GeneracionPersonaje IniciarCombate(GeneracionPersonaje jug1, GeneracionPersonaje jug2)
    {
        Console.WriteLine($"Combate entre {jug1.Nombre} y {jug2.Nombre}");

        while (jug1.Durabilidad > 0 && jug2.Durabilidad > 0)
        {
            RealizarTurno(jug1, jug2);
        }
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