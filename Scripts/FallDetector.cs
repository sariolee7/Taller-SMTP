using UnityEngine;

public class FallDetector : MonoBehaviour
{
    public float fallLimit = -10f;

    void Update()
    {
        if (transform.position.y < fallLimit)
        {
            Lose();
        }
    }

    void Lose()
    {
        Debug.Log("Perdiste");
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}