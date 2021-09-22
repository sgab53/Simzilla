using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [SerializeField][HideInInspector] CharacterControl character;
    [SerializeField][HideInInspector] Transform cam;

    Vector3 movement;
    Vector3 camForward;

    // Get input
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        camForward = Vector3.Normalize(Vector3.Scale(cam.forward, new Vector3(1, 0, 1)));
        movement = v * camForward + h * cam.right;
    }

    private void FixedUpdate()
    {
        character.Move(movement);
    }

    private void Skill()
    {
        // skill logic
    }

    private void Attack()
    {
        // attack logic
    }

    private void ReleaseJump()
    {
        // jump logic
    }

    private void LoadJump()
    {
        // jump logic
    }

    private void Move(Vector3 movement)
    {
        // character.Move(...);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (character == null)
            character = (CharacterControl)GetComponent(typeof(CharacterControl));
        if (cam == null)
            cam = Camera.main.transform;
    }
#endif
}
