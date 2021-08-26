using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private Transform slicedParent;

    LayerMask kaijuLayer = 6;

    public Transform SlicedParent => slicedParent;

    public void Smash()
    {
        gameObject.SetActive(false);
        slicedParent.gameObject.SetActive(true);
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
