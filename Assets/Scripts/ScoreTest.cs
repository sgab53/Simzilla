using UnityEngine;
using TMPro;

public class ScoreTest : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int score = 0;

    private void Start()
    {
        Invoke("AddScore", Random.Range(0.5f, 3f));
    }

    private void AddScore()
    {
        score += Random.Range(1, 1200);
        scoreText.text = string.Format("{0:000000}", score);
        Invoke("AddScore", Random.Range(0.5f, 3f));
    }
}
