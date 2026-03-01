using UnityEngine;

public class FallDetector : MonoBehaviour
{
    public float fallLimit = -10f; // Altura límite

    void Update()
    {
        if (transform.position.y < fallLimit)
        {
            Lose();
        }
    }

    void Lose()
    {
        Debug.Log("Perdiste 😢");

        // Opcional: reiniciar escena
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}