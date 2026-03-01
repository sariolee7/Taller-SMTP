using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreSystem : MonoBehaviour
{
    public string cubeTag = "Cube";
    public string cubeWin = "Win";

    public int score = 0;
    public TextMeshProUGUI scoreText;

    private HashSet<GameObject> cubesTouched = new HashSet<GameObject>();

    void Start()
    {
        UpdateScoreUI();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Detecta si es Cube o Win
        if (collision.gameObject.CompareTag(cubeTag) || 
            collision.gameObject.CompareTag(cubeWin))
        {
            if (!cubesTouched.Contains(collision.gameObject))
            {
                cubesTouched.Add(collision.gameObject);
                score++;
                UpdateScoreUI();
            }
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Puntaje: " + score;
    }
}