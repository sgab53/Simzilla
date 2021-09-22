using UnityEngine;

public static class ExtensionMethods
{
    /// <summary>
    /// Checks whether a layermask contains a layer.
    /// </summary>
    /// <param name="mask"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}
