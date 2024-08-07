using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EspacioPartidaEHistorial;


string nombreArchivo = "partidas.json";
HistorialJson historialJson = new HistorialJson(nombreArchivo);

ComplementoGrafico.MostrarLineasDivisorias();
ComplementoGrafico.MostrarTitulo();
ComplementoGrafico.MostrarLineasDivisorias();

MenuYJuego menu = new MenuYJuego(historialJson);
menu.MostrarMenu();

