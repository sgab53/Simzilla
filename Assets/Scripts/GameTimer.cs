using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float timeOut;

    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text msText;

    [SerializeField] private UnityEvent onTimeOut;
    [SerializeField] private UnityEvent onAfterTimeout;

    private float startTime;

    private void Start()
    {
        startTime = Time.realtimeSinceStartup;

        var time = Mathf.Clamp(timeOut, 0, timeOut);

        var ms = Mathf.FloorToInt((time * 1000) % 1000);
        var seconds = Mathf.FloorToInt(time % 60);
        var minutes = Mathf.FloorToInt(time / 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        msText.text = string.Format(":{0:000}", ms);
    }

    private void LateUpdate()
    {
        var count = Time.realtimeSinceStartup - startTime;
        var time = Mathf.Clamp(timeOut - count, 0, timeOut);

        var ms = Mathf.FloorToInt((time * 1000) % 1000);
        var seconds = Mathf.FloorToInt(time % 60);
        var minutes = Mathf.FloorToInt(time / 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        msText.text = string.Format(":{0:000}", ms);

        if (time <= 0.0f)
        {
            enabled = false;
            onTimeOut.Invoke();
            Invoke("ReloadScene", 3.0f);
        }
    }

    private void AfterTimeout()
    {
        onAfterTimeout.Invoke();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
