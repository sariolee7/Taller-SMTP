# Taller-SMTP
Juego 3D de desafíos y progreso, con sistema de notificaciones por correo según las acciones del jugador.

# Evento que Dispara la Notificación
Las notificaciones por correo se activan automáticamente cuando ocurren eventos importantes dentro del juego.

## Eventos configurados

- **Inicio del juego:** El jugador ingresa el correo del destinatario antes de comenzar. Este correo se almacena y será usado para las notificaciones.
- **Derrota:** Cuando el jugador cae del escenario (posición Y menor al límite).
- **Progreso:** Cuando el jugador colisiona con el primer cubo.
- **Victoria:** Cuando el jugador colisiona con el cubo final.
- **Finalización:** Al terminar el juego, se solicita al jugador que ingrese un mensaje sobre su experiencia, el cual puede ser incluido en la notificación final.

En cada caso, el sistema genera un asunto y mensaje específico y ejecuta el método `SendEmail()` para enviar la notificación mediante SMTP.

# User Flow

| Step | Action | Component | Result |
|------|--------|------------|--------|
| 1 | Juego inicia con menú visible | menuCanvas (Canvas) | Jugador puede ingresar su correo |
| 2 | Usuario ingresa su correo | emailInput (TMP_InputField) | Correo almacenado en el campo |
| 3 | Usuario hace clic en "Start" | StartGame() method | Se valida que el correo no esté vacío |
| 4 | Menú se desactiva | menuCanvas.SetActive(false) | Interfaz de inicio se oculta |
| 5 | Juego se activa | gameStarted = true | Se habilita la lógica del juego |
| 6 | Mouse se oculta y bloquea | Cursor.lockState / Cursor.visible | Control total del jugador |
| 7 | Jugador interactúa con objetos | OnCollisionEnter() | Se detectan eventos del juego |
| 8 | Evento de progreso o victoria | CompareTag("Cube"/"Win") | Se define asunto y mensaje |
| 9 | Evento de derrota | transform.position.y < fallLimit | Se genera mensaje de pérdida |
| 10 | Envío de notificación | SendEmail() + SMTP | Correo enviado al destinatario ingresado |


##  Configuración y Envío de Correo SMTP

El método configura la conexión con el servidor de Gmail y envía el mensaje al correo ingresado por el usuario.

```csharp
public void SendEmail()
{
    if (string.IsNullOrEmpty(toEmail))
    {
        Debug.Log("No hay correo ingresado");
        return;
    }

    string fromEmail = "tu_correo@gmail.com";
    string password = "tu_contraseña_app";

    MailMessage mail = new MailMessage();
    mail.From = new MailAddress(fromEmail);
    mail.To.Add(toEmail);
    mail.Subject = subject;
    mail.Body = body;

    SmtpClient smtp = new SmtpClient("smtp.gmail.com")
    {
        Port = 587,
        Credentials = new NetworkCredential(fromEmail, password),
        EnableSsl = true
    };

    try
    {
        smtp.Send(mail);
        Debug.Log("Email enviado correctamente");
    }
    catch (Exception ex)
    {
        Debug.Log("Error: " + ex.Message);
    }
}
```

# Arquitectura del Proyecto

La implementación del taller sigue una arquitectura organizada por responsabilidades dentro de Unity.

## •  Capa de Interfaz de Usuario (UI) – SimpleEmailSender.cs

- Gestiona los componentes de Unity (Canvas, TMP_InputField).
- Controla el botón StartGame().
- Valida que el usuario ingrese un correo.
- Oculta el menú (menuCanvas.SetActive(false)).
- Controla la pausa del juego (Time.timeScale) y el cursor.

Responsabilidad:
Interacción con el jugador y control visual del sistema.

## •  Capa de Lógica del Juego – SimpleEmailSender.cs

- Detecta derrota cuando el jugador cae (fallLimit).
- Detecta colisiones con:
  - cubeTag → Progreso.
  - cubeWin → Victoria.
- Define el asunto (subject) y el mensaje (body).
- Evita envíos repetidos con la variable emailSent.

Responsabilidad:
Controlar eventos del juego y decidir cuándo enviar notificaciones.

## •  Capa de Infraestructura – Método SendEmail()

- Configura la conexión SMTP con Gmail.
- Crea el objeto MailMessage.
- Maneja autenticación con credenciales.
- Implementa control de errores con try-catch.
- Envía el correo usando smtp.Send().

Responsabilidad:
Gestionar la comunicación externa mediante protocolo SMTP.

---

# Flujo General del Sistema

1. El juego inicia en pausa con el menú activo.
2. El usuario ingresa su correo.
3. Se oculta el menú y el juego comienza.
4. Ocurre un evento (progreso, victoria o derrota).
5. Se construye el mensaje.
6. Se envía el correo al usuario.


# Manejo de Respuestas del Servidor SMTP

El envío del correo se realiza mediante el método `smtp.Send(mail)`, el cual establece una comunicación con el servidor SMTP de Gmail a través del puerto 587 utilizando TLS (EnableSsl = true).

## Proceso Técnico

1. Se establece conexión con `smtp.gmail.com`.
2. Se realiza autenticación mediante `NetworkCredential`.
3. Se negocia cifrado TLS.
4. Se envía el comando SMTP correspondiente (MAIL FROM, RCPT TO, DATA).
5. El servidor responde con un código de estado SMTP.

##  Manejo de Respuesta

El método se ejecuta dentro de un bloque `try-catch` para capturar excepciones del tipo `SmtpException` o `Exception`.

```csharp
try
{
    smtp.Send(mail);
    Debug.Log("Email enviado correctamente");
}
catch (SmtpException smtpEx)
{
    Debug.Log("Error SMTP: " + smtpEx.StatusCode);
}
catch (Exception ex)
{
    Debug.Log("Error general: " + ex.Message);
}
```
---
# Autor

Sara Elizabeth Inguilar Muñoz
