using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMessageSender : MonoBehaviour
{
    public TMP_InputField inputField;
    public SimpleEmailSender emailSender;

    public void SendPlayerMessage()
    {
        if (inputField == null || emailSender == null)
        {
            Debug.LogError("Falta asignar InputField o EmailSender en el Inspector");
            return;
        }

        string playerMessage = inputField.text.Trim();
        if (string.IsNullOrEmpty(playerMessage))
        {
            Debug.Log("El mensaje está vacío");
            return;
        }
        emailSender.subject = "Mensaje del jugador";
        emailSender.body = playerMessage;

        emailSender.SendEmail();
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}