using UnityEngine;

public class WinManager : MonoBehaviour
{
    public ScoreSystem scoreSystem; 
    public int winScore = 9;        
    public GameObject winCanvas;    

    private bool hasWon = false;

    void Start()
    {
        if (winCanvas != null)
        {
            winCanvas.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (!hasWon && scoreSystem.score >= winScore)
        {
            hasWon = true;
            Win();
        }

    }

    void Win()
    {
        Debug.Log("¡Ganaste!");
        winCanvas.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}