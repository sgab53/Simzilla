using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource footstep;
    [SerializeField] AudioSource swipe;

    public void PlayFootstep()
    {
        footstep.Play();
    }
}
