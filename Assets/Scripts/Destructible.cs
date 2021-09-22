using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[ExecuteAlways]
public class Destructible : MonoBehaviour
{
    [SerializeField] private Transform slicedParent;
    
    private AudioSource crash;
    private AudioSource shatter;

    LayerMask kaijuLayer = 6;

    public Transform SlicedParent => slicedParent;

    [SerializeField] [HideInInspector] private List<Collider> chunks;

    private void Awake()
    {
        chunks = new List<Collider>();

        for (int i = 0; i < slicedParent.childCount; i++)
        {
            chunks.Add(slicedParent.GetChild(i).GetComponent<Collider>());
        }

        var sources = GetComponentsInParent<AudioSource>();
        crash = sources[0];
        shatter = sources[1];

        crash.volume = 0.1f;
        shatter.volume = 0.06f;
    }

    public void Smash(Collision collision)
    {
        crash.clip = AudioManager.Instance.GetCrushClip();
        crash.pitch = Random.Range(1.0f, 1.5f);
        crash.Play();
        shatter.clip = AudioManager.Instance.GetShatterClip();
        shatter.pitch = Random.Range(.75f, 1.2f);
        shatter.Play();

        slicedParent.gameObject.SetActive(true);
        gameObject.SetActive(false);

        float score = 0.0f;
        foreach (var chunk in chunks)
        {
            var size = chunk.bounds.size;
            var impulse = collision.relativeVelocity;

            chunk.attachedRigidbody.AddForceAtPosition(impulse * chunk.attachedRigidbody.mass, collision.contacts[0].point);

            var impulseScore = (Mathf.Abs(impulse.x) +
                                Mathf.Abs(impulse.y) +
                                Mathf.Abs(impulse.z)) / 3;

            score += (impulseScore + (size.x * size.y * size.z));
        }

        ScoreManager.Instance.AddScore(score);
    }

    public void GenerateParent()
    {
        var parent = transform.parent.Find("Sliced Parent");

        if (parent == null)
        {
            parent = new GameObject("Sliced Parent").transform;
            parent.parent = transform.parent;
        }

        if (slicedParent == null)
        {
            slicedParent = parent;
            slicedParent.localPosition = Vector3.zero;
            slicedParent.localRotation = Quaternion.identity;
            slicedParent.localScale = Vector3.one;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == kaijuLayer)
        {
            Smash(collision);
        }
    }
}
