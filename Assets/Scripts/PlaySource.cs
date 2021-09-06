using UnityEngine;

public class PlaySource : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    private void OnCollisionEnter(Collision collision)
    {
        source.Play();
    }
}
