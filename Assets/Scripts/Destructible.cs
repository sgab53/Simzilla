using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private Transform slicedParent;

    LayerMask kaijuLayer = 6;

    public Transform SlicedParent => slicedParent;

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

    public void Smash()
    {
        gameObject.SetActive(false);
        slicedParent.gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == kaijuLayer)
        {
            Smash();
        }
    }
}
