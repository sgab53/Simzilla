using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[ExecuteAlways]
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

    public void Smash(Collision collision)
    {
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
