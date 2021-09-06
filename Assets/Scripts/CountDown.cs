using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CountDown : MonoBehaviour
{
    [SerializeField] private TMP_Text countdown;

    [SerializeField] private UnityEvent onTimeOut;

    private float cd = 3.0f;
    private float startTime;

    private void Start()
    {
        startTime = Time.realtimeSinceStartup;
    }

        // Update is called once per frame
    void LateUpdate()
    {
        var count = Time.realtimeSinceStartup - startTime;
        var time = Mathf.Clamp(cd - count, 0, cd);

        var seconds = Mathf.CeilToInt(time % 60);

        countdown.text = string.Format("{0:0}", seconds);

        if (time <= 0.0f)
        {
            enabled = false;
            onTimeOut.Invoke();
        }
    }
}
