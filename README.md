# LA GUERRA DE LOS MUNDOS
**No basado en la pelicula**

## Descripción
Es un mini juego en el que podrás aventurarte entre todos los personajes de todos (o casi todos) los universos de Super héroes como ser Marvel, DC, personajes de Harry Potter, entre otros.
En este mini juego, tendrás la oportunidad de utilizar todos los atributos de los personajes como ser su fuerza, inteligencia, poder, velocidad, durabilidad y habilidad de combate, pudiendo realizar ataques basados en estas estadísticas donde todo suma para acertar el ataque.

## Funcionalidades

- **Inicio de Juego Nuevo**: Permite al usuario ingresar un nickname y empezar una nueva partida. Los personajes se obtienen desde una API externa y se guardan en un archivo JSON.
- **Continuar Juego**: Permite al usuario cargar una partida guardada previamente utilizando el nickname del jugador.
- **Combate**: Realiza combates entre el personaje principal y una serie de enemigos. El jugador pierde una vida por cada derrota y la partida termina cuando se quedan sin vidas.
- **Guardar Partida**: Guarda el estado actual del juego en un archivo JSON cuando el jugador decide finalizar la partida.

## Uso

1. Al iniciar el juego, se te presentará un menú con las siguientes opciones:
    - **1 - Iniciar Juego Nuevo**: Permite comenzar una nueva partida.
    - **2 - Continuar Juego**: Permite continuar una partida guardada previamente.

2. Si eliges iniciar un juego nuevo, se te pedirá que ingreses un nickname. Luego, el juego obtendrá los personajes desde la API y comenzará la partida.

3. Durante el juego, se realizarán combates entre el personaje principal y una serie de enemigos. Después de cada combate, se te pedirá que presiones 'Enter' para continuar.

4. Si decides terminar el juego, el estado de la partida se guardará en un archivo JSON para que puedas continuar en otro momento.

5. Si eliges continuar una partida, se te pedirá que ingreses tu nickname para cargar la partida guardada y continuar desde donde la dejaste.

## Mejoras que se podrían aplicar

1. Que el Usuario pueda elegir de una lista de personajes, cuál será el principal.
2. Que el usuario elija en su turno si decide atacar o defenderse del próximo ataque.

## API Utilizada

- La API que utilicé fue https://www.superheroapi.com/ es una API de todos los superhéroes del universo del cómic. Se accede a los datos a través de una API REST. 
- Para acceder a ella necesitas una cuenta de Git para obtener un token de acceso.