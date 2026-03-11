using UnityEngine;
using System;
using System.Net;
using System.Net.Mail;
using UnityEngine.UI;
using TMPro;
public class SimpleEmailSender : MonoBehaviour
{
    public string cubeTag = "Cube";
    public string cubeWin = "Win";
    public bool emailSent = false;
    public string subject;
    public string body;
    public float fallLimit = -10f;

    public GameObject menuCanvas;
    public TMP_InputField emailInput;
    private string toEmail;

void Start()
{
    Time.timeScale = 0f;
    menuCanvas.SetActive(true);

    Cursor.visible = true;
    Cursor.lockState = CursorLockMode.None;
}

    void Update()
    {

        if (Time.timeScale == 0f) return;

        if (transform.position.y < fallLimit)
        {
            subject = "Perdiste el juego";
            body = "El jugador cayó del escenario.";
            SendEmail();
            Debug.Log("Jugador cayó y perdió");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!emailSent && collision.gameObject.CompareTag(cubeTag))
        {
            emailSent = true;
            subject = "Jugador tocó el primer cubo";
            body = "El jugador ha activado el primer collider en el juego";
            SendEmail();
        }

        if (collision.gameObject.CompareTag(cubeWin))
        {
            subject = "Jugador finalizo el juego";
            body = "El jugador ha ganado!!";
            SendEmail();
        }
    }

    public void StartGame()
    {
        toEmail = emailInput.text;

        if (string.IsNullOrEmpty(toEmail))
        {
            Debug.Log("Por favor ingresa un correo válido");
            return;
        }

        Debug.Log("Correo guardado: " + toEmail);
        menuCanvas.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void SendEmail()
    {
        if (string.IsNullOrEmpty(toEmail))
        {
            Debug.Log("No hay correo ingresado");
            return;
        }

        string fromEmail = "Colocar correo";
        string password = "Colocar contraseña";

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
}