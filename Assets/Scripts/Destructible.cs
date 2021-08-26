using UnityEngine;
using System.Collections.Generic;

public class Destructible : MonoBehaviour
{
    [SerializeField] private Transform slicedParent;

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
    }

    public void Smash()
    {
        gameObject.SetActive(false);
        slicedParent.gameObject.SetActive(true);

        int score = 0;
        foreach (var chunk in chunks)
        {
            var velocity = chunk.attachedRigidbody.velocity;
            var angularVelocity = chunk.attachedRigidbody.angularVelocity;
            var size = chunk.bounds.size;

            score += (int)(((velocity.x * velocity.y * velocity.z) +
                        (angularVelocity.x * angularVelocity.y * angularVelocity.z) +
                        (size.x * size.y * size.z)) * 0.01f);
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
            Smash();
        }
    }
}
