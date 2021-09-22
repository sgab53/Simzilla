using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class CharacterControl : MonoBehaviour
{
    [SerializeField] float groundCheckDistance = 0.101f;
    //[SerializeField] LayerMask groundCheckLayer;

    [SerializeField] [HideInInspector] Rigidbody rb;

    Vector3 groundNormal;
    bool grounded;

    Vector3 ProjectOnContactPlane(Vector3 vector, Vector3 normal)
    {
        return vector - normal * Vector3.Dot(vector, normal);
    }

    public void Move(Vector3 movement)
    {
        if (movement.sqrMagnitude > 1f)
            movement.Normalize();

        movement = transform.InverseTransformDirection(movement);

        //CheckGround();

        movement = Vector3.ProjectOnPlane(movement, groundNormal);

        rb.MovePosition(transform.position + (movement * Time.fixedDeltaTime));
    }

    public void SetGroundState(Vector3 normal, bool isGrounded)
    {
        groundNormal = normal;
        grounded = isGrounded;
    }

/*    private void CheckGround()
    {
        RaycastHit[] hit = new RaycastHit[1];

        Vector3 start = transform.position + (Vector3.up * 0.1f);

#if UNITY_EDITOR
        Debug.DrawLine(start, start + (Vector3.down * groundCheckDistance));
#endif

        if (Physics.RaycastNonAlloc(start, Vector3.down, hit, groundCheckDistance, groundCheckLayer, QueryTriggerInteraction.Ignore) > 0)
        {
            groundNormal = hit[0].normal;
            grounded = true;
        }
        else
        {
            groundNormal = Vector3.up;
            grounded = false;
        }
    }*/

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (rb == null)
            rb = (Rigidbody)GetComponent(typeof(Rigidbody));
    }

    private void OnGUI()
    {
        string text = string.Format(
            "Grounded: {0}\n",
            grounded
            );
        GUILayout.TextArea(text);
    }

    public float GroundCheckDistance => groundCheckDistance;
#endif
}
