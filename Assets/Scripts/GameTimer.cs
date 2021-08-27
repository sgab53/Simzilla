using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float timeOut;

    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text msText;

    [SerializeField] private UnityEvent onTimeOut;

    private float startTime;

    private void Start()
    {
        startTime = Time.realtimeSinceStartup;
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
        }
    }
}
