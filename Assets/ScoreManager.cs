using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Animator scoreAnimator;

    private int scoreUpAnimation = Animator.StringToHash("ScoreUpAnimation");

    private float score = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void AddScore(float s)
    {
        score += s;
        scoreText.text = string.Format("{0:000000}", (int)score);
        scoreAnimator.Play(scoreUpAnimation);
    }


}
