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
            Console.WriteLine($"{jug1.Nombre} tiene {jug1.Durabilidad} puntos de durabilidad");
            Console.WriteLine($"{jug2.Nombre} tiene {jug2.Durabilidad} puntos de durabilidad");


            RealizarTurno(jug1, jug2);
            
            if (jug2.Durabilidad <= 0)
            {
                Console.WriteLine($"{jug2.Nombre} ha sido derrotado");
                jug1.Victorias++;

                if (jug1.Victorias % 3 == 0)  //Agregué el parámetro de Victorias para que cada 3 victorias mi personaje pueda ser mejorado, de lo contrario sería muy desbalanceado que por cada victoria, se pueda mejorar
                {
                    MejorarAtributo(jug1);
                }
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

        if(AtaqueAcertado(ataca))
        {
            daño = CalcularDaño(ataca);
            defiende.Durabilidad -= daño;

            if(defiende.Durabilidad < 0)
            {
                defiende.Durabilidad = 0; //Para que la vida no se muestre en caso de ser < 0
            }
            Console.WriteLine($"{ataca.Nombre} ataca a {defiende.Nombre} y causa {daño} puntos de daño");
        }
        else
        {
            Console.WriteLine($"{ataca.Nombre} falló el ataque contra {defiende.Nombre}");

        }
        Console.WriteLine($"{defiende.Nombre} tiene {defiende.Durabilidad} puntos de durabilidad restantes");
    }

    private int CalcularDaño(GeneracionPersonaje jug)
    {
        int dañoBase, dañoACausar; //dañoBase me permite utilizar los atributos de fuerza y poder para obtener un equilibrio entre ambos a la hora de hacer el cálculo del daño a causar
        Random random = new Random();
        double varDaño = random.NextDouble() * 0.5 + 0.5; //me permitirá equilibrar el daño entre el 50% y 100% del valor de la fuerza del jugador
        //random.NextDouble() * 0.5 me da un random entre 0.0 y 0.5, le sumo el otro 0.5 para poder equilibrar esto entre 0.5 y 1.0

        dañoBase = (int)((jug.Fuerza * 0.5) + (jug.Poder * 0.5)); //combino la suma de la mitad de ambos atributos. | hice el casteo ya que los atributos son "double"
        dañoACausar = (int)(dañoBase * varDaño); // obtengo el daño a causar a partir del daño base y el porcentaje de variacion de éste, es decir que hará un efecto del 50 al 100%
        
        return dañoACausar;
    }

    private bool AtaqueAcertado(GeneracionPersonaje jug)
    {
        Random random = new Random();
        double probabilidadBase;
        bool ataqueExitoso;

        probabilidadBase = 0.8 + jug.Velocidad * 0.01; //esto determina la probabilidad de que el ataque sea acertado entre un 80% y un 1% más por cada punto de velocidad.
        if (probabilidadBase > 1.0)
        {
            probabilidadBase = 1.0;
        }

        ataqueExitoso = random.NextDouble() < probabilidadBase; //determina si el ataque es acertado

        return ataqueExitoso;
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
            Console.WriteLine("Opcion no valida. Intente de nuevo");
            MejorarAtributo(personaje);
        }

    }



}