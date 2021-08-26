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
        if (slicedParent == null)
        {
            slicedParent = new GameObject(gameObject.name + " Shattered").transform;
            slicedParent.position = transform.position;
            slicedParent.rotation = transform.rotation;
            slicedParent.localScale = transform.localScale;
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
