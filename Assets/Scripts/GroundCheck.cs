using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GroundCheck : MonoBehaviour
{
    [SerializeField] [HideInInspector] CharacterControl character;
    [SerializeField] LayerMask groundCheckLayer;

    Vector3 normal;

    private void OnCollisionEnter(Collision collision)
    {
        if (groundCheckLayer.Contains(collision.gameObject.layer))
        {
            normal = Vector3.zero;

            foreach (var contact in collision.contacts)
            {
                normal += contact.normal;
            }

            if (normal != Vector3.zero)
                normal.Normalize();
            else
                normal = Vector3.up;

            character.SetGroundState(normal, true);
            Debug.Log("Collision");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (groundCheckLayer.Contains(collision.gameObject.layer))
        {

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (groundCheckLayer.Contains(collision.gameObject.layer))
        {
            normal = Vector3.up;

            character.SetGroundState(normal, false);

            Debug.Log("End Collision");
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (character == null)
            character = (CharacterControl)GetComponentInParent(typeof(CharacterControl));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        var start = transform.position + (Vector3.up * 0.01f);
        Gizmos.DrawLine(start, start + Vector3.down * character.GroundCheckDistance);
    }
#endif
}
