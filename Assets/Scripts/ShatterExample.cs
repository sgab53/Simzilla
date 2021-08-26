using UnityEngine;
using System.Collections.Generic;
using EzySlice;
using System.Collections;

/**
 * An example fun script to show how a shatter operation can be applied to a GameObject
 * by repeatedly and randomly slicing an object
 */
[DisallowMultipleComponent]
public class ShatterExample : MonoBehaviour {

    [SerializeField] private Material sliceMaterial;
    [SerializeField] [Range(0, 10)] private int minSlices = 2;
    [SerializeField] [Range(0, 10)] private int maxSlices = 3;

    [Header("Rigidbody properties")]
    [Tooltip("Concrete min density: 2900 kg/m^3")]
    [SerializeField] private float minDensity = 100;
    [Tooltip("Concrete max density: 3200 kg/m^3")]
    [SerializeField] private float maxDensity = 500;

    private List<GameObject> chunks = new List<GameObject>();

    /**
     * This function will slice the provided object by the plane defined in this
     * GameObject. We use the GameObject this script is attached to define the position
     * and direction of our cutting Plane. Results are then returned to the user.
     */
    public bool ShatterObject(GameObject obj, int iterations, Material crossSectionMaterial = null) {
        if (iterations > 0) {
            GameObject[] slices = obj.SliceInstantiate(GetRandomPlane(obj.transform.position),
                                                       new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f),
                                                       crossSectionMaterial);

            if (slices != null) {
                // shatter the shattered!
                for (int i = 0; i < slices.Length; i++) {
                    if (ShatterObject(slices[i], iterations - 1, crossSectionMaterial)) {
                        // delete the parent
                        GameObject.DestroyImmediate(slices[i]);
                    }
                    else
                    {
                        chunks.Add(slices[i]);
                    }
                }

                return true;
            }

            return ShatterObject(obj, iterations - 1, crossSectionMaterial);
        }

        return false;
    }

    public void AttachToParent(Transform parent)
    {
        foreach (var chunk in chunks)
        {
            var rb = chunk.AddComponent<Rigidbody>();
            var mesh = chunk.AddComponent<MeshCollider>();
            mesh.convex = true;
            var size = mesh.bounds.size;
            rb.mass = size.x * size.y * size.z * (Random.Range(minDensity, maxDensity));
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            var pos = chunk.transform.localPosition;
            var rot = chunk.transform.localRotation;
            var scl = chunk.transform.localScale;

            chunk.transform.SetParent(parent);

            chunk.transform.localPosition = pos;
            chunk.transform.localRotation = rot;
            chunk.transform.localScale = scl;
        }

        parent.gameObject.SetActive(false);

        chunks.Clear();
    }

    /**
     * Given an offset position and an offset scale, calculate a random plane
     * which can be used to randomly slice an object
     */
    public EzySlice.Plane GetRandomPlane(Vector3 positionOffset) {
        Vector3 randomDirection = Random.onUnitSphere;

        return new EzySlice.Plane(Vector2.zero, randomDirection);
    }

    public void SliceAll(GameObject[] objects)
    {
        foreach (var go in objects)
        {
            Destructible destructible = go.GetComponent<Destructible>();
            destructible.GenerateParent();
            bool done = ShatterObject(go, Random.Range(minSlices, maxSlices), sliceMaterial);
            if (destructible.SlicedParent.gameObject.GetComponent<TimedDestruction>() == null)
            {
                destructible.SlicedParent.gameObject.AddComponent<TimedDestruction>();
            }
            AttachToParent(destructible.SlicedParent);
            go.layer = 3;
        }
    }
}
