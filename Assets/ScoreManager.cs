using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Animator scoreAnimator;

    private int scoreUpAnimation = Animator.StringToHash("ScoreUpAnimation");

    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void AddScore(int s)
    {
        score += s;
        scoreText.text = string.Format("{0:000000}", score);
        scoreAnimator.Play(scoreUpAnimation);
    }


}
