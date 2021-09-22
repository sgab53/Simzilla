using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource crush;
    [SerializeField] private AudioSource[] shatters;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public AudioClip GetCrushClip()
    {
        return crush.clip;
    }

    public AudioClip GetShatterClip()
    {
        return shatters[Random.Range(0, shatters.Length)].clip;
    }
}
