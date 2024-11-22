# RPSGame
Technical Test - Rock, Paper, Scissors

El juego se encuentra funcional, solo faltan algunas mejoras que no llegue a implementar por falta de tiempo, pero es un MVP funcional.
Falta guardar los jugadores en el DbSet Players para mantener el historial de victorias y derrotas. Tampoco llegue a alojarlo en Azure o AWS.

Para correr el servidor de backend, se debe abrir una terminal y en la raiz de la carpeta y ejecutar:
$ cd RPSGame
$ dotnet run

Para correr el servidor de front se debe ejecutar:
$ cd rps-game
$ ng serve

Por defecto el front se levanta en localhost:4200 y el back en localhost:5109 en mi caso.