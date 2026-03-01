# Taller-SMTP
Juego 3D de desafíos y progreso, con sistema de notificaciones por correo según las acciones del jugador.


# Evento que Dispara la Notificación

Las notificaciones por correo se activan automáticamente cuando ocurren eventos importantes dentro del juego.

## Eventos configurados

- **Inicio del juego:** El jugador ingresa el correo del destinatario antes de comenzar. Este correo se almacena y será usado para las notificaciones.
- **Derrota:** Cuando el jugador cae del escenario.
- **Progreso:** Cuando el jugador colisiona con el primer cubo.
- **Victoria:** Cuando el jugador colisiona con el cubo final.
- **Finalización:** Al terminar el juego, se solicita al jugador que ingrese un mensaje sobre su experiencia, el cual sera la notificación final.

En cada caso, el sistema genera un asunto y mensaje específico y ejecuta el método `SendEmail()` para enviar la notificación mediante SMTP.
